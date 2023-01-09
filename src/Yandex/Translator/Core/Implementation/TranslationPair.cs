using Catharsis.Extensions;

namespace Yandex.Translator;

internal sealed class TranslationPair : ITranslationPair
{
  public TranslationPair(string fromLanguage, string toLanguage)
  {
    if (fromLanguage is null) throw new ArgumentNullException(nameof(fromLanguage));
    if (fromLanguage.IsEmpty()) throw new ArgumentException(nameof(fromLanguage));
    if (toLanguage is null) throw new ArgumentNullException(nameof(toLanguage));
    if (toLanguage.IsEmpty()) throw new ArgumentException(nameof(toLanguage));

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