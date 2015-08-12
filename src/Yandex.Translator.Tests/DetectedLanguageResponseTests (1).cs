using Xunit;

namespace Yandex.Translator
{
  /// <summary>
  ///   <para>Tests set for class <see cref="DetectedLanguageResult"/>.</para>
  /// </summary>
  public sealed class DetectedLanguageResponseTests
  {
    /// <summary>
    ///   <para>Performs testing of class constructor(s).</para>
    /// </summary>
    /// <seealso cref="DetectedLanguageResult()"/>
    /// <seealso cref="DetectedLanguageResult(int, string)"/>
    [Fact]
    public void Constructors()
    {
      var response = new DetectedLanguageResult();
      Assert.Equal(0, response.Code);
      Assert.Null(response.Language);

      response = new DetectedLanguageResult(1, "language");
      Assert.Equal(1, response.Code);
      Assert.Equal("language", response.Language);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="DetectedLanguageResult.Code"/> property.</para>
    /// </summary>
    [Fact]
    public void Code_Property()
    {
      Assert.Equal(1, new DetectedLanguageResult { Code = 1 }.Code);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="DetectedLanguageResult.Language"/> property.</para>
    /// </summary>
    [Fact]
    public void Language_Property()
    {
      Assert.Equal("language", new DetectedLanguageResult { Language = "language" }.Language);
    }
  }
}