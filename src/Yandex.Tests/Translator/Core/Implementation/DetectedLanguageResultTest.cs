using System.Runtime.Serialization;
using Catharsis.Commons;
using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Json;
using Xunit;
using Yandex.Translator;

namespace Yandex.Tests.Translator;

/// <summary>
///   <para>Tests set for class <see cref="DetectedLanguageResult"/>.</para>
/// </summary>
public sealed class DetectedLanguageResultTest : UnitTest
{
  /// <summary>
  ///   <para>Performs testing of class constructor(s).</para>
  /// </summary>
  /// <seealso cref="DetectedLanguageResult(int, string)"/>
  /// <seealso cref="DetectedLanguageResult(DetectedLanguageResult.Info)"/>
  /// <seealso cref="DetectedLanguageResult(object)"/>
  [Fact]
  public void Constructors()
  {
    typeof(DetectedLanguageResult).Should().BeDerivedFrom<object>();

    var result = new DetectedLanguageResult(int.MaxValue, Guid.Empty.ToString());
    result.Code.Should().Be(int.MaxValue);
    result.Language.Should().Be(Guid.Empty.ToString());

    result = new DetectedLanguageResult(new DetectedLanguageResult.Info());
    result.Code.Should().Be(default);
    result.Language.Should().BeEmpty();

    result = new DetectedLanguageResult(new {});
    result.Code.Should().Be(default);
    result.Language.Should().BeEmpty();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DetectedLanguageResult.Code"/> property.</para>
  /// </summary>
  [Fact]
  public void Code_Property()
  {
    new DetectedLanguageResult(new { Code = int.MaxValue }).Code.Should().Be(int.MaxValue);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DetectedLanguageResult.Language"/> property.</para>
  /// </summary>
  [Fact]
  public void Language_Property()
  {
    new DetectedLanguageResult(new { Language = "en" }).Language.Should().Be("en");
  }
}

/// <summary>
///   <para>Tests set for class <see cref="DetectedLanguageResult.Info"/>.</para>
/// </summary>
public sealed class DetectedLanguageResultInfoTests
{
  /// <summary>
  ///   <para>Performs testing of class constructor(s).</para>
  /// </summary>
  /// <seealso cref="DetectedLanguageResult.Info()"/>
  [Fact]
  public void Constructors()
  {
    typeof(DetectedLanguageResult.Info).Should().BeDerivedFrom<object>().And.Implement<IResultable<DetectedLanguageResult>>().And.BeDecoratedWith<DataContractAttribute>();

    var info = new DetectedLanguageResult.Info();
    info.Code.Should().BeNull();
    info.Language.Should().BeNull();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DetectedLanguageResult.Info.Code"/> property.</para>
  /// </summary>
  [Fact]
  public void Code_Property()
  {
    new DetectedLanguageResult.Info { Code = int.MaxValue }.Code.Should().Be(int.MaxValue);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DetectedLanguageResult.Info.Language"/> property.</para>
  /// </summary>
  [Fact]
  public void Language_Property()
  {
    new DetectedLanguageResult.Info { Language = "en" }.Language.Should().Be("en");
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DetectedLanguageResult.Info.ToResult()"/> method.</para>
  /// </summary>
  [Fact]
  public void ToResult_Method()
  {
    using (new AssertionScope())
    {
      Validate(new DetectedLanguageResult(default, string.Empty), new DetectedLanguageResult.Info());
    }

    return;

    static void Validate(DetectedLanguageResult result, DetectedLanguageResult.Info info)
    {
      var languageResult = info.ToResult();

      languageResult.Should().BeOfType<DetectedLanguageResult>();
      languageResult.Code.Should().Be(result.Code);
      languageResult.Language.Should().Be(result.Language);
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
      Validate(new DetectedLanguageResult.Info());
    }

    return;

    static void Validate(object instance) => instance.Should().BeDataContractSerializable().And.BeXmlSerializable().And.BeJsonSerializable();
  }
}