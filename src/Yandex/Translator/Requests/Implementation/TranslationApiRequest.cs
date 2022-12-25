using Catharsis.Commons;

namespace Yandex.Translator;

internal sealed class TranslationApiRequest : ApiRequest, ITranslationApiRequest
{
  private string? FromLanguage { get; set; }

  private string? ToLanguage { get; set; }

  public ITranslationApiRequest Format(string? format)
  {
    Parameters["format"] = format;

    return this;
  }

  public ITranslationApiRequest From(string? language)
  {
    FromLanguage = language;

    Parameters["lang"] = Languages;

    return this;
  }

  public ITranslationApiRequest To(string? language)
  {
    ToLanguage = language;

    Parameters["lang"] = Languages;

    return this;
  }

  public ITranslationApiRequest Text(string? text)
  {
    Parameters["text"] = text;

    return this;
  }

  private string? Languages
  {
    get
    {
      if (FromLanguage.IsEmpty() && ToLanguage.IsEmpty())
      {
        return null;
      }

      if (FromLanguage.IsEmpty())
      {
        return ToLanguage;
      }

      if (ToLanguage.IsEmpty())
      {
        return FromLanguage;
      }

      return $"{FromLanguage}-{ToLanguage}";
    }
  }
}