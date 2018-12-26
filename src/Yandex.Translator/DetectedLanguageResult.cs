namespace Yandex.Translator
{
  using System;
  using System.Xml.Serialization;
  using Catharsis.Commons;
  using Newtonsoft.Json;

  /// <summary>
  ///   <para>Represents a result of call to Yandex.Translator service's operation of language detection.</para>
  /// </summary>
  [XmlType("DetectedLang")]
  public sealed class DetectedLanguageResult
  {
    /// <summary>
    ///   <para>HTTP result status code.</para>
    /// </summary>
    [JsonProperty("code")]
    [XmlAttribute("code")]
    public int Code { get; set; }

    /// <summary>
    ///   <para>Detected text's language.</para>
    /// </summary>
    [JsonProperty("lang")]
    [XmlAttribute("lang")]
    public string Language { get; set; }

    /// <summary>
    ///   <para>Creates new result instance.</para>
    /// </summary>
    public DetectedLanguageResult()
    {
    }

    /// <summary>
    ///   <para>Creates new result instance.</para>
    /// </summary>
    /// <param name="code">HTTP status code.</param>
    /// <param name="language">Detected text's language.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="language"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <paramref name="language"/> is <see cref="string.Empty"/> string.</exception>
    public DetectedLanguageResult(int code, string language)
    {
      Assertion.NotEmpty(language);

      Code = code;
      Language = language;
    }
  }
}