using Catharsis.Extensions;
using Catharsis.Fixture;
using RestSharp;
using RestSharp.Serializers;
using FluentAssertions;
using Xunit;
using Yandex.Translator;
using FluentAssertions.Execution;

namespace Yandex.Tests.Translator;

/// <summary>
///   <para>Tests set for class <see cref="Api"/>.</para>
/// </summary>
/// <seealso cref="Api"/>
public sealed class ApiTest : IntegrationTest
{
  /// <summary>
  ///   <para>Performs testing of class constructor(s).</para>
  /// </summary>
  /// <seealso cref="Api(string)"/>
  [Fact]
  public void Constructors()
  {
    typeof(Api).Should().BeDerivedFrom<object>().And.Implement<IApi>();

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => new Api(null)).ThrowExactly<ArgumentNullException>().WithParameterName("key");
      AssertionExtensions.Should(() => new Api(string.Empty)).ThrowExactly<ArgumentException>().WithParameterName("key");


      var api = new Api("apiKey");
      api.GetPropertyValue<ISerializer>("JsonSerializer").Should().NotBeNull();
      api.GetPropertyValue<IDeserializer>("JsonDeserializer").Should().NotBeNull();

      var client = api.GetFieldValue<RestClient>("restClient");
      var key = client.DefaultParameters.FirstOrDefault(parameter => parameter.Name == "key");
      key.Should().NotBeNull();
      key.Value.Should().Be("apiKey");

      throw new NotImplementedException();
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="Api.PairsAsync(CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void PairsAsync_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => Api.PairsAsync(Fixture<CancellationToken>.Create())).ThrowExactly<OperationCanceledException>();

      Test([new TranslationPair("en", "ru"), new TranslationPair("ru", "en")], Api);
    }

    return;

    static void Test(IEnumerable<ITranslationPair> result, IApi api) => api.PairsAsync().ToArray().Should().IntersectWith(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="Api.DetectAsync"/> method.</para>
  /// </summary>
  [Fact]
  public void Detect_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => Api.DetectAsync(null)).ThrowExactlyAsync<ArgumentNullException>().Await();
      AssertionExtensions.Should(() => Api.DetectAsync(string.Empty)).ThrowExactlyAsync<ArgumentException>().Await();
      AssertionExtensions.Should(() => Api.DetectAsync("text", Fixture<CancellationToken>.Create())).ThrowExactlyAsync<TaskCanceledException>().Await();

      Test("en", "Hello, world", Api);
      Test("ru", "Привет, мир", Api);
    }

    return;

    static void Test(string result, string text, IApi api) => api.Detect(text).Should().BeOfType<string>().And.Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="Api.TranslateAsync(ITranslationApiRequest, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void TranslateAsync_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => Api.TranslateAsync(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("request").Await();
      AssertionExtensions.Should(() => Api.TranslateAsync(null, Fixture<CancellationToken>.Create())).ThrowExactlyAsync<OperationCanceledException>().Await();

      Test(new Translation("ru", "en", "Hello world"), request => request.From("ru").To("en").Text("Привет, мир"), Api);
      Test(new Translation("en", "ru", "Привет, мир"), request => request.From("en").To("ru").Text("Hello, world"), Api);
    }

    return;

    static void Test(ITranslation result, Action<ITranslationApiRequest> request, IApi api)
    {
      var task = api.TranslateAsync(request);
      task.Should().BeAssignableTo<Task<ITranslation>>();

      var translation = task.Await();
      translation.Should().BeOfType<Translation>();
      translation.FromLanguage.Should().BeOfType<string>().And.Be(result.FromLanguage);
      translation.ToLanguage.Should().BeOfType<string>().And.Be(result.ToLanguage);
      translation.Text.Should().BeOfType<string>().And.Be(result.Text);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="Api.Dispose()"/> method.</para>
  /// </summary>
  [Fact]
  public void Dispose_Method()
  {
    using (new AssertionScope())
    {
      Test(Api);
    }

    return;

    static void Test(IDisposable disposable)
    {
      disposable.Dispose();

      AssertionExtensions.Should(disposable.Dispose).ThrowExactly<ObjectDisposedException>();
    }
  }
}