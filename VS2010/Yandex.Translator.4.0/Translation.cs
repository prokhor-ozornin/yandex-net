using System;
using Catharsis.Commons;

namespace Yandex.Translator
{
  internal sealed class Translation : IEquatable<ITranslation>, ITranslation
  {
    private readonly string fromLanguage;
    private readonly string text;
    private readonly string toLanguage;

    public Translation(string fromLanguage, string toLanguage, string text)
    {
      Assertion.NotEmpty(fromLanguage);
      Assertion.NotEmpty(toLanguage);
      Assertion.NotEmpty(text);

      this.fromLanguage = fromLanguage;
      this.toLanguage = toLanguage;
      this.text = text;
    }

    public string FromLanguage
    {
      get { return this.fromLanguage; }
    }

    public string Text
    {
      get { return this.text; }
    }

    public string ToLanguage
    {
      get { return this.toLanguage; }
    }

    public bool Equals(ITranslation other)
    {
      return this.Equality(other, x => x.FromLanguage, x => x.Text, x => x.ToLanguage);
    }

    public override bool Equals(object other)
    {
      return this.Equals(other as ITranslation);
    }

    public override int GetHashCode()
    {
      return this.GetHashCode(x => x.FromLanguage, x => x.Text, x => x.ToLanguage);
    }

    public override string ToString()
    {
      return this.Text;
    }
  }
}