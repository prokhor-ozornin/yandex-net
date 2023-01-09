using FluentAssertions;
using Xunit;
using Yandex.Translator;

namespace Yandex.Tests.Translator;

/// <summary>
///   <para>Tests set for class <see cref="Translation"/>.</para>
/// </summary>
public sealed class TranslationTest : UnitTest
{
  /// <summary>
  ///   <para>Performs testing of class constructor(s).</para>
  /// </summary>
  /// <seealso cref="Translation(string, string, string)"/>
  [Fact]
  public void Constructors()
  {
    AssertionExtensions.Should(() => new Translation(null, "to", "text")).ThrowExactly<ArgumentNullException>().WithParameterName("fromLanguage");
    AssertionExtensions.Should(() => new Translation("from", null, "text")).ThrowExactly<ArgumentNullException>().WithParameterName("toLanguage");
    AssertionExtensions.Should(() => new Translation("from", "to", null)).ThrowExactly<ArgumentNullException>().WithParameterName("text");
    AssertionExtensions.Should(() => new Translation(string.Empty, "to", "text")).ThrowExactly<ArgumentException>().WithParameterName("fromLanguage");
    AssertionExtensions.Should(() => new Translation("from", string.Empty, "text")).ThrowExactly<ArgumentException>().WithParameterName("toLanguage");

    var translation = new Translation("en", "ru", "text");
    translation.FromLanguage.Should().Be("en");
    translation.Text.Should().Be("text");
    translation.ToLanguage.Should().Be("ru");
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="Translation.Equals(ITranslation)"/></description></item>
  ///     <item><description><see cref="Translation.Equals(object)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void Equals_Methods()
  {
    new Translation("fromLanguage", "toLanguage", "text").Should().Be(new Translation("fromLanguage", "toLanguage", "text"));
    new Translation("first", "toLanguage", "text").Should().NotBe(new Translation("second", "toLanguage", "text"));
    new Translation("fromLanguage", "first", "text").Should().NotBe(new Translation("fromLanguage", "second", "text"));
    new Translation("fromLanguage", "toLanguage", "first").Should().NotBe(new Translation("fromLanguage", "toLanguage", "second"));
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="Translation.GetHashCode()"/> method.</para>
  /// </summary>
  [Fact]
  public void GetHashCode_Method()
  {
    new Translation("fromLanguage", "toLanguage", "text").GetHashCode().Should().Be(new Translation("fromLanguage", "toLanguage", "text").GetHashCode());
    new Translation("first", "toLanguage", "text").GetHashCode().Should().NotBe(new Translation("second", "toLanguage", "text").GetHashCode());
    new Translation("fromLanguage", "first", "text").GetHashCode().Should().NotBe(new Translation("fromLanguage", "second", "text").GetHashCode());
    new Translation("fromLanguage", "toLanguage", "first").GetHashCode().Should().NotBe(new Translation("fromLanguage", "toLanguage", "second").GetHashCode());
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="Translation.ToString()"/> method.</para>
  /// </summary>
  [Fact]
  public void ToString_Method() { new Translation("fromLanguage", "toLanguage", "text").ToString().Should().Be("text"); }
}