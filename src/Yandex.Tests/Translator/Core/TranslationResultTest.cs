using Catharsis.Commons;
using Catharsis.Extensions;
using FluentAssertions;
using FluentAssertions.Json;
using Xunit;
using Yandex.Translator;

namespace Yandex.Tests.Translator;

/// <summary>
///   <para>Tests set for class <see cref="TranslationResult"/>.</para>
/// </summary>
public sealed class TranslationResultTest : ClassTest<TranslationResult>
{
  /// <summary>
  ///   <para>Performs testing of <see cref="TranslationResult.Code"/> property.</para>
  /// </summary>
  [Fact]
  public void Code_Property() { new TranslationResult(new {Code = int.MaxValue}).Code.Should().Be(int.MaxValue); }

  /// <summary>
  ///   <para>Performs testing of <see cref="TranslationResult.Language"/>.</para>
  /// </summary>
  [Fact]
  public void Language_Property() { new TranslationResult(new {Language = Guid.Empty.ToString()}).Language.Should().Be(Guid.Empty.ToString()); }

  /// <summary>
  ///   <para>Performs testing of <see cref="TranslationResult.Lines"/> property.</para>
  /// </summary>
  [Fact]
  public void Lines_Property()
  {
    var result = new TranslationResult(new {});
    result.Lines.Should().BeEmpty();

    var lines = result.Lines.To<List<string>>();
    lines.Add("line");
    result.Lines.Single().Should().Be("line");
    lines.Remove("line");
    result.Lines.Should().BeEmpty();
  }


  /// <summary>
  ///   <para>Performs testing of class constructor(s).</para>
  /// </summary>
  /// <seealso cref="TranslationResult(int, string, IEnumerable{string})"/>
  /// <seealso cref="TranslationResult(TranslationResult.Info)"/>
  /// <seealso cref="TranslationResult(object)"/>
  [Fact]
  public void Constructors()
  {
    var result = new TranslationResult(int.MaxValue, Guid.Empty.ToString(), Enumerable.Empty<string>());
    result.Code.Should().Be(int.MaxValue);
    result.Language.Should().Be(Guid.Empty.ToString());
    result.Lines.Should().BeEmpty();

    result = new TranslationResult(new TranslationResult.Info());
    result.Code.Should().Be(0);
    result.Language.Should().BeEmpty();
    result.Lines.Should().BeEmpty();

    result = new TranslationResult(new {});
    result.Code.Should().Be(0);
    result.Language.Should().BeEmpty();
    result.Lines.Should().BeEmpty();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TranslationResult.ToString()"/> method.</para>
  /// </summary>
  [Fact]
  public void ToString_Method() { new TranslationResult(new {Lines = new List<string> {"first", "second"}}).ToString().Should().Be("firstsecond"); }
}

/// <summary>
///   <para>Tests set for class <see cref="TranslationResult.Info"/>.</para>
/// </summary>
public sealed class TranslationResultInfoTests : ClassTest<TranslationResult.Info>
{
  /// <summary>
  ///   <para>Performs testing of <see cref="TranslationResult.Info.Code"/> property.</para>
  /// </summary>
  [Fact]
  public void Code_Property() { new TranslationResult.Info {Code = int.MaxValue}.Code.Should().Be(int.MaxValue); }

  /// <summary>
  ///   <para>Performs testing of <see cref="TranslationResult.Info.Language"/>.</para>
  /// </summary>
  [Fact]
  public void Language_Property() { new TranslationResult.Info {Language = Guid.Empty.ToString()}.Language.Should().Be(Guid.Empty.ToString()); }

  /// <summary>
  ///   <para>Performs testing of <see cref="TranslationResult.Info.Lines"/> property.</para>
  /// </summary>
  [Fact]
  public void Lines_Property()
  {
    var lines = new List<string>();
    new TranslationResult.Info {Lines = lines}.Lines.Should().BeSameAs(lines);
  }

  /// <summary>
  ///   <para>Performs testing of class constructor(s).</para>
  /// </summary>
  /// <seealso cref="TranslationResult.Info()"/>
  [Fact]
  public void Constructors()
  {
    var info = new TranslationResult.Info();
    info.Code.Should().BeNull();
    info.Language.Should().BeNull();
    info.Lines.Should().BeNull();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TranslationResult.Info.ToResult()"/> method.</para>
  /// </summary>
  [Fact]
  public void ToResult_Method()
  {
    var result = new TranslationResult.Info().ToResult();
    result.Should().NotBeNull().And.BeOfType<TranslationResult>();
    result.Code.Should().Be(0);
    result.Language.Should().BeEmpty();
    result.Lines.Should().BeEmpty();
  }

  /// <summary>
  ///   <para>Performs testing of serialization/deserialization process.</para>
  /// </summary>
  [Fact]
  public void Serialization()
  {
    var info = new TranslationResult.Info();
    info.Should().BeDataContractSerializable().And.BeXmlSerializable().And.BeJsonSerializable();
  }
}