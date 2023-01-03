using FluentAssertions;
using FluentAssertions.Json;
using Xunit;
using Yandex.Translator;

namespace Yandex.Tests.Translator;

/// <summary>
///   <para>Tests set for class <see cref="Error"/>.</para>
/// </summary>
public sealed class ErrorTest : UnitTest<Error>
{
  /// <summary>
  ///   <para>Performs testing of <see cref="Error.Code"/> property.</para>
  /// </summary>
  [Fact]
  public void Code_Property() { new Error(new {Code = int.MaxValue}).Code.Should().Be(int.MaxValue); }

  /// <summary>
  ///   <para>Performs testing of <see cref="Error.Text"/> property.</para>
  /// </summary>
  [Fact]
  public void Text_Property() { new Error(new {Text = Guid.Empty.ToString()}).Text.Should().Be(Guid.Empty.ToString()); }

  /// <summary>
  ///   <para>Performs testing of class constructor(s).</para>
  /// </summary>
  /// <seealso cref="Error(int, string)"/>
  /// <seealso cref="Error(Error.Info)"/>
  /// <seealso cref="Error(object)"/>
  [Fact]
  public void Constructors()
  {
    var error = new Error(int.MaxValue, Guid.Empty.ToString());
    error.Code.Should().Be(int.MaxValue);
    error.Text.Should().Be(Guid.Empty.ToString());

    error = new Error(new Error.Info());
    error.Code.Should().Be(0);
    error.Text.Should().BeEmpty();

    error = new Error(new {});
    error.Code.Should().Be(0);
    error.Text.Should().BeEmpty();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="Error.CompareTo(IError)"/> method.</para>
  /// </summary>
  [Fact]
  public void CompareTo_Method() { TestCompareTo(nameof(Error.Code), 1, 2); }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="Error.Equals(IError)"/></description></item>
  ///     <item><description><see cref="Error.Equals(object)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void Equals_Methods() { TestEquality(nameof(Error.Code), 1, 2); }

  /// <summary>
  ///   <para>Performs testing of <see cref="Error.GetHashCode()"/> method.</para>
  /// </summary>
  [Fact]
  public void GetHashCode_Method() { TestHashCode(nameof(Error.Code), 1, 2); }

  /// <summary>
  ///   <para>Performs testing of <see cref="Error.ToString()"/> method.</para>
  /// </summary>
  [Fact]
  public void ToString_Method() { new Error(1, Guid.Empty.ToString()).ToString().Should().Be(Guid.Empty.ToString()); }
}

/// <summary>
///   <para>Tests set for class <see cref="Error.Info"/>.</para>
/// </summary>
public sealed class ErrorInfoTests : UnitTest<Error.Info>
{
  /// <summary>
  ///   <para>Performs testing of <see cref="Error.Info.Code"/> property.</para>
  /// </summary>
  [Fact]
  public void Code_Property() { new Error.Info {Code = int.MaxValue}.Code.Should().Be(int.MaxValue); }

  /// <summary>
  ///   <para>Performs testing of <see cref="Error.Info.Text"/> property.</para>
  /// </summary>
  [Fact]
  public void Text_Property() { new Error.Info {Text = Guid.Empty.ToString()}.Text.Should().Be(Guid.Empty.ToString()); }

  /// <summary>
  ///   <para>Performs testing of class constructor(s).</para>
  /// </summary>
  /// <seealso cref="Error.Info()"/>
  [Fact]
  public void Constructors()
  {
    var info = new Error.Info();
    info.Code.Should().BeNull();
    info.Text.Should().BeNull();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="Error.Info.ToResult()"/> method.</para>
  /// </summary>
  [Fact]
  public void ToResult_Method()
  {
    var result = new Error.Info().ToResult();
    result.Should().NotBeNull().And.BeOfType<Error>();
    result.Code.Should().Be(0);
    result.Text.Should().BeEmpty();
  }

  /// <summary>
  ///   <para>Performs testing of serialization/deserialization process.</para>
  /// </summary>
  [Fact]
  public void Serialization()
  {
    var info = new Error.Info();
    info.Should().BeDataContractSerializable().And.BeXmlSerializable().And.BeJsonSerializable();
  }
}