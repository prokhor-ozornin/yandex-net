using FluentAssertions;
using Xunit;
using Yandex.Detector;

namespace Yandex.Tests.Detector.Requests;

/// <summary>
///   <para>Tests set for class <see cref="IDetectorRequestExtensions"/>.</para>
/// </summary>
public sealed class IDetectorRequestExtensionsTest
{
  /// <summary>
  ///   <para>Performs testing of <see cref="IDetectorRequestExtensions.OperaMini(IDetectorRequest, string)"/> method.</para>
  /// </summary>
  [Fact]
  public void OperaMini_Method()
  {
    AssertionExtensions.Should(() => IDetectorRequestExtensions.OperaMini(null!, "version")).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => new DetectorRequest().OperaMini(null!)).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => new DetectorRequest().OperaMini(string.Empty)).ThrowExactly<ArgumentException>();

    var request = new DetectorRequest();
    
    request.Headers.Should().BeEmpty();

    request.OperaMini("1.0").Should().NotBeNull().And.BeSameAs(request);

    request.Headers.Should().ContainSingle();
    request.Headers["x-operamini-phone-ua"].Should().Be("1.0");
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IDetectorRequestExtensions.Profile(IDetectorRequest, string)"/> method.</para>
  /// </summary>
  [Fact]
  public void Profile_Method()
  {
    AssertionExtensions.Should(() => IDetectorRequestExtensions.Profile(null!, "profile")).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => new DetectorRequest().Profile(null!)).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => new DetectorRequest().Profile(string.Empty)).ThrowExactly<ArgumentException>();

    var request = new DetectorRequest();

    request.Headers.Should().BeEmpty();

    request.Profile("user").Should().NotBeNull().And.BeSameAs(request);
    request.Headers.Should().HaveCount(3).And.ContainKeys("profile", "wap-profile", "x-wap-profile").And.ContainValues("user");
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IDetectorRequestExtensions.UserAgent(IDetectorRequest, string)"/> method.</para>
  /// </summary>
  [Fact]
  public void UserAgent_Method()
  {
    AssertionExtensions.Should(() => IDetectorRequestExtensions.UserAgent(null!, "userAgent")).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => new DetectorRequest().UserAgent(null!)).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => new DetectorRequest().UserAgent(string.Empty)).ThrowExactly<ArgumentException>();

    var request = new DetectorRequest();

    request.Headers.Should().BeEmpty();

    request.UserAgent("Mozilla/Firefox").Should().NotBeNull().And.BeSameAs(request);
    request.Headers.Should().ContainSingle();
    request.Headers["user-agent"].Should().Be("Mozilla/Firefox");
  }
}