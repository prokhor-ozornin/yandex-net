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
  ///   <para>Performs testing of class constructor(s).</para>
  /// </summary>
  /// <seealso cref="Error()"/>
  /// <seealso cref="Error(string)"/>
  [Fact]
  public void Constructors()
  {
    typeof(Error).Should().BeDerivedFrom<object>().And.Implement<IError>();

    var error = new Error();
    error.Text.Should().BeNull();

    error = new Error("text");
    error.Text.Should().Be("text");
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="Error.Text"/> property.</para>
  /// </summary>
  [Fact]
  public void Text_Property()
  {
    new Error { Text = "text" }.Text.Should().Be("text");
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="Error.Equals(Error)"/></description></item>
  ///     <item><description><see cref="Error.Equals(object)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void Equals_Methods()
  {
    TestEquality(nameof(Error.Text), "first", "second");
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="Error.GetHashCode()"/> method.</para>
  /// </summary>
  [Fact]
  public void GetHashCode_Method()
  {
    TestHashCode(nameof(Error.Text), "first", "second");
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="Error.ToString()"/> method.</para>
  /// </summary>
  [Fact]
  public void ToString_Method()
  {
    using (new AssertionScope())
    {
      Validate(string.Empty, new Error());
      Validate(string.Empty, new Error(string.Empty));
      Validate("text", new Error("text"));
    }

    return;

    static void Validate(string value, object instance) => instance.ToString().Should().Be(value);
  }

  /// <summary>
  ///   <para>Performs testing of serialization/deserialization process.</para>
  /// </summary>
  [Fact]
  public void Serialization()
  {
    using (new AssertionScope())
    {
      Validate(new Error());
    }

    return;

    static void Validate(object instance) => instance.Should().BeDataContractSerializable().And.BeXmlSerializable().And.BeJsonSerializable();
  }
}