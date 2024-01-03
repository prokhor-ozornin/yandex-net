using Catharsis.Commons;
using FluentAssertions;
using Xunit;
using Yandex.Detector;

namespace Yandex.Tests.Detector;

/// <summary>
///   <para>Tests set for class <see cref="IYandexApiExtensions"/>.</para>
/// </summary>
public sealed class IYandexApiExtensionsTest : UnitTest
{
  /// <summary>
  ///   <para>Performs testing of <see cref="IYandexApiExtensions.Detector(IYandexApi)"/> method.</para>
  /// </summary>
  [Fact]
  public void Detector_Method()
  {
    AssertionExtensions.Should(() => IYandexApiExtensions.Detector(null)).ThrowExactly<ArgumentNullException>().WithParameterName("api");

    Yandex.Api.Detector().Should().NotBeNull().And.NotBeSameAs(Yandex.Api.Detector()).And.BeOfType<MobileDetector>();
  }
}