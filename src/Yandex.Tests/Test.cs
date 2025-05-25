using AutoFixture;
using Catharsis.Extensions;
using FluentAssertions;
using Newtonsoft.Json;
using Yandex.Detector;
using Yandex.Translator;
using ErrorDetector = Yandex.Detector.Error;
using ErrorTranslator = Yandex.Translator.Error;
using IErrorDetector = Yandex.Detector.IError;
using IErrorTranslator = Yandex.Translator.IError;

namespace Yandex.Tests;

public class Test : IDisposable
{
  protected IFixture Fixture { get; } = new Fixture();

  protected Test()
  {
    Fixture
      .Map<IErrorDetector, ErrorDetector>()
      .Map<IDetectorRequest, DetectorRequest>()

      .Map<IApiConfigurator, ApiConfigurator>()
      .Map<ITranslationApiRequest, TranslationApiRequest>()
      .Map<IErrorTranslator, ErrorTranslator>();
      ;

    Fixture.Customize<CancellationToken>(token => token.FromFactory<CancellationToken>(_ => new CancellationToken(true)));
    
    JsonConvert.DefaultSettings = () => new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto };
  }

  public virtual void Dispose()
  {
  }

  protected void TestCompareTo<TClass, TProperty>(string property, TProperty lower, TProperty greater, Func<TClass> constructor = null)
  {
    constructor ??= () => typeof(TClass).Instance<TClass>();

    var first = constructor().To<IComparable<TClass>>();
    var second = constructor().To<TClass>();

    first.SetPropertyValue(property, lower);
    second.SetPropertyValue(property, lower);

    first.CompareTo(second).Should().Be(0);
    second.SetPropertyValue(property, greater);
    first.CompareTo(second).Should().BeLessThan(0);
  }

  protected void TestEquality<TClass, TProperty>(string property, TProperty oldValue, TProperty newValue, Func<TClass> constructor = null)
  {
    constructor ??= () => typeof(TClass).Instance<TClass>();
    var entity = constructor();

    entity.Equals(new object()).Should().BeFalse();
    entity.Equals(null).Should().BeFalse();
    entity.Equals(entity).Should().BeTrue();
    //entity.Equals(constructor()).Should().BeTrue();

    constructor().SetPropertyValue(property, oldValue).Equals(constructor().SetPropertyValue(property, oldValue)).Should().BeTrue();
    constructor().SetPropertyValue(property, oldValue).Equals(constructor().SetPropertyValue(property, newValue)).Should().BeFalse();
  }

  protected void TestHashCode<TClass, TProperty>(string property, TProperty oldValue, TProperty newValue, Func<TClass> constructor = null)
  {
    constructor ??= () => typeof(TClass).Instance<TClass>();
    var entity = constructor();

    entity.GetHashCode().Should().Be(entity.GetHashCode());
    //entity.GetHashCode().Should().Be(constructor().GetHashCode());

    constructor().SetPropertyValue(property, oldValue).GetHashCode().Should().Be(constructor().SetPropertyValue(property, oldValue).GetHashCode());
    constructor().SetPropertyValue(property, oldValue).GetHashCode().Should().NotBe(constructor().SetPropertyValue(property, newValue).GetHashCode());
  }
}