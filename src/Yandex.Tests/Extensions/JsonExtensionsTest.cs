using FluentAssertions;
using FluentAssertions.Execution;
using Newtonsoft.Json;
using Xunit;

namespace Yandex.Tests;

/// <summary>
///   <para>Tests set for class <see cref="JsonExtensions"/>.</para>
/// </summary>
/// <seealso cref="JsonExtensions"/>
public sealed class JsonExtensionsTest : Test
{
  /// <summary>
  ///   <para>Performs testing of <see cref="JsonExtensions.SerializeAsJson(object, JsonSerializerSettings)"/> method.</para>
  /// </summary>
  [Fact]
  public void SerializeAsJson_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => JsonExtensions.SerializeAsJson(null)).ThrowExactly<ArgumentNullException>().WithParameterName("instance");
    }

    return;

    static void Test()
    {
      throw new NotImplementedException();
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="JsonExtensions.DeserializeAsJson{T}(string, JsonSerializerSettings)"/> method.</para>
  /// </summary>
  [Fact]
  public void DeserializeAsJson_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => JsonExtensions.DeserializeAsJson<object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("json");
    }

    return;

    static void Test()
    {
      throw new NotImplementedException();
    }
  }
}