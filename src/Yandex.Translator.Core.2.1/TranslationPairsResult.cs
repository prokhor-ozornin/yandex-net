using System.Collections.Generic;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace Yandex.Translator
{
  /// <summary>
  ///   <para>Represents a result of call to Yandex.Translator service's operation of retrieving supported translations pairs.</para>
  /// </summary>
  [XmlType("Langs")]
  public sealed class TranslationPairsResult
  {
    /// <summary>
    ///   <para>Collection of supported translations pairs (directions).</para>
    /// </summary>
    [JsonProperty("dirs")]
    [XmlArray("dirs")]
    [XmlArrayItem("string")]
    public List<string> Pairs { get; set; }

    /// <summary>
    ///   <para>Creates new result instance.</para>
    /// </summary>
    public TranslationPairsResult()
    {
      this.Pairs = new List<string>();
    }
  }
}