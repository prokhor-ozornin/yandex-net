using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;
using Yandex.Translator;

namespace Yandex.Tests.Translator;

/// <summary>
///   <para>Tests set for class <see cref="TranslationPair"/>.</para>
/// </summary>
/// <seealso cref="TranslationPair"/>
public sealed class TranslationPairTest : Test
{
  /// <summary>
  ///   <para>Performs testing of class constructor(s).</para>
  /// </summary>
  /// <seealso cref="TranslationPair(string, string)"/>
  [Fact]
  public void Constructors()
  {
    typeof(TranslationPair).Should().BeDerivedFrom<object>().And.Implement<ITranslationPair>();

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => new TranslationPair(null, "to")).ThrowExactly<ArgumentNullException>().WithParameterName("fromLanguage");
      AssertionExtensions.Should(() => new TranslationPair("from", null)).ThrowExactly<ArgumentNullException>().WithParameterName("toLanguage");
      AssertionExtensions.Should(() => new TranslationPair(string.Empty, "ro")).ThrowExactly<ArgumentException>().WithMessage("fromLanguage");
      AssertionExtensions.Should(() => new TranslationPair("from", string.Empty)).ThrowExactly<ArgumentException>().WithMessage("toLanguage");

      var pair = new TranslationPair("en", "ru");
      pair.FromLanguage.Should().Be("en");
      pair.ToLanguage.Should().Be("ru");
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TranslationPair.ToString()"/> method.</para>
  /// </summary>
  [Fact]
  public void ToString_Method()
  {
    using (new AssertionScope())
    {
      Test("en-ru", new TranslationPair("en", "ru"));
    }

    return;

    static void Test(string value, TranslationPair pair) => pair.ToString().Should().Be(value);
  }
}