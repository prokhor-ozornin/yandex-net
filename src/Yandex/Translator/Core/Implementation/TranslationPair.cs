using Catharsis.Extensions;

namespace Yandex.Translator;

internal sealed record TranslationPair : ITranslationPair
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

  public override string ToString() => $"{FromLanguage}-{ToLanguage}";
}