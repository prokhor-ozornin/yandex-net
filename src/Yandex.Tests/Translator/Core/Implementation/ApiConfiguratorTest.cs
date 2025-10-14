using Catharsis.Fixture;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;
using Yandex.Translator;

namespace Yandex.Tests.Translator;

/// <summary>
///   <para>Tests set for class <see cref="ApiConfigurator"/>.</para>
/// </summary>
/// <seealso cref="ApiConfigurator"/>
public sealed class ApiConfiguratorTest : Test
{
  /// <summary>
  ///   <para>Performs testing of class constructor(s).</para>
  /// </summary>
  /// <seealso cref="ApiConfigurator()"/>
  [Fact]
  public void Constructors()
  {
    typeof(ApiConfigurator).Should().BeDerivedFrom<object>().And.Implement<IApiConfigurator>();

    using (new AssertionScope())
    {
      var configurator = new ApiConfigurator();

      configurator.ApiKeyValue.Should().BeNull();
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ApiConfigurator.ApiKey(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void ApiKey_Method()
  {
    using (new AssertionScope())
    {
      Test("apiKey", new ApiConfigurator());
      Test(Fixture<string>.Create(), Fixture<IApiConfigurator>.Create());
    }

    return;

    static void Test(string key, IApiConfigurator configurator) => configurator.ApiKey(key).Should().BeSameAs(configurator).And.BeOfType<ApiConfigurator>().Which.ApiKeyValue.Should().Be(key);
  }
}