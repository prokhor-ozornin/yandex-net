using Catharsis.Extensions;
using Catharsis.Fixture;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;
using Yandex.Detector;

namespace Yandex.Tests.Detector;

/// <summary>
///   <para>Tests set for class <see cref="IDetectorRequestExtensions"/>.</para>
/// </summary>
/// <seealso cref="IDetectorRequestExtensions"/>
public sealed class IDetectorRequestExtensionsTest : Test
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
      AssertionExtensions.Should(() => new DetectorRequest().OperaMini(string.Empty)).ThrowExactly<ArgumentException>().WithMessage("version");

      Test("1.0", Fixture<IDetectorRequest>.Create());
    }

    return;

    static void Test(string version, IDetectorRequest request) => request.OperaMini(version).Should().BeSameAs(request).And.BeOfType<DetectorRequest>().Which.Headers.ToValueTuple().Should().Equal(("x-operamini-phone-ua", version));
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
      AssertionExtensions.Should(() => new DetectorRequest().Profile(string.Empty)).ThrowExactly<ArgumentException>().WithMessage("profile");

      Test("user", Fixture<IDetectorRequest>.Create());
    }

    return;

    static void Test(string profile, IDetectorRequest request) => request.Profile(profile).Should().BeSameAs(request).And.BeOfType<DetectorRequest>().Which.Headers.Should().HaveCount(3).And.ContainKeys("profile", "wap-profile", "x-wap-profile").And.ContainValues("user");
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
      AssertionExtensions.Should(() => new DetectorRequest().UserAgent(string.Empty)).ThrowExactly<ArgumentException>().WithMessage("userAgent");

      Test("Mozilla/Firefox", Fixture<IDetectorRequest>.Create());
    }

    return;

    static void Test(string userAgent, IDetectorRequest request) => request.UserAgent(userAgent).Should().BeSameAs(request).And.BeOfType<DetectorRequest>().Which.Headers.ToValueTuple().Should().Equal(("user-agent", userAgent));
  }
}