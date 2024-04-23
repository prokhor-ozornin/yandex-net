using System.Net;
using System.Runtime.CompilerServices;
using Catharsis.Extensions;
using RestSharp;

namespace Yandex.Translator;

internal sealed class Api : IApi
{
  private RestClient RestClient { get; } = new("https://translate.yandex.net/api/v1.5/tr.json".ToUri(), configureSerialization: config => config.UseSerializer<JsonRestSerializer>());
  private bool disposed;

  public Api(string key) => RestClient.AddDefaultParameter("key", key);

  public async IAsyncEnumerable<ITranslationPair> PairsAsync([EnumeratorCancellation] CancellationToken cancellation = default)
  {
    var result = (await Request<TranslationPairsResult.Info>("/getLangs", null, cancellation).ConfigureAwait(false)).ToResult();

    foreach (var languages in result.Pairs.Select(pair => pair.Split('-')))
    {
      yield return new TranslationPair(languages[0], languages[1]);
    }
  }

  public async Task<string> DetectAsync(string text, CancellationToken cancellation = default)
  {
    if (text is null) throw new ArgumentNullException(nameof(text));
    if (text.IsEmpty()) throw new ArgumentException(nameof(text));

    var result = (await Request<DetectedLanguageResult.Info>("detect", new Dictionary<string, object> {{"text", text}}, cancellation).ConfigureAwait(false)).ToResult();

    if (result.Code != (int) HttpStatusCode.OK || result.Language.IsUnset())
    {
      throw new TranslatorException(new Error(result.Code, "Cannot determine source language for text"));
    }

    return result.Language;
  }

  public async Task<ITranslation> TranslateAsync(ITranslationApiRequest request, CancellationToken cancellation = default)
  {
    if (request is null) throw new ArgumentNullException(nameof(request));

    var result = (await Request<TranslationResult.Info>("/translate", request.Parameters, cancellation).ConfigureAwait(false)).ToResult();

    var translation = result.ToString();

    if (result.Code != (int) HttpStatusCode.OK || result.Language.IsUnset() || translation.IsUnset())
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
    if (resource is null) throw new ArgumentNullException(nameof(resource));
    if (resource.IsEmpty()) throw new ArgumentException(nameof(resource));

    var request = new RestRequest(resource)
    {
      RequestFormat = DataFormat.Json
    };

    if (parameters is not null)
    {
      foreach (var parameter in parameters)
      {
        request.AddParameter(parameter.Key, parameter.Value?.ToInvariantString());
      }
    }

    var response = await RestClient.ExecuteGetAsync<T>(request, cancellation).ConfigureAwait(false);

    if (response.ErrorException is not null || response.StatusCode != HttpStatusCode.OK)
    {
      var responseError = new Error((int) response.StatusCode, response.ErrorMessage ?? (response.StatusDescription ?? response.StatusCode.ToInvariantString()));
      throw new TranslatorException(responseError, response.ErrorException);
    }

    IError error = null;

    try
    {
      error = response.Content?.DeserializeAsJson<Error>();
    }
    catch
    {
    }

    if (error is not null && !error.Text.IsUnset())
    {
      throw new TranslatorException(error);
    }

    return response.Data;
  }
}