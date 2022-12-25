using FluentAssertions;
using Xunit;
using Yandex.Detector;

namespace Yandex.Tests.Detector.Requests.Implementation;

/// <summary>
///   <para>Tests set for class <see cref="DetectorRequest"/>.</para>
/// </summary>
public sealed class DetectorRequestTest
{
  /// <summary>
  ///   <para>Performs testing of class constructor(s).</para>
  /// </summary>
  /// <seealso cref="DetectorRequest()"/>
  [Fact]
  public void Constructors()
  {
    var builder = new DetectorRequest();
    builder.Headers.Should().NotBeNullOrEmpty();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DetectorRequest.Header(string, object)"/> method.</para>
  /// </summary>
  [Fact]
  public void Header_Method()
  {
    AssertionExtensions.Should(() => new DetectorRequest().Header(null!, "value")).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => new DetectorRequest().Header("name", null!)).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => new DetectorRequest().Header(string.Empty, "value")).ThrowExactly<ArgumentException>();

    var request = new DetectorRequest();
    
    request.Headers.Should().BeEmpty();

    request.Header("uuid", Guid.Empty).Should().NotBeNull().And.BeSameAs(request);
    request.Headers.Should().ContainSingle();
    request.Headers["uuid"].Should().Be(Guid.Empty);
  }
}