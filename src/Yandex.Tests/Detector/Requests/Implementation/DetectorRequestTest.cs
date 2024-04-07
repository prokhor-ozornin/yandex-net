using Catharsis.Commons;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;
using Yandex.Detector;

namespace Yandex.Tests.Detector.Requests.Implementation;

/// <summary>
///   <para>Tests set for class <see cref="DetectorRequest"/>.</para>
/// </summary>
public sealed class DetectorRequestTest : UnitTest
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
  ///   <para>Performs testing of <see cref="DetectorRequest.WithHeader"/> method.</para>
  /// </summary>
  [Fact]
  public void Header_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => new DetectorRequest().WithHeader(null, "value")).ThrowExactly<ArgumentNullException>().WithParameterName("name");
      AssertionExtensions.Should(() => new DetectorRequest().WithHeader(string.Empty, "value")).ThrowExactly<ArgumentException>().WithParameterName("name");

      var request = new DetectorRequest();
      
      request.Headers.Should().BeEmpty();

      request.WithHeader("uuid", Guid.Empty).Should().NotBeNull().And.BeSameAs(request);
      request.Headers.Should().ContainSingle();
      request.Headers["uuid"].Should().Be(Guid.Empty);
    }

    return;

    static void Validate()
    {

    }
  }
}