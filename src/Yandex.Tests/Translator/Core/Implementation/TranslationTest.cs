using Catharsis.Commons;
using FluentAssertions;
using FluentAssertions.Execution;
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
    AssertionExtensions.Should(() => new Translation(string.Empty, "ru", "text")).ThrowExactly<ArgumentException>().WithMessage("fromLanguage");
    AssertionExtensions.Should(() => new Translation("en", null, "text")).ThrowExactly<ArgumentNullException>().WithParameterName("toLanguage");
    AssertionExtensions.Should(() => new Translation("en", string.Empty, "text")).ThrowExactly<ArgumentException>().WithMessage("toLanguage");
    AssertionExtensions.Should(() => new Translation("en", "ru", null)).ThrowExactly<ArgumentNullException>().WithParameterName("text");

    typeof(Translation).Should().BeDerivedFrom<object>().And.Implement<ITranslation>();

    var translation = new Translation("en", "ru", "text");
    translation.FromLanguage.Should().Be("en");
    translation.Text.Should().Be("text");
    translation.ToLanguage.Should().Be("ru");
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="Translation.ToString()"/> method.</para>
  /// </summary>
  [Fact]
  public void ToString_Method()
  {
    using (new AssertionScope())
    {
      Validate("text", new Translation("en", "ru", "text"));
    }

    return;

    static void Validate(string value, object instance) => instance.ToString().Should().Be(value);
  }
}