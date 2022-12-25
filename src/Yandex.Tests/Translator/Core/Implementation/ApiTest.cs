using Catharsis.Commons;
using RestSharp;
using RestSharp.Serializers;
using FluentAssertions;
using Xunit;
using Yandex.Translator;
using System.Configuration;

namespace Yandex.Tests.Translator;

/// <summary>
///   <para>Tests set for class <see cref="Api"/>.</para>
/// </summary>
public sealed class YandexTranslatorTests : IDisposable
{
  private IApi Api { get; } = Yandex.Api.Translator().Configure(configurator => configurator.ApiKey(ConfigurationManager.AppSettings["ApiKey"]));

  private CancellationToken Cancellation { get; } = new(true);

  /// <summary>
  ///   <para>Performs testing of class constructor(s).</para>
  /// </summary>
  [Fact]
  public void Constructors()
  {
    AssertionExtensions.Should(() => new Api(null!)).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => new Api(string.Empty)).ThrowExactly<ArgumentException>();

    var api = new Api("apiKey");
    api.Property("JsonSerializer").Should().NotBeNull().And.BeOfType<ISerializer>();
    api.Property("JsonDeserializer").Should().NotBeNull().And.BeOfType<IDeserializer>();

    var client = api.Field("restClient").To<RestClient>();
    //client.BaseUrl.ToString().Should().Be("https://translate.yandex.net/api/v1.5/tr");
    var key = client.DefaultParameters.FirstOrDefault(parameter => parameter.Name == "key");
    key.Should().NotBeNull();
    key.Value.Should().Be("apiKey");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IApi.Pairs(CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void Pairs_Method()
  {
    AssertionExtensions.Should(() => Api.Pairs(Cancellation)).ThrowExactly<TaskCanceledException>();

    using var api = Api;

    var pairs = api.Pairs().ToList().Await();
    pairs.Should().NotBeNullOrEmpty().And.Contain(translation => translation.FromLanguage == "en" && translation.ToLanguage == "ru").And.Contain(translation => translation.FromLanguage == "ru" && translation.ToLanguage == "en");
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IApi.Detect(string, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void Detect_Method()
  {
    AssertionExtensions.Should(() => Api.Detect(null!)).ThrowExactlyAsync<ArgumentNullException>().Await();
    AssertionExtensions.Should(() => Api.Detect(string.Empty)).ThrowExactlyAsync<ArgumentException>().Await();
    AssertionExtensions.Should(() => Api.Detect("text", Cancellation)).ThrowExactlyAsync<TaskCanceledException>().Await();

    using var api = Api;

    api.Detect("Hello, world").Should().Be("en");
    api.Detect("Привет, мир").Should().Be("ru");
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IApi.Translate(ITranslationApiRequest, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void Translate_Method()
  {
    AssertionExtensions.Should(() => Api.Translate(null!)).ThrowExactlyAsync<ArgumentNullException>().Await();
    AssertionExtensions.Should(() => Api.Translate(null!, Cancellation)).ThrowExactlyAsync<TaskCanceledException>().Await();

    using var api = Api;

    var translation = api.Translate(request => request.From("ru").To("en").Text("Привет, мир")).Await();
    translation.Should().NotBeNull().And.BeOfType<Translation>();
    translation.FromLanguage.Should().Be("ru");
    translation.ToLanguage.Should().Be("en");
    translation.Text.Should().Be("Hello world");

    translation = api.Translate(request => request.From("en").To("ru").Text("Hello, world")).Await();
    translation.Should().NotBeNull().And.BeOfType<Translation>();
    translation.FromLanguage.Should().Be("en");
    translation.ToLanguage.Should().Be("ru");
    translation.Text.Should().Be("Привет, мир");
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IApi.Dispose()"/> method.</para>
  /// </summary>
  [Fact]
  public void Dispose_Method()
  {
    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  public void Dispose()
  {
    Api.Dispose();
  }
}