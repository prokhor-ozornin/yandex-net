using FluentAssertions;
using Xunit;
using Yandex.Translator;

namespace Yandex.Tests.Translator;

/// <summary>
///   <para>Tests set for class <see cref="ApiConfigurator"/>.</para>
/// </summary>
public sealed class ApiConfiguratorTest
{
  /// <summary>
  ///   <para>Performs testing of class constructor(s).</para>
  /// </summary>
  /// <seealso cref="ApiConfigurator()"/>
  [Fact]
  public void Constructors()
  {
    var configurator = new ApiConfigurator();
    configurator.ApiKeyValue.Should().BeNull();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ApiConfigurator.ApiKey(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void ApiKey_Method()
  {
    AssertionExtensions.Should(() => new ApiConfigurator().ApiKey(null)).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => new ApiConfigurator().ApiKey(string.Empty)).ThrowExactly<ArgumentException>();

    var configurator = new ApiConfigurator();
    configurator.ApiKey("apiKey").Should().NotBeNull().And.BeSameAs(configurator);
    configurator.ApiKeyValue.Should().Be("apiKey");
  }
}