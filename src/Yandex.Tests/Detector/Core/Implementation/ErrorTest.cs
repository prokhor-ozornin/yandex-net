using Catharsis.Commons;
using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Json;
using Xunit;
using Yandex.Detector;

namespace Yandex.Tests.Detector.Core.Implementation;

/// <summary>
///   <para>Tests set for class <see cref="Error"/>.</para>
/// </summary>
public sealed class ErrorTest : ClassTest<Error>
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
  ///     <item><description><see cref="Error.Equals(Error)"/></description></item>
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
  public void ToString_Method()
  {
    using (new AssertionScope())
    {
      Validate(string.Empty, new Error(new {}));
      Validate(string.Empty, new Error(new { Text = string.Empty }));
      Validate("text", new Error(new { Text = "text" }));
    }

    return;

    static void Validate(string value, object instance) => instance.ToString().Should().Be(value);
  }
}

/// <summary>
///   <para>Tests set for class <see cref="Error.Info"/>.</para>
/// </summary>
public sealed class ErrorInfoTests : ClassTest<Error.Info>
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
    using (new AssertionScope())
    {
      var result = new Error.Info().ToResult();
      result.Should().NotBeNull().And.BeOfType<Error>();
      result.Text.Should().BeNull();
    }

    return;

    static void Validate()
    {

    }
  }

  /// <summary>
  ///   <para>Performs testing of serialization/deserialization process.</para>
  /// </summary>
  [Fact]
  public void Serialization()
  {
    using (new AssertionScope())
    {
      Validate(new Error.Info());
    }

    return;

    static void Validate(object instance) => instance.Should().BeDataContractSerializable().And.BeXmlSerializable().And.BeJsonSerializable();
  }
}