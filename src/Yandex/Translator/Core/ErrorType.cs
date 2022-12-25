namespace Yandex.Translator;

/// <summary>
///   <para>Type of errors, corresponding to HTTP status codes.</para>
/// </summary>
public enum ErrorType
{
  /// <summary>
  ///   <para>Invalid API key.</para>
  /// </summary>
  ApiKeyInvalid = 401,

  /// <summary>
  ///   <para>API key is blocked.</para>
  /// </summary>
  ApiKeyBlocked = 402,

  /// <summary>
  ///   <para>Exceeded daily requests limit.</para>
  /// </summary>
  DailyRequestsLimitExceeded = 403,

  /// <summary>
  ///   <para>Daily characters limit was exceeded</para>
  /// </summary>
  DailyCharactersLimitExceeded = 404,

  /// <summary>
  ///   <para>Translation text is too long.</para>
  /// </summary>
  TextTooLong = 413,

  /// <summary>
  ///   <para>Translation text is unprocessable.</para>
  /// </summary>
  UnprocessableText = 422,

  /// <summary>
  ///   <para>Translation language is not supported.</para>
  /// </summary>
  LanguageNotSupported = 501
}