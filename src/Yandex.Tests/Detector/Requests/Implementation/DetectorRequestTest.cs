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
    typeof(DetectorRequest).Should().BeDerivedFrom<object>().And.Implement<IDetectorRequest>();

    var builder = new DetectorRequest();
    builder.Headers.Should().BeEmpty();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DetectorRequest.WithHeader(string, object)"/> method.</para>
  /// </summary>
  [Fact]
  public void WithHeader_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => new DetectorRequest().WithHeader(null, "value")).ThrowExactly<ArgumentNullException>().WithParameterName("name");
      AssertionExtensions.Should(() => new DetectorRequest().WithHeader(string.Empty, "value")).ThrowExactly<ArgumentException>().WithMessage("name");

      Validate("id", Guid.NewGuid(), new DetectorRequest());
    }

    return;

    static void Validate(string name, object value, IDetectorRequest request) => request.WithHeader(name, value).Should().BeSameAs(request).And.BeOfType<DetectorRequest>().Which.Headers[name].Should().Be(value);
  }
}