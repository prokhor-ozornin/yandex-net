using System;
using System.Xml.Serialization;
using Catharsis.Commons;
using Newtonsoft.Json;

namespace Yandex.Translator
{
  /// <summary>
  ///   <para>Yandex Translator API call error.</para>
  /// </summary>
  [XmlType("Error")]
  public sealed class Error : IComparable<Error>, IEquatable<Error>
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

      this.Code = code;
      this.Text = text;
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
    ///   <para>Compares the current error with another.</para>
    /// </summary>
    /// <returns>A value that indicates the relative order of the objects being compared.</returns>
    /// <param name="other">The <see cref="Error"/> to compare with this instance.</param>
    public int CompareTo(Error other)
    {
      return this.Code.CompareTo(other.Code);
    }

    /// <summary>
    ///   <para>Determines whether two errors instances are equal.</para>
    /// </summary>
    /// <param name="other">The error to compare with the current one.</param>
    /// <returns><c>true</c> if specified error is equal to the current, <c>false</c> otherwise.</returns>
    public bool Equals(Error other)
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
      return this.Equals(other as Error);
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
    ///   <para>Returns a <see cref="string"/> that represents the current entity.</para>
    /// </summary>
    /// <returns>A string that represents the current entity.</returns>
    public override string ToString()
    {
      return this.Text;
    }
  }
}