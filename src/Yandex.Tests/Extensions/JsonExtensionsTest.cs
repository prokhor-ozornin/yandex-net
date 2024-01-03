using Catharsis.Commons;
using Newtonsoft.Json;
using FluentAssertions;
using Xunit;

namespace Yandex.Tests;

/// <summary>
///   <para>Tests set for class <see cref="JsonExtensions"/>.</para>
/// </summary>
public sealed class JsonExtensionsTest : UnitTest
{
  /// <summary>
  ///   <para>Performs testing of <see cref="JsonExtensions.SerializeAsJson(object, JsonSerializerSettings)"/> method.</para>
  /// </summary>
  [Fact]
  public void SerializeAsJson_Method()
  {
    AssertionExtensions.Should(() => JsonExtensions.SerializeAsJson(null)).ThrowExactly<ArgumentNullException>().WithParameterName("instance");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="JsonExtensions.DeserializeAsJson{T}(string, JsonSerializerSettings)"/> method.</para>
  /// </summary>
  [Fact]
  public void DeserializeAsJson_Method()
  {
    AssertionExtensions.Should(() => JsonExtensions.DeserializeAsJson<object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("json");

    throw new NotImplementedException();
  }
}