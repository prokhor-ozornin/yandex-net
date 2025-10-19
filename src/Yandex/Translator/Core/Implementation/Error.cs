using System.Runtime.Serialization;
using Catharsis.Extensions;

namespace Yandex.Translator;

/// <summary>
///   <para></para>
/// </summary>
[DataContract(Name = "Error")]
public sealed class Error : IError
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  [DataMember(Name = "code", IsRequired = true)]
  public int Code { get; init; }

  /// <summary>
  ///   <para></para>
  /// </summary>
  [DataMember(Name = "text", IsRequired = true)]
  public string Text { get; init; }

  /// <summary>
  ///   <para></para>
  /// </summary>
  public Error()
  {
  }
  
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="code"></param>
  /// <param name="text"></param>
  public Error(int code, string text)
  {
    Code = code;
    Text = text;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="other"></param>
  /// <returns></returns>
  public int CompareTo(IError other) => Code.CompareTo(other?.Code);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="other"></param>
  /// <returns></returns>
  public bool Equals(IError other) => this.Equality(other, nameof(Code));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="other"></param>
  /// <returns></returns>
  public override bool Equals(object other) => Equals(other as IError);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <returns></returns>
  public override int GetHashCode() => this.HashCode(nameof(Code));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <returns></returns>
  public override string ToString() => Text ?? string.Empty;
}