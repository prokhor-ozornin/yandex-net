using FluentAssertions;
using Xunit;
using Yandex.Translator;

namespace Yandex.Tests.Translator;

/// <summary>
///   <para>Tests set for class <see cref="TranslationPair"/>.</para>
/// </summary>
public sealed class TranslationPairTest : UnitTest
{
  /// <summary>
  ///   <para>Performs testing of class constructor(s).</para>
  /// </summary>
  /// <seealso cref="TranslationPair(string, string)"/>
  [Fact]
  public void Constructors()
  {
    AssertionExtensions.Should(() => new TranslationPair(null, "to")).ThrowExactly<ArgumentNullException>().WithParameterName("fromLanguage");
    AssertionExtensions.Should(() => new TranslationPair("from", null)).ThrowExactly<ArgumentNullException>().WithParameterName("toLanguage");
    AssertionExtensions.Should(() => new TranslationPair(string.Empty, "ro")).ThrowExactly<ArgumentException>().WithParameterName("fromLanguage");
    AssertionExtensions.Should(() => new TranslationPair("from", string.Empty)).ThrowExactly<ArgumentException>().WithParameterName("toLanguage");

    var pair = new TranslationPair("en", "ru");
    pair.FromLanguage.Should().Be("en");
    pair.ToLanguage.Should().Be("ru");
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="TranslationPair.Equals(ITranslationPair)"/></description></item>
  ///     <item><description><see cref="TranslationPair.Equals(object)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void Equals_Methods()
  {
    new TranslationPair("fromLanguage", "toLanguage").Should().Be(new TranslationPair("fromLanguage", "toLanguage"));
    new TranslationPair("first", "toLanguage").Should().NotBe(new TranslationPair("second", "toLanguage"));
    new TranslationPair("fromLanguage", "first").Should().NotBe(new TranslationPair("fromLanguage", "second"));
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TranslationPair.GetHashCode()"/> method.</para>
  /// </summary>
  [Fact]
  public void GetHashCode_Method()
  {
    new TranslationPair("fromLanguage", "toLanguage").GetHashCode().Should().Be(new TranslationPair("fromLanguage", "toLanguage").GetHashCode());
    new TranslationPair("first", "toLanguage").GetHashCode().Should().Be(new TranslationPair("second", "toLanguage").GetHashCode());
    new TranslationPair("fromLanguage", "first").GetHashCode().Should().Be(new TranslationPair("fromLanguage", "second").GetHashCode());
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TranslationPair.ToString()"/> method.</para>
  /// </summary>
  [Fact]
  public void ToString_Method() { new TranslationPair("en", "ru").ToString().Should().Be("en-ru"); }
}