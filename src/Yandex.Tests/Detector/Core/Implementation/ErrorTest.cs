using FluentAssertions;
using Xunit;
using Yandex.Detector;

namespace Yandex.Tests.Detector.Core.Implementation;

/// <summary>
///   <para>Tests set for class <see cref="Error"/>.</para>
/// </summary>
public sealed class ErrorTest : EntityTest<Error>
{
  /// <summary>
  ///   <para>Performs testing of <see cref="Error.Text"/> property.</para>
  /// </summary>
  [Fact]
  public void Text_Property() { new Error(new {Text = Guid.Empty.ToString()}).Text.Should().Be(Guid.Empty.ToString()); }

  /// <summary>
  ///   <para>Performs testing of class constructor(s).</para>
  /// </summary>
  /// <seealso cref="Error(string)"/>
  /// <seealso cref="Error(Error.Info)"/>
  /// <seealso cref="Error(object)"/>
  [Fact]
  public void Constructors()
  {
    var error = new Error(Guid.Empty.ToString());
    error.Text.Should().Be(Guid.Empty.ToString());

    error = new Error(new Error.Info());
    error.Text.Should().BeEmpty();

    error = new Error(new {});
    error.Text.Should().BeEmpty();
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="Error.Equals(IError)"/></description></item>
  ///     <item><description><see cref="Error.Equals(object)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void Equals_Methods() { TestEquality(nameof(Error.Text), "first", "second"); }

  /// <summary>
  ///   <para>Performs testing of <see cref="Error.GetHashCode()"/> method.</para>
  /// </summary>
  [Fact]
  public void GetHashCode_Method() { TestHashCode(nameof(Error.Text), "first", "second"); }

  /// <summary>
  ///   <para>Performs testing of <see cref="Error.ToString()"/> method.</para>
  /// </summary>
  [Fact]
  public void ToString_Method() { new Error(new {Text = Guid.Empty.ToString()}).ToString().Should().Be(Guid.Empty.ToString()); }
}

/// <summary>
///   <para>Tests set for class <see cref="Error.Info"/>.</para>
/// </summary>
public sealed class ErrorInfoTests : EntityTest<Error.Info>
{
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
    result.Text.Should().BeNull();
  }

  /// <summary>
  ///   <para>Performs testing of serialization/deserialization process.</para>
  /// </summary>
  [Fact]
  public void Serialization()
  {
    var info = new Error.Info();
    info.Should().BeDataContractSerializable().And.BeXmlSerializable();
  }
}