**Yandex.Translator** is a .NET library for Yandex.Translate web service ([Yandex.Translate web service](http://api.yandex.ru/translate)), used for text translation.

**NuGet package** : https://www.nuget.org/packages/Yandex.Translator

***

**Support**

This project needs your support for further developments ! Please consider donating.

- _Yandex.Money_ : 41001577953208

- _WebMoney (WMR)_ : R399275865890

[![Image](https://www.paypalobjects.com/en_US/i/btn/btn_donateCC_LG.gif)](https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=APHM8MU9N76V8 "Donate")

***

**Initialization**

Before you can make requests to the Yandex.Translate API, you need to setup it.
The following information is required :

1. _APIKey_ which you can acquire from here - [http://api.yandex.ru/key/form.xml?service=trnsl](http://api.yandex.ru/key/form.xml?service=trnsl)

2. Data exchange format to use (XML/JSON).


Example (initialize API, using value of API key stored inside application configuration file): 

`IYandexTranslator translator = Yandex.Translator(api => api.ApiKey(ConfigurationManager.AppSettings["ApiKey"]).Format(ApiDataFormat.Json));`

***

Supported set of operations:

***

**Translations pairs**

_Description:_ Returns list of supported language pairs (translation directions), consisting of source and destination language codes (for example, "en-ru").

_Documentation:_ [http://api.yandex.ru/translate/doc/dg/reference/getLangs.xml](http://api.yandex.ru/translate/doc/dg/reference/getLangs.xml)

_Code:_

`IYandexTranslator translator = ...`

`IEnumerable<ITranslationPair> translationPairs = translator.TranslationPairs();`

***

**Language detection**

_Description:_ Detects natural language of the provided text fragment.

_Documentation:_ [http://api.yandex.ru/translate/doc/dg/reference/detect.xml](http://api.yandex.ru/translate/doc/dg/reference/detect.xml)

`IYandexTranslator translator = ...`

`string language = translator.Detect("This is English text");`

**Text translation**

_Description:_ Performs translation of provided text fragment from source to destination languages.

_Documentation:_ [http://api.yandex.ru/translate/doc/dg/reference/translate.xml](http://api.yandex.ru/translate/doc/dg/reference/translate.xml)

_Code:_

`IYandexTranslator translator = ...`

`ITranslation translation = translator.Translate("ru", "To be translated to Russian");`

`ITranslation translation = translator.Translate("en-ru", "To be translated from English to Russian", "html");`

`ITranslation translation = translator.Translate(request => request.From("en").To("ru").Text("To be translated from English to Russian").Html());`