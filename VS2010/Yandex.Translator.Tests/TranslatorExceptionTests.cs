using System;
using Xunit;

namespace Yandex.Translator
{
  /// <summary>
  ///   <para>Tests set for class <see cref="TranslatorException"/>.</para>
  /// </summary>
  public sealed class TranslatorExceptionTests
  {
    /// <summary>
    ///   <para>Performs testing of class constructor(s).</para>
    /// </summary>
    /// <seealso cref="TranslatorException(Error, Exception)"/>
    [Fact]
    public void Constructors()
    {
      Assert.Throws<ArgumentNullException>(() => new TranslatorException(null));

      var innerException = new Exception();
      var error = new Error(1, "text");
      var exception = new TranslatorException(error, innerException);
      Assert.True(ReferenceEquals(exception.InnerException, innerException));
      Assert.Equal("text", exception.Message);
      Assert.True(ReferenceEquals(error, exception.Error));
    }
  }
}