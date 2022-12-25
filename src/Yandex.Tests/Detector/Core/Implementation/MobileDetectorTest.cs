using Catharsis.Commons;
using FluentAssertions;
using Xunit;
using Yandex.Detector;

namespace Yandex.Tests.Detector.Core.Implementation;

/// <summary>
///   <para>Tests set for class <see cref="MobileDetector"/>.</para>
/// </summary>
public sealed class MobileDetectorTest
{
  /// <summary>
  ///   <para>Performs testing of <see cref="MobileDetector.Detect(IDictionary{string, object?}, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void Detect_Method()
  {
    AssertionExtensions.Should(() => new MobileDetector().Detect(null!)).ThrowExactlyAsync<ArgumentNullException>().Await();

    AssertionExtensions.Should(() => new MobileDetector().Detect(new Dictionary<string, object?>())).ThrowExactlyAsync<DetectorException>().Await().WithMessage("No HTTP headers were specified").Which.InnerException.Should().BeNull();
    AssertionExtensions.Should(() => new MobileDetector().Detect(new Dictionary<string, object?> {{"user-agent", "invalid"}})).ThrowExactlyAsync<DetectorException>().Await().WithMessage("Unknown user agent and wap profile").Which.InnerException.Should().BeNull();
    AssertionExtensions.Should(() => new MobileDetector().Detect(new Dictionary<string, object?> {{"wap-profile", "invalid"}})).ThrowExactlyAsync<DetectorException>().Await().WithMessage("Failed to understand service's response").WithInnerExceptionExactly<InvalidOperationException>();

    using var detector = new MobileDetector();

    var headers = new Dictionary<string, object?> { { "wap-profile", "http://www-ccpp-mpd.alcatel.com/files/ALCATEL-CTH3_MMS10_1.0.rdf" } };
    var device = detector.Detect(headers).Await();
    device.Should().NotBeNull();
    device.Description.Should().Be("Java MIDP2 (small)");
    device.DeviceClass.Should().Be("midp2ss");
    device.JavaPlatform.Camera.Should().BeTrue();
    device.JavaPlatform.Certificate.Should().BeEmpty();
    device.JavaPlatform.FileSystem.Should().BeTrue();
    device.JavaPlatform.Icon.Height.Should().Be(18);
    device.JavaPlatform.Icon.Width.Should().Be(18);
    device.Name.Should().Be("CTH3");
    device.Screen.Height.Should().Be(160);
    device.Screen.Width.Should().Be(128);
    device.Vendor.Should().Be("Alcatel");

    headers = new Dictionary<string, object?> { { "user-agent", "Alcatel-CTH3/1.0 UP.Browser/6.2.ALCATEL MMP/1.0" } };
    device = detector.Detect(headers).Await();
    device.Should().NotBeNull();
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