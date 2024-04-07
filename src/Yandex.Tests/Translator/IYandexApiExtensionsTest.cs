using Catharsis.Commons;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;
using Yandex.Translator;

namespace Yandex.Tests.Translator;

/// <summary>
///   <para>Tests set for class <see cref="IYandexApiExtensions"/>.</para>
/// </summary>
public sealed class IYandexApiExtensionsTest : UnitTest
{
  /// <summary>
  ///   <para>Performs testing of <see cref="IYandexApiExtensions.Translator(IYandexApi)"/> method.</para>
  /// </summary>
  [Fact]
  public void Translator_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => IYandexApiExtensions.Translator(null)).ThrowExactly<ArgumentNullException>().WithParameterName("api");
    }

    Yandex.Api.Translator().Should().NotBeNull().And.NotBeSameAs(Yandex.Api.Translator());
  }
}