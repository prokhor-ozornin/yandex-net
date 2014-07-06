using System;
using Xunit;

namespace Yandex.Translator
{
  /// <summary>
  ///   <para>Tests set for class <see cref="Translation"/>.</para>
  /// </summary>
  public sealed class TranslationTests
  {
    /// <summary>
    ///   <para>Performs testing of class constructor(s).</para>
    /// </summary>
    /// <seealso cref="Translation(string, string, string)"/>
    [Fact]
    public void Constructors()
    {
      Assert.Throws<ArgumentNullException>(() => new Translation(null, "toLanguage", "text"));
      Assert.Throws<ArgumentNullException>(() => new Translation("fromLanguage", null, "text"));
      Assert.Throws<ArgumentNullException>(() => new Translation("fromLanguage", "toLanguage", null));
      Assert.Throws<ArgumentException>(() => new Translation(string.Empty, "toLanguage", "text"));
      Assert.Throws<ArgumentException>(() => new Translation("fromLanguage", string.Empty, "text"));
      Assert.Throws<ArgumentException>(() => new Translation("fromLanguage", "toLanguage", string.Empty));

      var translation = new Translation("fromLanguage", "toLanguage", "text");
      Assert.Equal("fromLanguage", translation.FromLanguage);
      Assert.Equal("text", translation.Text);
      Assert.Equal("toLanguage", translation.ToLanguage);
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
      Assert.Equal(new Translation("fromLanguage", "toLanguage", "text"), new Translation("fromLanguage", "toLanguage", "text"));
      Assert.NotEqual(new Translation("first", "toLanguage", "text"), new Translation("second", "toLanguage", "text"));
      Assert.NotEqual(new Translation("fromLanguage", "first", "text"), new Translation("fromLanguage", "second", "text"));
      Assert.NotEqual(new Translation("fromLanguage", "toLanguage", "first"), new Translation("fromLanguage", "toLanguage", "second"));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Translation.GetHashCode()"/> method.</para>
    /// </summary>
    [Fact]
    public void GetHashCode_Method()
    {
      Assert.Equal(new Translation("fromLanguage", "toLanguage", "text").GetHashCode(), new Translation("fromLanguage", "toLanguage", "text").GetHashCode());
      Assert.NotEqual(new Translation("first", "toLanguage", "text").GetHashCode(), new Translation("second", "toLanguage", "text").GetHashCode());
      Assert.NotEqual(new Translation("fromLanguage", "first", "text").GetHashCode(), new Translation("fromLanguage", "second", "text").GetHashCode());
      Assert.NotEqual(new Translation("fromLanguage", "toLanguage", "first").GetHashCode(), new Translation("fromLanguage", "toLanguage", "second").GetHashCode());
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Translation.ToString()"/> method.</para>
    /// </summary>
    [Fact]
    public void ToString_Method()
    {
      Assert.Equal("text", new Translation("fromLanguage", "toLanguage", "text").ToString());
    }
  }
}