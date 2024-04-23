using Catharsis.Commons;
using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Json;
using Xunit;
using Yandex.Translator;

namespace Yandex.Tests.Translator;

/// <summary>
///   <para>Tests set for class <see cref="Error"/>.</para>
/// </summary>
public sealed class ErrorTest : ClassTest<Error>
{
  /// <summary>
  ///   <para>Performs testing of class constructor(s).</para>
  /// </summary>
  /// <seealso cref="Error()"/>
  [Fact]
  public void Constructors()
  {
    typeof(Error).Should().BeDerivedFrom<object>().And.Implement<IError>();

    var error = new Error();
    error.Code.Should().Be(default);
    error.Text.Should().BeNull();

    error = new Error(int.MaxValue, "text");
    error.Code.Should().Be(int.MaxValue);
    error.Text.Should().Be("text");
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="Error.Code"/> property.</para>
  /// </summary>
  [Fact]
  public void Code_Property()
  {
    new Error { Code = int.MaxValue }.Code.Should().Be(int.MaxValue);
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
  ///   <para>Performs testing of <see cref="Error.CompareTo(IError)"/> method.</para>
  /// </summary>
  [Fact]
  public void CompareTo_Method()
  {
    TestCompareTo(nameof(Error.Code), 1, 2);
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="Error.Equals(IError)"/></description></item>
  ///     <item><description><see cref="Error.Equals(object)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void Equals_Methods()
  {
    TestEquality(nameof(Error.Code), 1, 2);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="Error.GetHashCode()"/> method.</para>
  /// </summary>
  [Fact]
  public void GetHashCode_Method()
  {
    TestHashCode(nameof(Error.Code), 1, 2);
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
      Validate(string.Empty, new Error { Text = string.Empty });
      Validate("text", new Error { Text = "text" });
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