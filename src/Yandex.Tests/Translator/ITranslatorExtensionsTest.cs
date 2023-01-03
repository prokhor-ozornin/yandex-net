using FluentAssertions;
using Xunit;
using Yandex.Translator;

namespace Yandex.Tests.Translator;

/// <summary>
///   <para>Tests set for class <see cref="ITranslatorExtensions"/>.</para>
/// </summary>
public sealed class ITranslatorExtensionsTest
{
  /// <summary>
  ///   <para>Performs testing of <see cref="ITranslatorExtensions.Configure(ITranslator, Action{IApiConfigurator})"/> method.</para>
  /// </summary>
  [Fact]
  public void Configure_Method()
  {
    AssertionExtensions.Should(() => ITranslatorExtensions.Configure(null, _ => { })).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => ITranslatorExtensions.Configure(Yandex.Api.Translator(), null)).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => Yandex.Api.Translator().Configure(_ => {})).ThrowExactly<ArgumentNullException>();

    var translator = Yandex.Api.Translator();
    var api = translator.Configure(configurator => configurator.ApiKey("key"));
    api.Should().NotBeNull().And.NotBeSameAs(translator.Configure(configurator => configurator.ApiKey("key"))).And.BeOfType<Api>();
  }
}