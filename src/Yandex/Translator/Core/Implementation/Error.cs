using System.Runtime.Serialization;
using Catharsis.Extensions;

namespace Yandex.Translator;

public sealed class Error : IError
{
  public int Code { get; }

  public string Text { get; }

  public Error(int code, string text)
  {
    Code = code;
    Text = text;
  }

  public Error(Info info)
  {
    Code = info.Code ?? 0;
    Text = info.Text ?? string.Empty;
  }

  public Error(object info) : this(new Info().SetState(info)) {}

  public int CompareTo(IError other) => Code.CompareTo(other?.Code);

  public bool Equals(IError other) => this.Equality(other, nameof(Code));

  public override bool Equals(object other) => Equals(other as IError);

  public override int GetHashCode() => this.HashCode(nameof(Code));

  public override string ToString() => Text;

  [DataContract(Name = "Error")]
  public sealed record Info : IResultable<IError>
  {
    [DataMember(Name = "code", IsRequired = true)]
    public int? Code { get; init; }

    [DataMember(Name = "text", IsRequired = true)]
    public string Text { get; init; }

    public IError ToResult() => new Error(this);
  }
}