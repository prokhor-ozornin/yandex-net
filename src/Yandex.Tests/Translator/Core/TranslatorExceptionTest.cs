using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;
using Yandex.Translator;

namespace Yandex.Tests.Translator;

/// <summary>
///   <para>Tests set for class <see cref="TranslatorException"/>.</para>
/// </summary>
public sealed class TranslatorExceptionTest : Test
{
  /// <summary>
  ///   <para>Performs testing of class constructor(s).</para>
  /// </summary>
  /// <seealso cref="TranslatorException(IError, Exception)"/>
  [Fact]
  public void Constructors()
  {
    typeof(TranslatorException).Should().BeDerivedFrom<Exception>();

    using (new AssertionScope())
    {
      var exception = new TranslatorException();
      exception.InnerException.Should().BeNull();
      exception.Message.Should().BeEmpty();
    }

    using (new AssertionScope())
    {
      var inner = new Exception();
      var error = new Error(1, "error");
      var exception = new TranslatorException(error, inner);
      exception.InnerException.Should().BeSameAs(inner);
      exception.Message.Should().Be("error");
      exception.Error.Should().BeSameAs(error);
    }
  }
}