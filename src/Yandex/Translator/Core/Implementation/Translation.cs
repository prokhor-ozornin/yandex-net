using Catharsis.Extensions;

namespace Yandex.Translator;

internal sealed record Translation : ITranslation
{
  public Translation(string fromLanguage, string toLanguage, string text)
  {
    if (fromLanguage is null) throw new ArgumentNullException(nameof(fromLanguage));
    if (fromLanguage.IsEmpty()) throw new ArgumentException(nameof(fromLanguage));
    if (toLanguage is null) throw new ArgumentNullException(nameof(toLanguage));
    if (toLanguage.IsEmpty()) throw new ArgumentException(nameof(toLanguage));

    FromLanguage = fromLanguage;
    ToLanguage = toLanguage;
    Text = text;
  }

  public string FromLanguage { get; }

  public string Text { get; }

  public string ToLanguage { get; }

  public override string ToString() => Text ?? string.Empty;
}