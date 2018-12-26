namespace Yandex.Translator
{
  using System;
  using System.Xml.Serialization;
  using Catharsis.Commons;
  using Newtonsoft.Json;

  /// <summary>
  ///   <para>Yandex Translator API call error.</para>
  /// </summary>
  [XmlType("Error")]
  public sealed class Error : IComparable<IError>, IEquatable<IError>, IError
  {
    /// <summary>
    ///   <para>Creates new error.</para>
    /// </summary>
    public Error()
    {
    }

    /// <summary>
    ///   <para>Creates new error.</para>
    /// </summary>
    /// <param name="code">Numeric code of error.</param>
    /// <param name="text">Text description of error.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="text"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <paramref name="text"/> is <see cref="string.Empty"/> string.</exception>
    public Error(int code, string text)
    {
      Assertion.NotEmpty(text);

      Code = code;
      Text = text;
    }

    /// <summary>
    ///   <para>Numeric code of error.</para>
    /// </summary>
    [JsonProperty("code")]
    [XmlElement("code")]
    public int Code { get; set; }

    /// <summary>
    ///   <para>Text description of error.</para>
    /// </summary>
    [JsonProperty("text")]
    [XmlElement("text")]
    public string Text { get; set; }

    /// <summary>
    ///   <para>Compares the current <see cref="IError"/> instance with another.</para>
    /// </summary>
    /// <returns>A value that indicates the relative order of the instances being compared.</returns>
    /// <param name="other">The <see cref="IError"/> to compare with this instance.</param>
    public int CompareTo(IError other)
    {
      return Code.CompareTo(other.Code);
    }

    /// <summary>
    ///   <para>Determines whether two <see cref="IError"/> instances are equal.</para>
    /// </summary>
    /// <param name="other">The instance to compare with the current one.</param>
    /// <returns><c>true</c> if specified instance is equal to the current, <c>false</c> otherwise.</returns>
    public bool Equals(IError other)
    {
      return this.Equality(other, error => error.Code);
    }

    /// <summary>
    ///   <para>Determines whether the specified <see cref="object"/> is equal to the current <see cref="object"/>.</para>
    /// </summary>
    /// <param name="other">The object to compare with the current object.</param>
    /// <returns><c>true</c> if the specified object is equal to the current object, <c>false</c>.</returns>
    public override bool Equals(object other)
    {
      return Equals(other as IError);
    }

    /// <summary>
    ///   <para>Returns hash code for the current object.</para>
    /// </summary>
    /// <returns>Hash code of current instance.</returns>
    public override int GetHashCode()
    {
      return this.GetHashCode(error => error.Code);
    }

    /// <summary>
    ///   <para>Returns a <see cref="string"/> that represents the current <see cref="IError"/> instance.</para>
    /// </summary>
    /// <returns>A string that represents the current <see cref="IError"/>.</returns>
    public override string ToString()
    {
      return Text;
    }
  }
}