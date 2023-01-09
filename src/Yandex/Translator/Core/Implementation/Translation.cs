using Catharsis.Extensions;

namespace Yandex.Translator;

internal sealed class Translation : ITranslation
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

  public bool Equals(ITranslation other) => this.Equality(other, nameof(FromLanguage), nameof(ToLanguage), nameof(Text));

  public override bool Equals(object other) => Equals(other as ITranslation);

  public override int GetHashCode() => this.HashCode(nameof(FromLanguage), nameof(Text), nameof(ToLanguage));

  public override string ToString() => Text;
}