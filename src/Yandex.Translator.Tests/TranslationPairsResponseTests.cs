using System.Linq;
using Xunit;

namespace Yandex.Translator
{
  /// <summary>
  ///   <para>Tests set for class <see cref="TranslationPairsResult"/>.</para>
  /// </summary>
  public sealed class TranslationPairsResponseTests
  {
    /// <summary>
    ///   <para>Performs testing of class constructor(s).</para>
    /// </summary>
    /// <seealso cref="TranslationPairsResult()"/>
    [Fact]
    public void Constructors()
    {
      var response = new TranslationPairsResult();
      Assert.Empty(response.Pairs);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="TranslationPairsResult.Pairs"/> property.</para>
    /// </summary>
    [Fact]
    public void Pairs_Property()
    {
      var response = new TranslationPairsResult();
      Assert.Empty(response.Pairs);
      response.Pairs.Add("pair");
      Assert.Equal("pair", response.Pairs.Single());
      response.Pairs.Remove("pair");
      Assert.Empty(response.Pairs);
    }
  }
}