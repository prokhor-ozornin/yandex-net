using Catharsis.Commons;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;
using Yandex.Detector;

namespace Yandex.Tests.Detector;

/// <summary>
///   <para>Tests set for class <see cref="IMobileDetectorExtensions"/>.</para>
/// </summary>
public sealed class IMobileDetectorExtensionsTest : UnitTest
{
  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="IMobileDetectorExtensions.Detect(IMobileDetector, (string Name, object Value)[])"/></description></item>
  ///     <item><description><see cref="IMobileDetectorExtensions.Detect(IMobileDetector, Action{IDetectorRequest})"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void Detect_Methods()
  {
    using (new AssertionScope())
    {
      using var detector = Yandex.Api.Detector();

      AssertionExtensions.Should(() => IMobileDetectorExtensions.Detect(null)).ThrowExactly<ArgumentNullException>().WithParameterName("detector");

    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => IMobileDetectorExtensions.Detect(null, _ => { })).ThrowExactly<ArgumentNullException>().WithParameterName("detector");

    }

    throw new NotImplementedException();

    return;

    static void Validate(IMobileDevice device)
    {
      device.Description.Should().Be("Java MIDP2 (small)");
      device.DeviceClass.Should().Be("midp2ss");
      device.JavaPlatform.Camera.Should().BeTrue();
      device.JavaPlatform.Certificate.Should().BeEmpty();
      device.JavaPlatform.FileSystem.Should().BeTrue();
      device.JavaPlatform.Icon.Height.Should().Be(18);
      device.JavaPlatform.Icon.Width.Should().Be(18);
      device.Name.Should().Be("One Touch C651");
      device.Screen.Height.Should().Be(160);
      device.Screen.Width.Should().Be(128);
      device.Vendor.Should().Be("Alcatel");
    }
  }
}