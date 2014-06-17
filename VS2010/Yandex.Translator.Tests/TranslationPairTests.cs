using System;
using Xunit;

namespace Yandex.Translator
{
  /// <summary>
  ///   <para>Tests set for class <see cref="TranslationPair"/>.</para>
  /// </summary>
  public sealed class TranslationPairTests
  {
    /// <summary>
    ///   <para>Performs testing of class constructor(s).</para>
    /// </summary>
    /// <seealso cref="TranslationPair(string, string)"/>
    [Fact]
    public void Constructors()
    {
      Assert.Throws<ArgumentNullException>(() => new TranslationPair(null, "toLanguage"));
      Assert.Throws<ArgumentNullException>(() => new TranslationPair("fromLanguage", null));
      Assert.Throws<ArgumentException>(() => new TranslationPair(string.Empty, "toLanguage"));
      Assert.Throws<ArgumentException>(() => new TranslationPair("fromLanguage", string.Empty));

      var pair = new TranslationPair("fromLanguage", "toLanguage");
      Assert.Equal("fromLanguage", pair.FromLanguage);
      Assert.Equal("toLanguage", pair.ToLanguage);
    }

    /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="TranslationPair.Equals(TranslationPair)"/></description></item>
    ///     <item><description><see cref="TranslationPair.Equals(object)"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void Equals_Methods()
    {
      Assert.Equal(new TranslationPair("fromLanguage", "toLanguage"), new TranslationPair("fromLanguage", "toLanguage"));
      Assert.NotEqual(new TranslationPair("first", "toLanguage"), new TranslationPair("second", "toLanguage"));
      Assert.NotEqual(new TranslationPair("fromLanguage", "first"), new TranslationPair("fromLanguage", "second"));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="TranslationPair.GetHashCode()"/> method.</para>
    /// </summary>
    [Fact]
    public void GetHashCode_Method()
    {
      Assert.Equal(new TranslationPair("fromLanguage", "toLanguage").GetHashCode(), new TranslationPair("fromLanguage", "toLanguage").GetHashCode());
      Assert.NotEqual(new TranslationPair("first", "toLanguage").GetHashCode(), new TranslationPair("second", "toLanguage").GetHashCode());
      Assert.NotEqual(new TranslationPair("fromLanguage", "first").GetHashCode(), new TranslationPair("fromLanguage", "second").GetHashCode());
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="TranslationPair.ToString()"/> method.</para>
    /// </summary>
    [Fact]
    public void ToString_Method()
    {
      Assert.Equal("fromLanguage-toLanguage", new TranslationPair("fromLanguage", "toLanguage").ToString());
    }
  }
}