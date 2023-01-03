namespace Yandex.Detector;

/// <summary>
///   <para>Represent error that occurs during execution of request to Yandex.Detector service.</para>
/// </summary>
public sealed class DetectorException : Exception
{
  /// <summary>
  ///   <para>Initializes a new instance exception with a specified error message and a reference to the inner exception that is the cause of this exception.</para>
  /// </summary>
  /// <param name="message">The error message that explains the reason for the exception.</param>
  /// <param name="inner">The exception that is the cause of the current exception, or a <c>null</c> reference.</param>
  public DetectorException(string message = null, Exception inner = null) : base(message, inner)
  {
  }
}