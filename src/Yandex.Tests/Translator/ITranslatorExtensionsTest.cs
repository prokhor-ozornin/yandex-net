using Catharsis.Commons;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;
using Yandex.Translator;

namespace Yandex.Tests.Translator;

/// <summary>
///   <para>Tests set for class <see cref="ITranslatorExtensions"/>.</para>
/// </summary>
public sealed class ITranslatorExtensionsTest : UnitTest
{
  /// <summary>
  ///   <para>Performs testing of <see cref="ITranslatorExtensions.Configure(ITranslator, Action{IApiConfigurator})"/> method.</para>
  /// </summary>
  [Fact]
  public void Configure_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ITranslatorExtensions.Configure(null, _ => { })).ThrowExactly<ArgumentNullException>().WithParameterName("translator");
      AssertionExtensions.Should(() => ITranslatorExtensions.Configure(Yandex.Api.Translator(), null)).ThrowExactly<ArgumentNullException>().WithParameterName("action");

      Validate(configurator => configurator.ApiKey("key"), Yandex.Api.Translator());
    }

    return;

    static void Validate(Action<IApiConfigurator> configurator, ITranslator translator) => translator.Configure(configurator).Should().BeOfType<Api>().And.NotBeSameAs(configurator);
  }
}