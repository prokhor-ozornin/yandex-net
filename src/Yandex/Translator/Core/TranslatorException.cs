namespace Yandex.Translator;

/// <summary>
///   <para>Represent error that occurs during execution of request to Yandex.Translator service.</para>
/// </summary>
public sealed class TranslatorException : Exception
{
  /// <summary>
  ///   <para>Initializes a new instance exception with a specified error message and a reference to the inner exception that is the cause of this exception.</para>
  /// </summary>
  /// <param name="error">Detailed error information.</param>
  /// <param name="inner">The exception that is the cause of the current exception, or a <c>null</c> reference.</param>
  public TranslatorException(IError error = null, Exception inner = null) : base(error is not null ? error.Text : string.Empty, inner) => Error = error;

  /// <summary>
  ///   <para>Detailed error information</para>
  /// </summary>
  public IError Error { get; }
}