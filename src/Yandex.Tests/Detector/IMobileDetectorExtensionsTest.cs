using Catharsis.Commons;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;
using Yandex.Detector;

namespace Yandex.Tests.Detector;

/// <summary>
///   <para>Tests set for class <see cref="IMobileDetectorExtensions"/>.</para>
/// </summary>
public sealed class IMobileDetectorExtensionsTest
{
  private CancellationToken Cancellation { get; } = new(true);

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="IMobileDetectorExtensions.Detect(IMobileDetector, CancellationToken, (string Name, object? Value)[])"/></description></item>
  ///     <item><description><see cref="IMobileDetectorExtensions.Detect(IMobileDetector, Action{IDetectorRequest}, CancellationToken)"/></description></item>
  ///     <item><description><see cref="IMobileDetectorExtensions.Detect(IMobileDetector, out IMobileDevice?, CancellationToken, (string Name, object? Value)[])"/></description></item>
  ///     <item><description><see cref="IMobileDetectorExtensions.Detect(IMobileDetector, out IMobileDevice?, Action{IDetectorRequest}, CancellationToken)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void Detect_Methods()
  {
    void Validate(IMobileDevice device)
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

    using (new AssertionScope())
    {
      using var detector = Yandex.Api.Detector();

      AssertionExtensions.Should(() => IMobileDetectorExtensions.Detect(null!)).ThrowExactlyAsync<ArgumentNullException>().Await();
      AssertionExtensions.Should(() => IMobileDetectorExtensions.Detect(null!, _ => {})).ThrowExactlyAsync<ArgumentNullException>().Await();
      AssertionExtensions.Should(() => IMobileDetectorExtensions.Detect(new MobileDetector(), null!)).ThrowExactlyAsync<ArgumentNullException>().Await();
      AssertionExtensions.Should(() => IMobileDetectorExtensions.Detect(null!, out _)).ThrowExactly<ArgumentNullException>();
      AssertionExtensions.Should(() => IMobileDetectorExtensions.Detect(null!, out _, _ => {})).ThrowExactly<ArgumentNullException>();

      AssertionExtensions.Should(() => detector.Detect(out _, null!)).ThrowExactly<ArgumentNullException>();
      AssertionExtensions.Should(() => detector.Detect(out _, null!)).ThrowExactly<ArgumentNullException>();
      AssertionExtensions.Should(() => detector.Detect(_ => { })).ThrowExactlyAsync<DetectorException>().Await().WithMessage("No HTTP headers were specified").Which.InnerException.Should().BeNull();
      AssertionExtensions.Should(() => detector.Detect(request => request.Profile("invalid"))).ThrowExactlyAsync<DetectorException>().Await().WithMessage("Failed to understand service's response").WithInnerExceptionExactly<InvalidOperationException>();
      AssertionExtensions.Should(() => detector.Detect(request => request.UserAgent("invalid"))).ThrowExactlyAsync<DetectorException>().Await().WithMessage("Unknown user agent and wap profile").Which.InnerException.Should().BeNull();
      AssertionExtensions.Should(() => detector.Detect(Cancellation)).ThrowExactlyAsync<TaskCanceledException>().Await();
      AssertionExtensions.Should(() => detector.Detect(_ => { }, Cancellation)).ThrowExactlyAsync<TaskCanceledException>().Await();
      AssertionExtensions.Should(() => detector.Detect(out _, Cancellation)).ThrowExactly<TaskCanceledException>();
      AssertionExtensions.Should(() => detector.Detect(out _, _ => { }, Cancellation)).ThrowExactly<TaskCanceledException>();

      const string userAgent = "Alcatel-CTH3/1.0 UP.Browser/6.2.ALCATEL MMP/1.0";

      var device = detector.Detect(request => request.UserAgent(userAgent)).Await();

      device = detector.Detect(default, (Name: "user-agent", Value: userAgent)).Await();
      Validate(device);

      device = detector.Detect(request => request.UserAgent(userAgent)).Await();
      Validate(device);

      detector.Detect(out device).Should().BeTrue();
      device.Should().NotBeNull();
      Validate(device!);

      detector.Detect(out device, request => request.UserAgent(userAgent)).Should().BeTrue();
      device.Should().NotBeNull();
      Validate(device!);
    }
  }
}