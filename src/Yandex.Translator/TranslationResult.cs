namespace Yandex.Translator
{
  using System;
  using System.Collections.Generic;
  using System.Xml.Serialization;
  using Catharsis.Commons;
  using Newtonsoft.Json;

  /// <summary>
  ///   <para>Represents a result of call to Yandex.Translator service's operation of retrieving supported translations pairs.</para>
  /// </summary>
  [XmlType("Translation")]
  public sealed class TranslationResult
  {
    /// <summary>
    ///   <para>HTTP result status code.</para>
    /// </summary>
    [JsonProperty("code")]
    [XmlAttribute("code")]
    public int Code { get; set; }

    /// <summary>
    ///   <para>Languages pair (for example "en-ru"), representing text's translation direction.</para>
    /// </summary>
    [JsonProperty("lang")]
    [XmlAttribute("lang")]
    public string Language { get; set; }

    /// <summary>
    ///   <para>Collection of translated text fragments.</para>
    /// </summary>
    [JsonProperty("text")]
    [XmlElement("text")]
    public List<string> Lines { get; set; }

    /// <summary>
    ///   <para>Full translated text.</para>
    /// </summary>
    [JsonIgnore]
    [XmlIgnore]
    public string Text
    {
      get { return Lines.Join(string.Empty); }
    }

    /// <summary>
    ///   <para>Creates new result instance.</para>
    /// </summary>
    public TranslationResult()
    {
      Lines = new List<string>();
    }

    /// <summary>
    ///   <para>Creates new result instance.</para>
    /// </summary>
    /// <param name="code">HTTP result status code.</param>
    /// <param name="language">Languages pair, representing text's translation direction.</param>
    /// <param name="text">Translated version of text.</param>
    /// <exception cref="ArgumentNullException">If either <paramref name="language"/> or <paramref name="text"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If either <paramref name="language"/> or <paramref name="text"/> is <see cref="string.Empty"/> string.</exception>
    public TranslationResult(int code, string language, string text) : this()
    {
      Code = code;
      Language = language;
      Lines.Add(text);
    }

    /// <summary>
    ///   <para>Returns a <see cref="string"/> that represents the current <see cref="TranslationResult"/> instance.</para>
    /// </summary>
    /// <returns>A string that represents the current <see cref="TranslationResult"/>.</returns>
    public override string ToString()
    {
      return Text;
    }
  }
}