namespace Yandex.Translator
{
  using System;
  using Catharsis.Commons;

  internal sealed class Translation : IEquatable<ITranslation>, ITranslation
  {
    public Translation(string fromLanguage, string toLanguage, string text)
    {
      Assertion.NotEmpty(fromLanguage);
      Assertion.NotEmpty(toLanguage);
      Assertion.NotEmpty(text);

      FromLanguage = fromLanguage;
      ToLanguage = toLanguage;
      Text = text;
    }

    public string FromLanguage { get; private set; }

    public string Text { get; private set; }

    public string ToLanguage { get; private set; }

    public bool Equals(ITranslation other)
    {
      return this.Equality(other, x => x.FromLanguage, x => x.Text, x => x.ToLanguage);
    }

    public override bool Equals(object other)
    {
      return Equals(other as ITranslation);
    }

    public override int GetHashCode()
    {
      return this.GetHashCode(x => x.FromLanguage, x => x.Text, x => x.ToLanguage);
    }

    public override string ToString()
    {
      return Text;
    }
  }
}