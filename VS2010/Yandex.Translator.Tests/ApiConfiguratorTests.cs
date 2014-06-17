using System;
using Xunit;

namespace Yandex.Translator
{
  /// <summary>
  ///   <para>Tests set for class <see cref="ApiConfigurator"/>.</para>
  /// </summary>
  public sealed class ApiConfiguratorTests
  {
    /// <summary>
    ///   <para>Performs testing of class constructor(s).</para>
    /// </summary>
    /// <seealso cref="ApiConfigurator()"/>
    [Fact]
    public void Constructors()
    {
      var configurator = new ApiConfigurator();
      Assert.Equal(ApiDataFormat.Json, configurator.GetFormat());
      Assert.Null(configurator.GetApiKey());
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="ApiConfigurator.Format(ApiDataFormat)"/> method.</para>
    /// </summary>
    [Fact]
    public void Format_Method()
    {
      var configurator = new ApiConfigurator();
      Assert.True(ReferenceEquals(configurator.Format(ApiDataFormat.Xml), configurator));
      Assert.Equal(ApiDataFormat.Xml, configurator.GetFormat());
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="ApiConfigurator.ApiKey(string)"/> method.</para>
    /// </summary>
    [Fact]
    public void ApiKey_Method()
    {
      Assert.Throws<ArgumentNullException>(() => new ApiConfigurator().ApiKey(null));
      Assert.Throws<ArgumentException>(() => new ApiConfigurator().ApiKey(string.Empty));

      var configurator = new ApiConfigurator();
      Assert.True(ReferenceEquals(configurator.ApiKey("apiKey"), configurator));
      Assert.Equal("apiKey", configurator.GetApiKey());
    }
  }
}