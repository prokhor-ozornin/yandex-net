using System;
using Catharsis.Commons;

namespace Yandex.Translator
{
  /// <summary>
  ///   <para>Represent error that occurs during execution of request to Yandex.Translator service.</para>
  /// </summary>
  public sealed class TranslatorException : Exception
  {
    private readonly IError error;

    /// <summary>
    ///   <para>Initializes a new instance exception with a specified error message and a reference to the inner exception that is the cause of this exception.</para>
    /// </summary>
    /// <param name="error">Detailed error information.</param>
    /// <param name="innerException">The exception that is the cause of the current exception, or a <c>null</c> reference.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="error"/> is a <c>null</c> reference.</exception>
    public TranslatorException(IError error, Exception innerException = null) : base(error != null ? error.Text : string.Empty, innerException)
    {
      Assertion.NotNull(error);

      this.error = error;
    }

    /// <summary>
    ///   <para>Detailed error information</para>
    /// </summary>
    public IError Error
    {
      get { return this.error; }
    }
  }
}