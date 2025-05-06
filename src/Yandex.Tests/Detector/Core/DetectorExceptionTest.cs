using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;
using Yandex.Detector;

namespace Yandex.Tests.Detector;

/// <summary>
///   <para>Tests set for class <see cref="DetectorException"/>.</para>
/// </summary>
public sealed class DetectorExceptionTest : Test
{
  /// <summary>
  ///   <para>Performs testing of class constructor(s).</para>
  /// </summary>
  /// <seealso cref="DetectorException(string, Exception)"/>
  [Fact]
  public void Constructors()
  {
    typeof(DetectorException).Should().BeDerivedFrom<Exception>();

    using (new AssertionScope())
    {
      var exception = new DetectorException();
      exception.InnerException.Should().BeNull();
      exception.Message.Should().NotBeEmpty();
    }

    using (new AssertionScope())
    {
      var inner = new Exception();
      var exception = new DetectorException("message", inner);
      exception.InnerException.Should().NotBeNull().And.BeSameAs(inner);
      exception.Message.Should().Be("message");
    }
  }
}