namespace Yandex.Translator
{
  using System;
  using System.Globalization;
  using Catharsis.Commons;

  internal sealed class TranslationPair : IEquatable<ITranslationPair>, ITranslationPair
  {
    public TranslationPair(string fromLanguage, string toLanguage)
    {
      Assertion.NotEmpty(fromLanguage);
      Assertion.NotEmpty(toLanguage);

      FromLanguage = fromLanguage;
      ToLanguage = toLanguage;
    }

    public string FromLanguage { get; private set; }

    public string ToLanguage { get; }

    public bool Equals(ITranslationPair other)
    {
      return this.Equality(other, x => x.FromLanguage, x => x.ToLanguage);
    }

    public override bool Equals(object other)
    {
      return Equals(other as ITranslationPair);
    }

    public override int GetHashCode()
    {
      return this.GetHashCode(x => x.FromLanguage, x => x.ToLanguage);
    }

    public override string ToString()
    {
      return string.Format(CultureInfo.InvariantCulture, "{0}-{1}", FromLanguage, ToLanguage);
    }
  }
}