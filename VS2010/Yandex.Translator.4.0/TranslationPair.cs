using System;
using Catharsis.Commons;

namespace Yandex.Translator
{
  internal sealed class TranslationPair : IEquatable<TranslationPair>, ITranslationPair
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

    public bool Equals(TranslationPair other)
    {
      return this.Equality(other, x => x.FromLanguage, x => x.ToLanguage);
    }

    public override bool Equals(object other)
    {
      return this.Equals(other as TranslationPair);
    }

    public override int GetHashCode()
    {
      return this.GetHashCode(x => x.FromLanguage, x => x.ToLanguage);
    }

    public override string ToString()
    {
      return "{0}-{1}".FormatSelf(this.FromLanguage, this.ToLanguage);
    }
  }
}