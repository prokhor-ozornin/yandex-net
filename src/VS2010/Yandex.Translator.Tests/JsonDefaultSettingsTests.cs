using Catharsis.Commons;
using Newtonsoft.Json;
using Xunit;

namespace Yandex.Translator
{
  /// <summary>
  ///   <para>Tests set for class <see cref="JsonDefaultSettings"/>.</para>
  /// </summary>
  public sealed class JsonDefaultSettingsTests
  {
    /// <summary>
    ///   <para>Performs testing of <see cref="JsonDefaultSettings.Deserializer"/> property.</para>
    /// </summary>
    [Fact]
    public void Deserializer_Property()
    {
      var settings = JsonDefaultSettings.Serializer;
      Assert.True(settings.ContractResolver.Is<JsonEntityOrderedContractResolver>());
      Assert.Equal(Formatting.None, settings.Formatting);
      Assert.Equal("o", settings.DateFormatString);
      Assert.Equal(DateFormatHandling.IsoDateFormat, settings.DateFormatHandling);
      Assert.Equal(DateTimeZoneHandling.RoundtripKind, settings.DateTimeZoneHandling);
      Assert.Equal(DefaultValueHandling.Include, settings.DefaultValueHandling);
      Assert.Equal(NullValueHandling.Ignore, settings.NullValueHandling);
      Assert.Equal(PreserveReferencesHandling.None, settings.PreserveReferencesHandling);
      Assert.Equal(ReferenceLoopHandling.Ignore, settings.ReferenceLoopHandling);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="JsonDefaultSettings.Serializer"/> property.</para>
    /// </summary>
    [Fact]
    public void Serializer_Property()
    {
      var settings = JsonDefaultSettings.Deserializer;
      Assert.Equal(ConstructorHandling.AllowNonPublicDefaultConstructor, settings.ConstructorHandling);
      Assert.Equal(DateTimeZoneHandling.RoundtripKind, settings.DateTimeZoneHandling);
      Assert.Equal(NullValueHandling.Ignore, settings.NullValueHandling);
      Assert.Equal(ObjectCreationHandling.Auto, settings.ObjectCreationHandling);
    }
  }
}