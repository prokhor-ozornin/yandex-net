namespace Yandex.Translator
{
  using System.Collections.Generic;
  using System.Globalization;
  using System.IO;
  using System.Net;
  using System.Xml.Serialization;
  using Catharsis.Commons;
  using RestSharp;
  using RestSharp.Deserializers;
  using RestSharp.Serializers;

  internal sealed class YandexTranslator : IYandexTranslator
  {
    private const string EndpointUrl = "https://translate.yandex.net/api/v1.5/tr{0}";
    private readonly RestClient restClient;
    private readonly ISerializer jsonSerializer = new YandexTranslatorJsonSerializer();
    private readonly IDeserializer jsonDeserializer = new YandexTranslatorJsonDeserializer();
    private readonly IXmlSerializer xmlSerializer = new YandexTranslatorXmlSerializer();
    private readonly IDeserializer xmlDeserializer = new YandexTranslatorXmlDeserializer();

    public YandexTranslator(ApiDataFormat format, string apiKey)
    {
      Assertion.NotEmpty(apiKey);

      Format = format;
      ApiKey = apiKey;

      restClient = new RestClient(EndpointUrl);
      restClient.AddHandler("application/xml", xmlDeserializer);
      restClient.AddHandler("text/xml", xmlDeserializer);
      restClient.AddHandler("application/json", jsonDeserializer);
      restClient.AddHandler("text/json", jsonDeserializer);
      restClient.AddHandler("text/x-json", jsonDeserializer);
      restClient.AddHandler("text/javascript", jsonDeserializer);
      restClient.AddHandler("*", xmlDeserializer);

      switch (format)
      {
        case ApiDataFormat.Json:
          restClient.BaseUrl = string.Format(CultureInfo.InvariantCulture, EndpointUrl, ".json").ToUri();
          break;

        case ApiDataFormat.Xml:
          restClient.BaseUrl = string.Format(CultureInfo.InvariantCulture, EndpointUrl, string.Empty).ToUri();
          break;
      }
      
      restClient.AddDefaultParameter("key", apiKey);
    }

    public string ApiKey { get; private set; }

    public ApiDataFormat Format { get; private set; }

    public IRestResponse Call(string resource, IDictionary<string, object> parameters = null, IDictionary<string, object> headers = null)
    {
      Assertion.NotEmpty(resource);

      var request = CreateRequest(resource, parameters);
      var response = restClient.Post(request);

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
      var request = CreateRequest(resource, parameters);
      var response = restClient.Get<T>(request);

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

      switch (Format)
      {
        case ApiDataFormat.Json:
          request.RequestFormat = DataFormat.Json;
          request.JsonSerializer = jsonSerializer;
          break;

        case ApiDataFormat.Xml:
          request.RequestFormat = DataFormat.Xml;
          request.XmlSerializer = xmlSerializer;
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