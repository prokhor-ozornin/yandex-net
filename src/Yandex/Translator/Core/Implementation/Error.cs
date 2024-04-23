using System.Runtime.Serialization;
using Catharsis.Extensions;

namespace Yandex.Translator;

[DataContract(Name = "Error")]
public sealed class Error : IError
{
  [DataMember(Name = "code", IsRequired = true)]
  public int Code { get; init; }

  [DataMember(Name = "text", IsRequired = true)]
  public string Text { get; init; }

  public Error()
  {
  }
  
  public Error(int code, string text)
  {
    Code = code;
    Text = text;
  }

  public int CompareTo(IError other) => Code.CompareTo(other?.Code);

  public bool Equals(IError other) => this.Equality(other, nameof(Code));

  public override bool Equals(object other) => Equals(other as IError);

  public override int GetHashCode() => this.HashCode(nameof(Code));

  public override string ToString() => Text ?? string.Empty;
}