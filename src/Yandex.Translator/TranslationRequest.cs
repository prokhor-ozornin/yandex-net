using Catharsis.Commons;

namespace Yandex.Translator
{
    using System.Globalization;

    internal sealed class TranslationRequest : Request, ITranslationRequest
  {
    private string from;
    private string to;

    public ITranslationRequest Format(string format)
    {
      Assertion.NotEmpty(format);

      this.Parameters["format"] = format;
      return this;
    }

    public ITranslationRequest From(string language)
    {
      Assertion.NotEmpty(language);

      this.from = language;
      this.Parameters["lang"] = this.Language;
      return this;
    }

    public ITranslationRequest Text(string text)
    {
      Assertion.NotEmpty(text);

      this.Parameters["text"] = text;
      return this;
    }

    public ITranslationRequest To(string language)
    {
      Assertion.NotEmpty(language);

      this.to = language;
      this.Parameters["lang"] = this.Language;
      return this;
    }

    private string Language
    {
      get
      {
        if (this.from.IsEmpty() && this.to.IsEmpty())
        {
          return null;
        }
        
        if (this.from.IsEmpty())
        {
          return this.to;
        }
        
        if (this.to.IsEmpty())
        {
          return this.from;
        }

        return string.Format(CultureInfo.InvariantCulture, "{0}-{1}", this.from, this.to);
      }
    }
  }
}