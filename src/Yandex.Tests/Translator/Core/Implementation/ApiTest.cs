using Catharsis.Extensions;
using RestSharp;
using RestSharp.Serializers;
using FluentAssertions;
using Xunit;
using Yandex.Translator;
using System.Configuration;
using Catharsis.Commons;
using FluentAssertions.Execution;
using System.Runtime.Serialization;

namespace Yandex.Tests.Translator;

/// <summary>
///   <para>Tests set for class <see cref="Api"/>.</para>
/// </summary>
public sealed class ApiTests : UnitTest
{
  private IApi Api { get; } = Yandex.Api.Translator().Configure(configurator => configurator.ApiKey(ConfigurationManager.AppSettings["ApiKey"]));

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
    //client.BaseUrl.ToString().Should().Be("https://translate.yandex.net/api/v1.5/tr");
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

      using var api = Api;

      var pairs = api.PairsAsync().ToListAsync().Await();
      pairs.Should().NotBeNullOrEmpty().And.Contain(translation => translation.FromLanguage == "en" && translation.ToLanguage == "ru").And.Contain(translation => translation.FromLanguage == "ru" && translation.ToLanguage == "en");
    }

    return;

    static void Validate()
    {

    }
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

      using var api = Api;

      api.DetectAsync("Hello, world").Should().Be("en");
      api.DetectAsync("Привет, мир").Should().Be("ru");
    }

    return;

    static void Validate()
    {

    }
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

      using var api = Api;

      var translation = api.TranslateAsync(request => request.From("ru").To("en").Text("Привет, мир")).Await();
      translation.Should().NotBeNull().And.BeOfType<Translation>();
      translation.FromLanguage.Should().Be("ru");
      translation.ToLanguage.Should().Be("en");
      translation.Text.Should().Be("Hello world");

      translation = api.TranslateAsync(request => request.From("en").To("ru").Text("Hello, world")).Await();
      translation.Should().NotBeNull().And.BeOfType<Translation>();
      translation.FromLanguage.Should().Be("en");
      translation.ToLanguage.Should().Be("ru");
      translation.Text.Should().Be("Привет, мир");
    }

    return;

    static void Validate()
    {

    }
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
  public override void Dispose() => Api.Dispose();
}