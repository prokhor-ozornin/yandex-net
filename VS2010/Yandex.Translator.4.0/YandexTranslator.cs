using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Xml.Serialization;
using Catharsis.Commons;
using RestSharp;
using RestSharp.Deserializers;
using RestSharp.Serializers;

namespace Yandex.Translator
{
  internal sealed class YandexTranslator : IYandexTranslator
  {
    private const string EndpointUrl = "https://translate.yandex.net/api/v1.5/tr{0}";

    private readonly string apiKey;
    private readonly ApiDataFormat format;
    private readonly RestClient restClient;
    private readonly ISerializer jsonSerializer = new YandexTranslatorJsonSerializer();
    private readonly IDeserializer jsonDeserializer = new YandexTranslatorJsonDeserializer();
    private readonly ISerializer xmlSerializer = new YandexTranslatorXmlSerializer();
    private readonly IDeserializer xmlDeserializer = new YandexTranslatorXmlDeserializer();

    public YandexTranslator(ApiDataFormat format, string apiKey)
    {
      Assertion.NotEmpty(apiKey);

      this.format = format;
      this.apiKey = apiKey;

      this.restClient = new RestClient(EndpointUrl);
      this.restClient.AddHandler("application/xml", this.xmlDeserializer);
      this.restClient.AddHandler("text/xml", this.xmlDeserializer);
      this.restClient.AddHandler("application/json", this.jsonDeserializer);
      this.restClient.AddHandler("text/json", this.jsonDeserializer);
      this.restClient.AddHandler("text/x-json", this.jsonDeserializer);
      this.restClient.AddHandler("text/javascript", this.jsonDeserializer);
      this.restClient.AddHandler("*", this.xmlDeserializer);

      switch (format)
      {
        case ApiDataFormat.Json :
          this.restClient.BaseUrl = EndpointUrl.FormatSelf(".json");
          break;

        case ApiDataFormat.Xml :
          this.restClient.BaseUrl = EndpointUrl.FormatSelf(string.Empty);
          break;
      }
      
      this.restClient.AddDefaultParameter("key", apiKey);
    }

    public string ApiKey
    {
      get { return this.apiKey; }
    }

    public ApiDataFormat Format
    {
      get { return this.format; }
    }

    public IRestResponse Call(string resource, IDictionary<string, object> parameters = null, IDictionary<string, object> headers = null)
    {
      Assertion.NotEmpty(resource);

      var request = this.CreateRequest(resource, parameters);
      var response = this.restClient.Post(request);

      if (response.ErrorException != null || response.StatusCode != HttpStatusCode.OK)
      {
        throw new TranslatorException(new Error((int) response.StatusCode, response.ErrorMessage ?? response.StatusDescription), response.ErrorException);
      }

      IError error = null;
      try
      {
        switch (request.RequestFormat)
        {
          case DataFormat.Json:
            error = response.Content.Json<Error>();
            break;

          case DataFormat.Xml:
            var serializer = new System.Xml.Serialization.XmlSerializer(typeof(Error), new XmlRootAttribute("result"));
            using (var source = new StringReader(response.Content))
            {
              error = serializer.Deserialize(source).To<Error>();
            }
            break;
        }
      }
      catch
      {
      }
      if (error != null && !error.Text.IsEmpty())
      {
        throw new TranslatorException(error);
      }

      return response;
    }

    public IRestResponse<T> Call<T>(string resource, IDictionary<string, object> parameters = null, IDictionary<string, object> headers = null) where T : new()
    {
      var request = this.CreateRequest(resource, parameters);
      var response = this.restClient.Get<T>(request);

      if (response.ErrorException != null || response.StatusCode != HttpStatusCode.OK)
      {
        throw new TranslatorException(new Error((int) response.StatusCode, response.ErrorMessage ?? response.StatusDescription), response.ErrorException);
      }

      IError error = null;
      try
      {
        switch (request.RequestFormat)
        {
          case DataFormat.Json:
            error = response.Content.Json<Error>();
            break;

          case DataFormat.Xml:
            var serializer = new System.Xml.Serialization.XmlSerializer(typeof(Error), new XmlRootAttribute("result"));
            using (var source = new StringReader(response.Content))
            {
              error = serializer.Deserialize(source).To<Error>();
            }
            break;
        }
      }
      catch
      {
      }
      if (error != null && !error.Text.IsEmpty())
      {
        throw new TranslatorException(error);
      }

      return response;
    }

    private RestRequest CreateRequest(string resource, IEnumerable<KeyValuePair<string, object>> parameters = null)
    {
      Assertion.NotEmpty(resource);

      var request = new RestRequest(resource);

      switch (this.format)
      {
        case ApiDataFormat.Json:
          request.RequestFormat = DataFormat.Json;
          request.JsonSerializer = this.jsonSerializer;
          break;

        case ApiDataFormat.Xml:
          request.RequestFormat = DataFormat.Xml;
          request.XmlSerializer = this.xmlSerializer;
          break;
      }

      if (parameters != null)
      {
        foreach (var parameter in parameters)
        {
          if (parameter.Value != null)
          {
            request.AddParameter(parameter.Key, parameter.Value.ToStringInvariant());
          }
        }
      }

      return request;
    }
  }
}