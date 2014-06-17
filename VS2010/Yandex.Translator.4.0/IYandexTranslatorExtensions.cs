using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Catharsis.Commons;
using RestSharp;

namespace Yandex.Translator
{
  /// <summary>
  ///   <para>Set of extension methods for interface <see cref="IYandexTranslator"/>.</para>
  /// </summary>
  /// <seealso cref="IYandexTranslator"/>
  public static class IYandexTranslatorExtensions
  {
    /// <summary>
    ///   <para>Makes a remote call to Yandex.Translator REST web service and returns it's response.</para>
    /// </summary>
    /// <param name="translator">Translator instance to be used.</param>
    /// <param name="resource">Relative URL of web resource to be used. Base endpoint URL is predefined by implentation.</param>
    /// <param name="parameters">Object whose public properties represent query parameters names/values.</param>
    /// <param name="headers">Object whose public properties represent HTTP headers names/values.</param>
    /// <returns>Response from Yandex.Translator web service.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="translator"/> or <paramref name="resource"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <paramref name="resource"/> is <see cref="string.Empty"/> string.</exception>
    /// <exception cref="TranslatorException">If error occurs during the processing of web request, or if request was invalid.</exception>
    public static IRestResponse Call(this IYandexTranslator translator, string resource, object parameters = null, object headers = null)
    {
      Assertion.NotNull(translator);
      Assertion.NotEmpty(resource);

      IDictionary<string, object> parametersMap = null;
      if (parameters != null)
      {
        parametersMap = new Dictionary<string, object>();
        foreach (var property in parameters.GetType().GetProperties())
        {
          parametersMap[property.Name] = property.GetValue(parameters, null);
        }
      }

      IDictionary<string, object> headersMap = null;
      if (headers != null)
      {
        headersMap = new Dictionary<string, object>();
        foreach (var property in headers.GetType().GetProperties())
        {
          headersMap[property.Name] = property.GetValue(parameters, null);
        }
      }

      return translator.Call(resource, parametersMap, headersMap);
    }

    /// <summary>
    ///   <para>Makes a remote call to Yandex.Translator REST web service and returns deserialized generic response.</para>
    /// </summary>
    /// <typeparam name="T">Type of object that is created from web service's response.</typeparam>
    /// <param name="translator">Translator instance to be used.</param>
    /// <param name="resource">Relative URL of web resource to be used. Base endpoint URL is predefined by implentation.</param>
    /// <param name="parameters">Object whose public properties represent query parameters names/values.</param>
    /// <param name="headers">Object whose public properties represent HTTP headers names/values.</param>
    /// <returns>Generic response from Yandex.Translator web service.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="translator"/> or <paramref name="resource"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <paramref name="resource"/> is <see cref="string.Empty"/> string.</exception>
    /// <exception cref="TranslatorException">If error occurs during the processing of web request, or if request was invalid.</exception>
    public static IRestResponse<T> Call<T>(this IYandexTranslator translator, string resource, object parameters = null, object headers = null) where T : new()
    {
      Assertion.NotNull(translator);
      Assertion.NotEmpty(resource);

      IDictionary<string, object> parametersMap = null;
      if (parameters != null)
      {
        parametersMap = new Dictionary<string, object>();
        foreach (var property in parameters.GetType().GetProperties())
        {
          parametersMap[property.Name] = property.GetValue(parameters, null);
        }
      }

      IDictionary<string, object> headersMap = null;
      if (headers != null)
      {
        headersMap = new Dictionary<string, object>();
        foreach (var property in parameters.GetType().GetProperties())
        {
          headersMap[property.Name] = property.GetValue(parameters, null);
        }
      }

      return translator.Call<T>(resource, parametersMap, headersMap);
    }

    /// <summary>
    ///   <para>Makes a language detection request to Yandex.Translator web service.</para>
    /// </summary>
    /// <param name="translator">Translator instance to be used.</param>
    /// <param name="text">Text fragment which language is to be detected.</param>
    /// <returns>Language of the provided text fragment.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="translator"/> or <paramref name="text"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <paramref name="text"/> is <see cref="string.Empty"/> string.</exception>
    /// <exception cref="TranslatorException">If error occurs during the processing of web request, or if language of given text fragment cannot be reliably determined.</exception>
    /// <seealso cref="http://api.yandex.ru/translate/doc/dg/reference/detect.xml"/>
    public static string Detect(this IYandexTranslator translator, string text)
    {
      Assertion.NotNull(translator);
      Assertion.NotEmpty(text);

      var response = translator.Call<DetectedLanguageResult>("detect", new Dictionary<string, object> { { "text", text } }).Data;
      if (response.Code != (int) HttpStatusCode.OK || response.Language.IsEmpty())
      {
        throw new TranslatorException(new Error(response.Code, "Cannot determine source language for text"));
      }
      return response.Language;
    }

    /// <summary>
    ///   <para>Makes a translation request to Yandex.Translator web service.</para>
    /// </summary>
    /// <param name="translator">Translator instance to be used.</param>
    /// <param name="language">Translation language. Can be either a single language code (like "en"), representing a target language, or a language pair (like "en-ru"), representing both source and target languages respectively.</param>
    /// <param name="text">Text to be translated.</param>
    /// <param name="format">Format of original text.</param>
    /// <returns><see cref="ITranslation"/> instance that represents result of text's translation.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="translator"/>, <paramref name="language"/> or <paramref name="text"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If either <paramref name="language"/> or <paramref name="text"/> is <see cref="string.Empty"/> string.</exception>
    /// <exception cref="TranslatorException">If error occurs during the processing of web request, or if given text cannot be translated by web service.</exception>
    /// <seealso cref="Translate(IYandexTranslator, Action{ITranslationRequest})"/>
    /// <seealso cref="http://api.yandex.ru/translate/doc/dg/reference/translate.xml"/>
    public static ITranslation Translate(this IYandexTranslator translator, string language, string text, string format = null)
    {
      Assertion.NotNull(translator);
      Assertion.NotEmpty(language);
      Assertion.NotEmpty(text);

      var response = translator.Call<TranslationResult>("/translate", new Dictionary<string, object> { { "text", text }, { "lang", language }, { "format", format } }).Data;
      if (response.Code != (int)HttpStatusCode.OK || response.Text.IsEmpty())
      {
        throw new TranslatorException(new Error(response.Code, "Text translation failed"));
      }

      var languages = response.Language.Split('-');

      return new Translation(languages[0], languages[1], response.Text);
    }

    /// <summary>
    ///   <para>Makes a translation request to Yandex.Translator web service.</para>
    /// </summary>
    /// <param name="translator">Translator instance to be used.</param>
    /// <param name="request">Delegate that specifies text for translation, source/target languages and addional options.</param>
    /// <returns><see cref="ITranslation"/> instance that represents result of text's translation.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="translator"/> or <paramref name="request"/> is a <c>null</c> reference.</exception>
    /// <exception cref="TranslatorException">If error occurs during the processing of web request, or if given text cannot be translated by web service.</exception>
    /// <seealso cref="Translate(IYandexTranslator, string, string, string)"/>
    /// <seealso cref="http://api.yandex.ru/translate/doc/dg/reference/translate.xml"/>
    public static ITranslation Translate(this IYandexTranslator translator, Action<ITranslationRequest> request)
    {
      Assertion.NotNull(translator);
      Assertion.NotNull(request);

      var translationRequest = new TranslationRequest();
      request(translationRequest);

      var response = translator.Call<TranslationResult>("/translate", translationRequest.Parameters).Data;
      if (response.Code != (int)HttpStatusCode.OK || response.Text.IsEmpty())
      {
        throw new TranslatorException(new Error(response.Code, "Text translation failed"));
      }

      var languages = response.Language.Split('-');

      return new Translation(languages[0], languages[1], response.Text);
    }

    /// <summary>
    ///   <para>Makes a request to Yandex.Translator web service to return collection of supported languages pairs (translations directions).</para>
    /// </summary>
    /// <param name="translator">Translator instance to be used.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="translator"/> is a <c>null</c> reference.</exception>
    /// <exception cref="TranslatorException">If error occurs during the processing of web request.</exception>
    /// <seealso cref="http://api.yandex.ru/translate/doc/dg/reference/getLangs.xml"/>
    public static IEnumerable<ITranslationPair> TranslationPairs(this IYandexTranslator translator)
    {
      Assertion.NotNull(translator);

      return translator.Call<TranslationPairsResult>("/getLangs").Data.Pairs.Select(pair =>
      {
        var languages = pair.Split('-');
        return new TranslationPair(languages[0], languages[1]);
      }).Cast<ITranslationPair>();
    }
  }
}