namespace Yandex.Translator
{
  using System.Globalization;
  using Catharsis.Commons;

  internal sealed class TranslationRequest : Request, ITranslationRequest
  {
    private string from;
    private string to;

    public ITranslationRequest Format(string format)
    {
      Assertion.NotEmpty(format);

      Parameters["format"] = format;
      return this;
    }

    public ITranslationRequest From(string language)
    {
      Assertion.NotEmpty(language);

      from = language;
      Parameters["lang"] = Language;
      return this;
    }

    public ITranslationRequest Text(string text)
    {
      Assertion.NotEmpty(text);

      Parameters["text"] = text;
      return this;
    }

    public ITranslationRequest To(string language)
    {
      Assertion.NotEmpty(language);

      to = language;
      Parameters["lang"] = Language;
      return this;
    }

    private string Language
    {
      get
      {
        if (from.IsEmpty() && to.IsEmpty())
        {
          return null;
        }
        
        if (from.IsEmpty())
        {
          return to;
        }
        
        if (to.IsEmpty())
        {
          return from;
        }

        return string.Format(CultureInfo.InvariantCulture, "{0}-{1}", from, to);
      }
    }
  }
}