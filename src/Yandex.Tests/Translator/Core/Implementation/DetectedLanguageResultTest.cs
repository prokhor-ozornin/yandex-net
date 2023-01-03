using FluentAssertions;
using FluentAssertions.Json;
using Xunit;
using Yandex.Translator;

namespace Yandex.Tests.Translator;

/// <summary>
///   <para>Tests set for class <see cref="DetectedLanguageResult"/>.</para>
/// </summary>
public sealed class DetectedLanguageResultTest
{
  /// <summary>
  ///   <para>Performs testing of <see cref="DetectedLanguageResult.Code"/> property.</para>
  /// </summary>
  [Fact]
  public void Code_Property() { new DetectedLanguageResult(new {Code = int.MaxValue}).Code.Should().Be(int.MaxValue); }

  /// <summary>
  ///   <para>Performs testing of <see cref="DetectedLanguageResult.Language"/> property.</para>
  /// </summary>
  [Fact]
  public void Language_Property() { new DetectedLanguageResult(new {Language = Guid.Empty.ToString()}).Language.Should().Be(Guid.Empty.ToString()); }

  /// <summary>
  ///   <para>Performs testing of class constructor(s).</para>
  /// </summary>
  /// <seealso cref="DetectedLanguageResult(int, string)"/>
  /// <seealso cref="DetectedLanguageResult(DetectedLanguageResult.Info)"/>
  /// <seealso cref="DetectedLanguageResult(object)"/>
  [Fact]
  public void Constructors()
  {
    var result = new DetectedLanguageResult(int.MaxValue, Guid.Empty.ToString());
    result.Code.Should().Be(int.MaxValue);
    result.Language.Should().Be(Guid.Empty.ToString());

    result = new DetectedLanguageResult(new DetectedLanguageResult.Info());
    result.Code.Should().Be(0);
    result.Language.Should().BeEmpty();

    result = new DetectedLanguageResult(new {});
    result.Code.Should().Be(0);
    result.Language.Should().BeEmpty();
  }
}

/// <summary>
///   <para>Tests set for class <see cref="DetectedLanguageResult.Info"/>.</para>
/// </summary>
public sealed class DetectedLanguageResultInfoTests
{
  /// <summary>
  ///   <para>Performs testing of <see cref="DetectedLanguageResult.Info.Code"/> property.</para>
  /// </summary>
  [Fact]
  public void Code_Property() { new DetectedLanguageResult.Info {Code = int.MaxValue}.Code.Should().Be(int.MaxValue); }

  /// <summary>
  ///   <para>Performs testing of <see cref="DetectedLanguageResult.Info.Language"/> property.</para>
  /// </summary>
  [Fact]
  public void Language_Property() { new DetectedLanguageResult.Info {Language = Guid.Empty.ToString()}.Language.Should().Be(Guid.Empty.ToString()); }

  /// <summary>
  ///   <para>Performs testing of class constructor(s).</para>
  /// </summary>
  /// <seealso cref="DetectedLanguageResult.Info()"/>
  [Fact]
  public void Constructors()
  {
    var info = new DetectedLanguageResult.Info();
    info.Code.Should().BeNull();
    info.Language.Should().BeNull();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DetectedLanguageResult.Info.ToResult()"/> method.</para>
  /// </summary>
  [Fact]
  public void ToResult_Method()
  {
    var result = new DetectedLanguageResult.Info().ToResult();
    result.Should().NotBeNull().And.BeOfType<DetectedLanguageResult>();
    result.Code.Should().Be(0);
    result.Language.Should().BeEmpty();
  }

  /// <summary>
  ///   <para>Performs testing of serialization/deserialization process.</para>
  /// </summary>
  [Fact]
  public void Serialization()
  {
    var info = new DetectedLanguageResult.Info();
    info.Should().BeDataContractSerializable().And.BeXmlSerializable().And.BeJsonSerializable();
  }
}