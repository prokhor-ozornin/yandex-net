using Catharsis.Commons;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;
using Yandex.Detector;

namespace Yandex.Tests.Detector.Requests;

/// <summary>
///   <para>Tests set for class <see cref="IDetectorRequestExtensions"/>.</para>
/// </summary>
public sealed class IDetectorRequestExtensionsTest : UnitTest
{
  /// <summary>
  ///   <para>Performs testing of <see cref="IDetectorRequestExtensions.OperaMini(IDetectorRequest, string)"/> method.</para>
  /// </summary>
  [Fact]
  public void OperaMini_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => IDetectorRequestExtensions.OperaMini(null, "version")).ThrowExactly<ArgumentNullException>().WithParameterName("request");
      AssertionExtensions.Should(() => new DetectorRequest().OperaMini(null)).ThrowExactly<ArgumentNullException>().WithParameterName("version");
      AssertionExtensions.Should(() => new DetectorRequest().OperaMini(string.Empty)).ThrowExactly<ArgumentException>().WithParameterName("version");

      var request = new DetectorRequest();
      
      request.Headers.Should().BeEmpty();

      request.OperaMini("1.0").Should().NotBeNull().And.BeSameAs(request);

      request.Headers.Should().ContainSingle();
      request.Headers["x-operamini-phone-ua"].Should().Be("1.0");
    }

    return;

    static void Validate()
    {

    }
  } 

  /// <summary>
  ///   <para>Performs testing of <see cref="IDetectorRequestExtensions.Profile(IDetectorRequest, string)"/> method.</para>
  /// </summary>
  [Fact]
  public void Profile_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => IDetectorRequestExtensions.Profile(null, "profile")).ThrowExactly<ArgumentNullException>().WithParameterName("request");
      AssertionExtensions.Should(() => new DetectorRequest().Profile(null)).ThrowExactly<ArgumentNullException>().WithParameterName("profile");
      AssertionExtensions.Should(() => new DetectorRequest().Profile(string.Empty)).ThrowExactly<ArgumentException>().WithParameterName("profile");

      var request = new DetectorRequest();

      request.Headers.Should().BeEmpty();

      request.Profile("user").Should().NotBeNull().And.BeSameAs(request);
      request.Headers.Should().HaveCount(3).And.ContainKeys("profile", "wap-profile", "x-wap-profile").And.ContainValues("user");
    }

    return;

    static void Validate()
    {

    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IDetectorRequestExtensions.UserAgent(IDetectorRequest, string)"/> method.</para>
  /// </summary>
  [Fact]
  public void UserAgent_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => IDetectorRequestExtensions.UserAgent(null, "userAgent")).ThrowExactly<ArgumentNullException>().WithParameterName("request");
      AssertionExtensions.Should(() => new DetectorRequest().UserAgent(null)).ThrowExactly<ArgumentNullException>().WithParameterName("userAgent");
      AssertionExtensions.Should(() => new DetectorRequest().UserAgent(string.Empty)).ThrowExactly<ArgumentException>().WithParameterName("userAgent");

      var request = new DetectorRequest();

      request.Headers.Should().BeEmpty();

      request.UserAgent("Mozilla/Firefox").Should().NotBeNull().And.BeSameAs(request);
      request.Headers.Should().ContainSingle();
      request.Headers["user-agent"].Should().Be("Mozilla/Firefox");
    }

    return;

    static void Validate()
    {

    }
  }
}