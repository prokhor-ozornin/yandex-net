using Catharsis.Extensions;
using Catharsis.Fixture;
using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Json;
using Xunit;
using Yandex.Translator;

namespace Yandex.Tests.Translator;

/// <summary>
///   <para>Tests set for class <see cref="Error"/>.</para>
/// </summary>
/// <seealso cref="Error"/>
public sealed class ErrorTest : Test
{
  /// <summary>
  ///   <para>Performs testing of class constructor(s).</para>
  /// </summary>
  /// <seealso cref="Error()"/>
  [Fact]
  public void Constructors()
  {
    typeof(Error).Should().BeDerivedFrom<object>().And.Implement<IError>();

    using (new AssertionScope())
    {
      var error = new Error();
      error.Code.Should().Be(0);
      error.Text.Should().BeNull();
    }

    using (new AssertionScope())
    {
      var error = new Error(int.MaxValue, "text");
      error.Code.Should().Be(int.MaxValue);
      error.Text.Should().Be("text");
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="Error.Code"/> property.</para>
  /// </summary>
  [Fact]
  public void Code_Property() => new Error { Code = int.MaxValue }.Code.Should().Be(int.MaxValue);

  /// <summary>
  ///   <para>Performs testing of <see cref="Error.Text"/> property.</para>
  /// </summary>
  [Fact]
  public void Text_Property() => new Error { Text = "text" }.Text.Should().Be("text");

  /// <summary>
  ///   <para>Performs testing of <see cref="Error.CompareTo(IError)"/> method.</para>
  /// </summary>
  [Fact]
  public void CompareTo_Method() => TestCompareTo<Error, int>(nameof(Error.Code), 1, 2);

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="Error.Equals(IError)"/></description></item>
  ///     <item><description><see cref="Error.Equals(object)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void Equals_Methods() => TestEquality<Error, int>(nameof(Error.Code), 1, 2);

  /// <summary>
  ///   <para>Performs testing of <see cref="Error.GetHashCode()"/> method.</para>
  /// </summary>
  [Fact]
  public void GetHashCode_Method() => TestHashCode<Error, int>(nameof(Error.Code), 1, 2);

  /// <summary>
  ///   <para>Performs testing of <see cref="Error.ToString()"/> method.</para>
  /// </summary>
  [Fact]
  public void ToString_Method()
  {
    using (new AssertionScope())
    {
      Test(string.Empty, new Error());
      Test(string.Empty, new Error { Text = string.Empty });
      Test("text", new Error { Text = "text" });
    }

    return;

    static void Test(string value, Error error) => error.ToString().Should().Be(value);
  }

  /// <summary>
  ///   <para>Performs testing of serialization/deserialization process.</para>
  /// </summary>
  [Fact]
  public void Serialization()
  {
    using (new AssertionScope())
    {
      Test(new Error());
      Test(Fixture<IError>.Create());
    }

    return;

    static void Test(IError error) => error.To<object>().Should().BeDataContractSerializable().And.BeXmlSerializable().And.BeJsonSerializable();
  }
}