using Newtonsoft.Json;
using FluentAssertions;
using Xunit;

namespace Yandex.Tests;

/// <summary>
///   <para>Tests set for class <see cref="JsonExtensions"/>.</para>
/// </summary>
public sealed class JsonExtensionsTest
{
  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="JsonExtensions.AsJson(object, JsonSerializerSettings?)"/></description></item>
  ///     <item><description><see cref="JsonExtensions.AsJson(object, JsonSerializerSettings?)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void AsJson_Methods()
  {
    AssertionExtensions.Should(() => JsonExtensions.AsJson(null!)).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => JsonExtensions.AsJson<object>(null!)).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => string.Empty.AsJson<object>()).ThrowExactly<ArgumentException>();

    AssertionExtensions.Should(() => "{}".AsJson<string>()).ThrowExactly<JsonReaderException>();

    string.Empty.AsJson().AsJson<string>().Should().BeEmpty();

    var jsonArray = @"[""first"",""second""]";
    jsonArray.AsJson<string[]>().Should().Equal("first", "second");
    jsonArray.AsJson<IList<string>>().Should().Equal("first", "second");
    jsonArray.AsJson<ICollection<string>>().Should().Equal("first", "second");
    jsonArray.AsJson<IEnumerable<string>>().Should().Equal("first", "second");

    var subject = @"{""Id"":0}".AsJson<MockJsonObject>();
    subject.Date.Should().BeNull();
    subject.Id.Should().Be(0);
    subject.PublicField.Should().BeNull();
    subject.PublicProperty.Should().BeNull();

    subject = $@"{{""Id"":1,""Date"":""{DateTimeOffset.MinValue}"",""PublicField"":""field"",""PublicProperty"":""property""}}".AsJson<MockJsonObject>();
    subject.Date.Should().Be(DateTimeOffset.MinValue);
    subject.Id.Should().Be(1);
    subject.PublicField.Should().Be("field");
    subject.PublicProperty.Should().Be("property");

    throw new NotImplementedException();
  }

  private sealed class MockJsonObject
  {
    public long? Id
    {
      get; set;
    }

    public string? PublicProperty
    {
      get; set;
    }

    public string? PublicField;

    public DateTimeOffset? Date
    {
      get; set;
    }
  }
}