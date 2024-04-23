using Catharsis.Commons;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;
using Yandex.Translator;

namespace Yandex.Tests.Translator;

/// <summary>
///   <para>Tests set for class <see cref="ApiConfigurator"/>.</para>
/// </summary>
public sealed class ApiConfiguratorTest : UnitTest
{
  /// <summary>
  ///   <para>Performs testing of class constructor(s).</para>
  /// </summary>
  /// <seealso cref="ApiConfigurator()"/>
  [Fact]
  public void Constructors()
  {
    typeof(ApiConfigurator).Should().BeDerivedFrom<object>().And.Implement<IApiConfigurator>();

    var configurator = new ApiConfigurator();
    configurator.ApiKeyValue.Should().BeNull();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ApiConfigurator.ApiKey(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void ApiKey_Method()
  {
    using (new AssertionScope())
    {
      Validate("apiKey", new ApiConfigurator());
    }

    return;

    static void Validate(string key, IApiConfigurator configurator) => configurator.ApiKey(key).Should().BeSameAs(configurator).And.BeOfType<ApiConfigurator>().Which.ApiKeyValue.Should().Be(key);
  }
}