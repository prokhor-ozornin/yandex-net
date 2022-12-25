using System.Configuration;
using Catharsis.Commons;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;
using Yandex.Translator;

namespace Yandex.Tests.Translator;

/// <summary>
///   <para>Tests set for class <see cref="IApiExtensions"/>.</para>
/// </summary>
public sealed class IApiExtensionsTest : IDisposable
{
  private IApi Api { get; } = Yandex.Api.Translator().Configure(configurator => configurator.ApiKey(ConfigurationManager.AppSettings["ApiKey"]));

  private CancellationToken Cancellation { get; } = new(true);

  /// <summary>
  ///   <para>Performs testing of <see cref="IApiExtensions.Pairs(IApi, out IEnumerable{ITranslationPair}?, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void Pairs_Method()
  {
    AssertionExtensions.Should(() => IApiExtensions.Pairs(null!, out _)).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => Api.Pairs(out _, Cancellation)).ThrowExactly<TaskCanceledException>();

    Api.Pairs(out var pairs).Should().BeTrue();
    pairs.Should().NotBeNullOrEmpty().And.HaveCountGreaterThanOrEqualTo(2).And.Contain(translation => translation.FromLanguage == "en" && translation.ToLanguage == "ru").And.Contain(translation => translation.FromLanguage == "ru" && translation.ToLanguage == "en");
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IApiExtensions.Detect(IApi, out string?, string, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void Detect_Method()
  {
    AssertionExtensions.Should(() => IApiExtensions.Detect(null!, out _, "text")).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => Api.Detect(out _, null!)).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => Api.Detect(out _, string.Empty)).ThrowExactly<ArgumentException>();
    AssertionExtensions.Should(() => Api.Detect(out _, "text", Cancellation)).ThrowExactly<TaskCanceledException>();

    using var api = Api;
    
    api.Detect(out var language, "Hello, world").Should().BeTrue();
    language.Should().NotBeNull().And.Be("en");

    api.Detect(out language, "Привет, мир").Should().BeTrue();
    language.Should().NotBeNull().And.Be("ru");
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="IApiExtensions.Translate(IApi, Action{ITranslationApiRequest}, CancellationToken)"/></description></item>
  ///     <item><description><see cref="IApiExtensions.Translate(IApi, Action{ITranslationApiRequest}, out ITranslation?, CancellationToken)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void Translate_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => IApiExtensions.Translate(null!, _ => {})).ThrowExactlyAsync<ArgumentNullException>().Await();
      AssertionExtensions.Should(() => IApiExtensions.Translate(Api, null!)).ThrowExactlyAsync<ArgumentNullException>().Await();
      AssertionExtensions.Should(() => Api.Translate(_ => {}, Cancellation)).ThrowExactlyAsync<TaskCanceledException>().Await();

      using var api = Api;

      var translation = api.Translate(request => request.From("ru").To("en").Text("Привет, мир")).Await();
      translation.Should().NotBeNull();
      translation.FromLanguage.Should().Be("ru");
      translation.ToLanguage.Should().Be("en");
      translation.Text.Should().Be("Hello world");
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => IApiExtensions.Translate(null!, _ => {}, out _)).ThrowExactly<ArgumentNullException>();
      AssertionExtensions.Should(() => Api.Translate(null!, out _)).ThrowExactly<ArgumentNullException>();
      AssertionExtensions.Should(() => Api.Translate(_ => {}, out _, Cancellation)).ThrowExactly<TaskCanceledException>();

      using var api = Api;

      api.Translate(request => request.From("ru").To("en").Text("Привет, мир"), out var translation).Should().BeTrue();
      translation.Should().NotBeNull().And.BeOfType<Translation>();
      translation.FromLanguage.Should().Be("ru");
      translation.ToLanguage.Should().Be("en");
      translation.Text.Should().Be("Hello world");
    }
  }

  public void Dispose()
  {
    Api.Dispose();
  }
}