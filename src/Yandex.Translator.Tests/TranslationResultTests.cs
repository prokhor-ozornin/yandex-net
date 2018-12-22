using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Yandex.Translator
{
  /// <summary>
  ///   <para>Tests set for class <see cref="TranslationResult"/>.</para>
  /// </summary>
  public sealed class TranslationResultTests : UnitTestsBase<TranslationResult>
  {
    /// <summary>
    ///   <para>Performs testing of class constructor(s).</para>
    /// </summary>
    /// <seealso cref="TranslationResult()"/>
    /// <seealso cref="TranslationResult(int, string, string)"/>
    [Fact]
    public void Constructors()
    {
      var result = new TranslationResult();
      Assert.Equal(0, result.Code);
      Assert.Null(result.Language);
      Assert.Empty(result.Lines);

      result = new TranslationResult(1, "language", "text");
      Assert.Equal(1, result.Code);
      Assert.Equal("language", result.Language);
      Assert.Equal("text", result.Lines.Single());
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="TranslationResult.Code"/> property.</para>
    /// </summary>
    [Fact]
    public void Code_Property()
    {
      Assert.Equal(1, new TranslationResult { Code = 1 }.Code);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="TranslationResult.Language"/>.</para>
    /// </summary>
    [Fact]
    public void Language_Property()
    {
      Assert.Equal("language", new TranslationResult { Language = "language" }.Language);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="TranslationResult.Lines"/> property.</para>
    /// </summary>
    [Fact]
    public void Lines_Property()
    {
      var result = new TranslationResult();
      Assert.Empty(result.Lines);
      result.Lines.Add("line");
      Assert.Equal("line", result.Lines.Single());
      result.Lines.Remove("line");
      Assert.Empty(result.Lines);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="TranslationResult.ToString()"/> method.</para>
    /// </summary>
    [Fact]
    public void ToString_Method()
    {
      Assert.Equal("firstsecond", new TranslationResult { Lines = new List<string> { "first", "second" } }.Text);
    }
  }
}