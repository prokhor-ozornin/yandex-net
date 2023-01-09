using System.Configuration;
using Catharsis.Extensions;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;
using Yandex.Translator;

namespace Yandex.Tests.Translator;

/// <summary>
///   <para>Tests set for class <see cref="IApiExtensions"/>.</para>
/// </summary>
public sealed class IApiExtensionsTest : UnitTest
{
  private IApi Api { get; } = Yandex.Api.Translator().Configure(configurator => configurator.ApiKey(ConfigurationManager.AppSettings["ApiKey"]));

  /// <summary>
  ///   <para>Performs testing of <see cref="IApiExtensions.TranslateAsync(IApi, Action{ITranslationApiRequest}, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void TranslateAsync_Method()
  {
    AssertionExtensions.Should(() => IApiExtensions.TranslateAsync(null, _ => { })).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("api").Await();
    AssertionExtensions.Should(() => IApiExtensions.TranslateAsync(Api, null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("action").Await();
    AssertionExtensions.Should(() => Api.TranslateAsync(_ => { }, Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();

    var translation = Api.TranslateAsync(request => request.From("ru").To("en").Text("Привет, мир")).Await();
    translation.Should().NotBeNull();
    translation.FromLanguage.Should().Be("ru");
    translation.ToLanguage.Should().Be("en");
    translation.Text.Should().Be("Hello world");
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IApiExtensions.Pairs(IApi)"/> method.</para>
  /// </summary>
  [Fact]
  public void Pairs_Method()
  {
    AssertionExtensions.Should(() => IApiExtensions.Pairs(null)).ThrowExactly<ArgumentNullException>().WithParameterName("api");

    Api.Pairs().Should().NotBeNullOrEmpty().And.HaveCountGreaterThanOrEqualTo(2).And.Contain(translation => translation.FromLanguage == "en" && translation.ToLanguage == "ru").And.Contain(translation => translation.FromLanguage == "ru" && translation.ToLanguage == "en");
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IApiExtensions.Detect(IApi, string)"/> method.</para>
  /// </summary>
  [Fact]
  public void Detect_Method()
  {
    AssertionExtensions.Should(() => IApiExtensions.Detect(null, string.Empty)).ThrowExactly<ArgumentNullException>().WithParameterName("api");
    AssertionExtensions.Should(() => Api.Detect(null)).ThrowExactly<ArgumentNullException>().WithParameterName("text");

    Api.Detect("Hello, world").Should().NotBeNull().And.Be("en");
    Api.Detect("Привет, мир").Should().NotBeNull().And.Be("ru");
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="IApiExtensions.Translate(IApi, ITranslationApiRequest)"/></description></item>
  ///     <item><description><see cref="IApiExtensions.Translate(IApi, Action{ITranslationApiRequest})"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void Translate_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => IApiExtensions.Translate(null, new TranslationApiRequest())).ThrowExactly<ArgumentNullException>().WithParameterName("api");
      AssertionExtensions.Should(() => Api.Translate((ITranslationApiRequest) null)).ThrowExactly<ArgumentNullException>().WithParameterName("request");

      var translation = Api.Translate(new TranslationApiRequest().From("ru").To("en").Text("Привет, мир"));
      translation.Should().NotBeNull().And.BeOfType<Translation>();
      translation.FromLanguage.Should().Be("ru");
      translation.ToLanguage.Should().Be("en");
      translation.Text.Should().Be("Hello world");
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => IApiExtensions.Translate(null, _ => {})).ThrowExactly<ArgumentNullException>().WithParameterName("api");
      AssertionExtensions.Should(() => Api.Translate((Action<ITranslationApiRequest>) null)).ThrowExactly<ArgumentNullException>().WithParameterName("action");

      var translation = Api.Translate(request => request.From("ru").To("en").Text("Привет, мир"));
      translation.Should().NotBeNull().And.BeOfType<Translation>();
      translation.FromLanguage.Should().Be("ru");
      translation.ToLanguage.Should().Be("en");
      translation.Text.Should().Be("Hello world");
    }
  }

  public override void Dispose()
  {
    Api.Dispose();
  }
}