using Catharsis.Commons;
using FluentAssertions;
using Xunit;
using Yandex.Translator;

namespace Yandex.Tests.Translator;

/// <summary>
///   <para>Tests set for class <see cref="TranslatorException"/>.</para>
/// </summary>
public sealed class TranslatorExceptionTest : UnitTest
{
  /// <summary>
  ///   <para>Performs testing of class constructor(s).</para>
  /// </summary>
  /// <seealso cref="TranslatorException(IError, Exception)"/>
  [Fact]
  public void Constructors()
  {
    typeof(TranslatorException).Should().BeDerivedFrom<Exception>();

    var exception = new TranslatorException();
    exception.InnerException.Should().BeNull();
    exception.Message.Should().BeNull();

    var inner = new Exception();
    var error = new Error(1, "error");
    exception = new TranslatorException(error, inner);
    exception.InnerException.Should().NotBeNull().And.BeSameAs(inner);
    exception.Message.Should().Be("error");
    exception.Error.Should().NotBeNull().And.BeSameAs(error);
  }
}