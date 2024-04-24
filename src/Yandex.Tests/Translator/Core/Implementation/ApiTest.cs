using Catharsis.Extensions;
using RestSharp;
using RestSharp.Serializers;
using FluentAssertions;
using Xunit;
using Yandex.Translator;
using Catharsis.Commons;
using FluentAssertions.Execution;

namespace Yandex.Tests.Translator;

/// <summary>
///   <para>Tests set for class <see cref="Api"/>.</para>
/// </summary>
public sealed class ApiTest : IntegrationTest
{
  /// <summary>
  ///   <para>Performs testing of class constructor(s).</para>
  /// </summary>
  [Fact]
  public void Constructors()
  {
    AssertionExtensions.Should(() => new Api(null)).ThrowExactly<ArgumentNullException>().WithParameterName("key");
    AssertionExtensions.Should(() => new Api(string.Empty)).ThrowExactly<ArgumentException>().WithParameterName("key");

    typeof(Api).Should().BeDerivedFrom<object>().And.Implement<IApi>();

    var api = new Api("apiKey");
    api.GetPropertyValue<ISerializer>("JsonSerializer").Should().NotBeNull();
    api.GetPropertyValue<IDeserializer>("JsonDeserializer").Should().NotBeNull();

    var client = api.GetFieldValue<RestClient>("restClient");
    var key = client.DefaultParameters.FirstOrDefault(parameter => parameter.Name == "key");
    key.Should().NotBeNull();
    key.Value.Should().Be("apiKey");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IApi.PairsAsync(CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void PairsAsync_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => Api.PairsAsync(Attributes.CancellationToken())).ThrowExactly<OperationCanceledException>();

      Validate([new TranslationPair("en", "ru"), new TranslationPair("ru", "en")], Api);
    }

    return;

    static void Validate(IEnumerable<ITranslationPair> result, IApi api) => api.PairsAsync().ToArray().Should().IntersectWith(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IApi.DetectAsync"/> method.</para>
  /// </summary>
  [Fact]
  public void Detect_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => Api.DetectAsync(null)).ThrowExactlyAsync<ArgumentNullException>().Await();
      AssertionExtensions.Should(() => Api.DetectAsync(string.Empty)).ThrowExactlyAsync<ArgumentException>().Await();
      AssertionExtensions.Should(() => Api.DetectAsync("text", Attributes.CancellationToken())).ThrowExactlyAsync<TaskCanceledException>().Await();

      Validate("en", "Hello, world", Api);
      Validate("ru", "Привет, мир", Api);
    }

    return;

    static void Validate(string result, string text, IApi api) => api.Detect(text).Should().BeOfType<string>().And.Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IApi.TranslateAsync(ITranslationApiRequest, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void TranslateAsync_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => Api.TranslateAsync(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("request").Await();
      AssertionExtensions.Should(() => Api.TranslateAsync(null, Attributes.CancellationToken())).ThrowExactlyAsync<OperationCanceledException>().Await();

      Validate(new Translation("ru", "en", "Hello world"), request => request.From("ru").To("en").Text("Привет, мир"), Api);
      Validate(new Translation("en", "ru", "Привет, мир"), request => request.From("en").To("ru").Text("Hello, world"), Api);
    }

    return;

    static void Validate(ITranslation result, Action<ITranslationApiRequest> request, IApi api)
    {
      var task = api.TranslateAsync(request);
      task.Should().BeAssignableTo<Task<ITranslation>>();

      var translation = task.Await();
      translation.Should().BeOfType<Translation>();
      translation.FromLanguage.Should().BeOfType<string>().And.Be(result.FromLanguage);
      translation.ToLanguage.Should().BeOfType<string>().And.Be(result.ToLanguage);
      translation.Text.Should().BeOfType<string>().And.Be(result.Text);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IApi.Dispose()"/> method.</para>
  /// </summary>
  [Fact]
  public void Dispose_Method()
  {
    using (new AssertionScope())
    {
      Validate(Api);
    }

    return;

    static void Validate(IDisposable disposable)
    {
      disposable.Dispose();

      AssertionExtensions.Should(disposable.Dispose).ThrowExactly<ObjectDisposedException>();
    }
  }
}