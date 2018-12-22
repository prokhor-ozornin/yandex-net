using System;
using Catharsis.Commons;

namespace Yandex.Translator
{
    using System.Globalization;

    internal sealed class TranslationPair : IEquatable<ITranslationPair>, ITranslationPair
  {
    private readonly string fromLanguage;
    private readonly string toLanguage;

    public TranslationPair(string fromLanguage, string toLanguage)
    {
      Assertion.NotEmpty(fromLanguage);
      Assertion.NotEmpty(toLanguage);

      this.fromLanguage = fromLanguage;
      this.toLanguage = toLanguage;
    }

    public string FromLanguage
    {
      get { return this.fromLanguage; }
    }

    public string ToLanguage
    {
      get { return this.toLanguage; }
    }

    public bool Equals(ITranslationPair other)
    {
      return this.Equality(other, x => x.FromLanguage, x => x.ToLanguage);
    }

    public override bool Equals(object other)
    {
      return this.Equals(other as ITranslationPair);
    }

    public override int GetHashCode()
    {
      return this.GetHashCode(x => x.FromLanguage, x => x.ToLanguage);
    }

    public override string ToString()
    {
      return string.Format(CultureInfo.InvariantCulture, "{0}-{1}", this.FromLanguage, this.ToLanguage);
    }
  }
}