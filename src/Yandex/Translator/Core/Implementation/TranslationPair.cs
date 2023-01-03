using Catharsis.Commons;

namespace Yandex.Translator;

internal sealed class TranslationPair : ITranslationPair
{
  public TranslationPair(string fromLanguage, string toLanguage)
  {
    FromLanguage = fromLanguage;
    ToLanguage = toLanguage;
  }

  public string FromLanguage { get; }

  public string ToLanguage { get; }

  public bool Equals(ITranslationPair other) => this.Equality(other, nameof(FromLanguage), nameof(ToLanguage));

  public override bool Equals(object other) => Equals(other as ITranslationPair);

  public override int GetHashCode() => this.HashCode(nameof(FromLanguage), nameof(ToLanguage));

  public override string ToString() => $"{FromLanguage}-{ToLanguage}";
}