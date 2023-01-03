using System.Net;
using System.Runtime.CompilerServices;
using Catharsis.Commons;
using RestSharp;

namespace Yandex.Translator;

internal sealed class Api : IApi
{
  private bool disposed;

  private Uri EndpointUrl { get; } = "https://translate.yandex.net/api/v1.5/tr".ToUri();
  private RestClient RestClient { get; }

  public Api(string apiKey)
  {
    RestClient = new RestClient(EndpointUrl);
    RestClient.Options.BaseUrl = $"{EndpointUrl}.json".ToUri();
    RestClient.AddDefaultParameter("key", apiKey);
    RestClient.UseSerializer<JsonRestSerializer>();
  }

  public async IAsyncEnumerable<ITranslationPair> PairsAsync([EnumeratorCancellation] CancellationToken cancellation = default)
  {
    var result = (await Request<TranslationPairsResult.Info>("/getLangs", null, cancellation).ConfigureAwait(false)).Result();

    foreach (var languages in result.Pairs.Select(pair => pair.Split('-')))
    {
      yield return new TranslationPair(languages[0], languages[1]);
    }
  }

  public async Task<string> DetectAsync(string text, CancellationToken cancellation = default)
  {
    var result = (await Request<DetectedLanguageResult.Info>("detect", new Dictionary<string, object> {{"text", text}}, cancellation).ConfigureAwait(false)).Result();

    if (result.Code != (int) HttpStatusCode.OK || result.Language.IsEmpty())
    {
      throw new TranslatorException(new Error(result.Code, "Cannot determine source language for text"));
    }

    return result.Language;
  }

  public async Task<ITranslation> TranslateAsync(ITranslationApiRequest request, CancellationToken cancellation = default)
  {
    var result = (await Request<TranslationResult.Info>("/translate", request.Parameters, cancellation).ConfigureAwait(false)).Result();

    var translation = result.ToString();

    if (result.Code != (int) HttpStatusCode.OK || result.Language.IsEmpty() || translation.IsEmpty())
    {
      throw new TranslatorException(new Error(result.Code, "Text translation failed"));
    }

    var languages = result.Language.Split('-');

    if (languages.Length != 2)
    {
      throw new TranslatorException(new Error(result.Code, $"Unspecified result languages pair: {languages}"));
    }

    return new Translation(languages[0], languages[1], translation);
  }

  public void Dispose()
  {
    Dispose(true);
    GC.SuppressFinalize(this);
  }

  private void Dispose(bool disposing)
  {
    if (!disposing || disposed)
    {
      return;
    }

    RestClient.Dispose();

    disposed = true;
  }

  private async Task<T> Request<T>(string resource, IReadOnlyDictionary<string, object> parameters = null, CancellationToken cancellation = default) where T : new()
  {
    var request = new RestRequest(resource)
    {
      RequestFormat = DataFormat.Json
    };

    if (parameters != null)
    {
      foreach (var parameter in parameters)
      {
        request.AddParameter(parameter.Key, parameter.Value?.ToInvariantString());
      }
    }

    var response = await RestClient.ExecuteGetAsync<T>(request, cancellation).ConfigureAwait(false);

    if (response.ErrorException != null || response.StatusCode != HttpStatusCode.OK)
    {
      var responseError = new Error((int) response.StatusCode, response.ErrorMessage ?? (response.StatusDescription ?? response.StatusCode.ToInvariantString()));
      throw new TranslatorException(responseError, response.ErrorException);
    }

    IError error = null;

    try
    {
      error = response.Content?.AsJson<Error.Info>()?.Result();
    }
    catch
    {
    }

    if (error != null && !error.Text.IsEmpty())
    {
      throw new TranslatorException(error);
    }

    return response.Data;
  }
}