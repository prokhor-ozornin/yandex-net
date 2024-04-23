using Catharsis.Commons;
using Catharsis.Extensions;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;
using Yandex.Translator;

namespace Yandex.Tests.Translator;

/// <summary>
///   <para>Tests set for class <see cref="IApiExtensions"/>.</para>
/// </summary>
public sealed class IApiExtensionsTest : IntegrationTest
{
  /// <summary>
  ///   <para>Performs testing of <see cref="IApiExtensions.TranslateAsync(IApi, Action{ITranslationApiRequest}, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void TranslateAsync_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => IApiExtensions.TranslateAsync(null, _ => { })).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("api").Await();
      AssertionExtensions.Should(() => IApiExtensions.TranslateAsync(Api, null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("action").Await();
      AssertionExtensions.Should(() => Api.TranslateAsync(_ => { }, Attributes.CancellationToken())).ThrowExactlyAsync<OperationCanceledException>().Await();

      Validate(new Translation("ru", "en", "Hello world"), request => request.From("ru").To("en").Text("Привет, мир"), Api);
    }

    return;

    static void Validate(ITranslation result, Action<ITranslationApiRequest> request, IApi api)
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
  ///   <para>Performs testing of <see cref="IApiExtensions.Pairs(IApi)"/> method.</para>
  /// </summary>
  [Fact]
  public void Pairs_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => IApiExtensions.Pairs(null)).ThrowExactly<ArgumentNullException>().WithParameterName("api");

      Validate([new TranslationPair("en", "ru"), new TranslationPair("ru", "en")], Api);
    }

    return;

    static void Validate(IEnumerable<ITranslationPair> result, IApi api) => api.Pairs().Should().BeOfType<List<ITranslationPair>>().And.IntersectWith(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IApiExtensions.Detect(IApi, string)"/> method.</para>
  /// </summary>
  [Fact]
  public void Detect_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => IApiExtensions.Detect(null, string.Empty)).ThrowExactly<ArgumentNullException>().WithParameterName("api");
      AssertionExtensions.Should(() => Api.Detect(null)).ThrowExactly<ArgumentNullException>().WithParameterName("text");

      Validate("en", "Hello, world", Api);
      Validate("ru", "Привет, мир", Api);
    }

    return;

    static void Validate(string result, string text, IApi api) => api.Detect(text).Should().BeOfType<string>().And.Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="IApiExtensions.Translate(IApi, ITranslationApiRequest)"/></description></item>
  ///     <item><description><see cref="IApiExtensions.Translate(IApi, Action{ITranslationApiRequest})"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void Translate_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => IApiExtensions.Translate(null, new TranslationApiRequest())).ThrowExactly<ArgumentNullException>().WithParameterName("api");
      AssertionExtensions.Should(() => Api.Translate((ITranslationApiRequest) null)).ThrowExactly<ArgumentNullException>().WithParameterName("request");

      Validate(new Translation("ru", "en", "Hello world"), new TranslationApiRequest().From("ru").To("en").Text("Привет, мир"), Api);

      static void Validate(ITranslation result, ITranslationApiRequest request, IApi api)
      {
        var translation = api.Translate(request);

        translation.Should().BeOfType<Translation>();
        translation.FromLanguage.Should().BeOfType<string>().And.Be(result.FromLanguage);
        translation.ToLanguage.Should().BeOfType<string>().And.Be(result.ToLanguage);
        translation.Text.Should().BeOfType<string>().And.Be(result.Text);
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => IApiExtensions.Translate(null, _ => {})).ThrowExactly<ArgumentNullException>().WithParameterName("api");
      AssertionExtensions.Should(() => Api.Translate((Action<ITranslationApiRequest>) null)).ThrowExactly<ArgumentNullException>().WithParameterName("action");

      Validate(new Translation("ru", "en", "Hello world"), request => request.From("ru").To("en").Text("Привет, мир"), Api);

      static void Validate(ITranslation result, Action<ITranslationApiRequest> request, IApi api)
      {
        var translation = api.Translate(request);

        translation.Should().BeOfType<Translation>();
        translation.FromLanguage.Should().BeOfType<string>().And.Be(result.FromLanguage);
        translation.ToLanguage.Should().BeOfType<string>().And.Be(result.ToLanguage);
        translation.Text.Should().BeOfType<string>().And.Be(result.Text);
      }
    }
  }
}