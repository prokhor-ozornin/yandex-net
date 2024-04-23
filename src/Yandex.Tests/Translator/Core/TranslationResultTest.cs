using System.Runtime.Serialization;
using Catharsis.Commons;
using Catharsis.Extensions;
using FluentAssertions;
using FluentAssertions.Execution;
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
  ///   <para>Performs testing of class constructor(s).</para>
  /// </summary>
  /// <seealso cref="TranslationResult(int, string, IEnumerable{string})"/>
  /// <seealso cref="TranslationResult(TranslationResult.Info)"/>
  /// <seealso cref="TranslationResult(object)"/>
  [Fact]
  public void Constructors()
  {
    typeof(TranslationResult).Should().BeDerivedFrom<object>();

    var result = new TranslationResult(int.MaxValue, "en", []);
    result.Code.Should().Be(int.MaxValue);
    result.Language.Should().Be("en");
    result.Lines.Should().BeEmpty();

    result = new TranslationResult(new TranslationResult.Info());
    result.Code.Should().Be(default);
    result.Language.Should().BeEmpty();
    result.Lines.Should().BeEmpty();

    result = new TranslationResult(new {});
    result.Code.Should().Be(default);
    result.Language.Should().BeEmpty();
    result.Lines.Should().BeEmpty();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TranslationResult.Code"/> property.</para>
  /// </summary>
  [Fact]
  public void Code_Property()
  {
    new TranslationResult(new { Code = int.MaxValue }).Code.Should().Be(int.MaxValue);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TranslationResult.Language"/>.</para>
  /// </summary>
  [Fact]
  public void Language_Property()
  {
    new TranslationResult(new { Language = "en" }).Language.Should().Be("en");
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TranslationResult.Lines"/> property.</para>
  /// </summary>
  [Fact]
  public void Lines_Property()
  {
    var result = new TranslationResult(new { });
    result.Lines.Should().BeEmpty();

    var lines = result.Lines.To<List<string>>();
    lines.Add("line");
    result.Lines.Single().Should().Be("line");
    lines.Remove("line");
    result.Lines.Should().BeEmpty();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TranslationResult.ToString()"/> method.</para>
  /// </summary>
  [Fact]
  public void ToString_Method()
  {
    using (new AssertionScope())
    {
      Validate(string.Empty, new TranslationResult(new { }));
      Validate(string.Empty, new TranslationResult(new { Lines = new List<string>() }));
      Validate("firstsecond", new TranslationResult(new { Lines = new List<string> { "first", "second" } }));
    }

    return;

    static void Validate(string value, object instance) => instance.ToString().Should().Be(value);
  }
}

/// <summary>
///   <para>Tests set for class <see cref="TranslationResult.Info"/>.</para>
/// </summary>
public sealed class TranslationResultInfoTests : ClassTest<TranslationResult.Info>
{
  /// <summary>
  ///   <para>Performs testing of class constructor(s).</para>
  /// </summary>
  /// <seealso cref="TranslationResult.Info()"/>
  [Fact]
  public void Constructors()
  {
    typeof(TranslationResult.Info).Should().BeDerivedFrom<object>().And.Implement<IResultable<TranslationResult>>().And.BeDecoratedWith<DataContractAttribute>();

    var info = new TranslationResult.Info();
    info.Code.Should().BeNull();
    info.Language.Should().BeNull();
    info.Lines.Should().BeNull();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TranslationResult.Info.Code"/> property.</para>
  /// </summary>
  [Fact]
  public void Code_Property()
  {
    new TranslationResult.Info { Code = int.MaxValue }.Code.Should().Be(int.MaxValue);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TranslationResult.Info.Language"/>.</para>
  /// </summary>
  [Fact]
  public void Language_Property()
  {
    new TranslationResult.Info { Language = "en" }.Language.Should().Be("en");
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TranslationResult.Info.Lines"/> property.</para>
  /// </summary>
  [Fact]
  public void Lines_Property()
  {
    var lines = new List<string>();
    new TranslationResult.Info { Lines = lines }.Lines.Should().BeSameAs(lines);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TranslationResult.Info.ToResult()"/> method.</para>
  /// </summary>
  [Fact]
  public void ToResult_Method()
  {
    using (new AssertionScope())
    {
      Validate(new TranslationResult(0, string.Empty, []), new TranslationResult.Info());
    }

    return;

    static void Validate(TranslationResult result, TranslationResult.Info info)
    {
      var translationResult = info.ToResult();

      translationResult.Should().BeOfType<TranslationResult>();
      translationResult.Code.Should().Be(result.Code);
      translationResult.Language.Should().Be(result.Language);
      translationResult.Lines.Should().Equal(result.Lines);
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
      Validate(new TranslationResult.Info());
    }

    return;

    static void Validate(object instance) => instance.Should().BeDataContractSerializable().And.BeXmlSerializable().And.BeJsonSerializable();
  }
}