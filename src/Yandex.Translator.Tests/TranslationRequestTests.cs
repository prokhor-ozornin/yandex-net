using System;
using System.Linq;
using Xunit;

namespace Yandex.Translator
{
  /// <summary>
  ///   <para>Tests set for class <see cref="TranslationRequest"/>.</para>
  /// </summary>
  public sealed class TranslationRequestTests
  {
    /// <summary>
    ///   <para>Performs testing of class constructor(s).</para>
    /// </summary>
    /// <seealso cref="TranslationRequest()"/>
    [Fact]
    public void Constructors()
    {
      var request = new TranslationRequest();
      Assert.Empty(request.Parameters);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="TranslationRequest.Format(string)"/> method.</para>
    /// </summary>
    [Fact]
    public void Format_Method()
    {
      Assert.Throws<ArgumentNullException>(() => new TranslationRequest().Format(null));
      Assert.Throws<ArgumentException>(() => new TranslationRequest().Format(string.Empty));

      var request = new TranslationRequest();
      Assert.False(request.Parameters.ContainsKey("format"));
      Assert.True(ReferenceEquals(request, request.Format("format")));
      Assert.Equal("format", request.Parameters["format"]);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="TranslationRequest.From(string)"/> method.</para>
    /// </summary>
    [Fact]
    public void From_Method()
    {
      Assert.Throws<ArgumentNullException>(() => new TranslationRequest().From(null));
      Assert.Throws<ArgumentException>(() => new TranslationRequest().From(string.Empty));

      var request = new TranslationRequest();
      Assert.False(request.Parameters.ContainsKey("lang"));
      Assert.True(ReferenceEquals(request, request.From("from")));
      Assert.Equal("from", request.Parameters["lang"]);
      Assert.Equal("from-to", request.To("to").Parameters["lang"]);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="TranslationRequest.Text(string)"/> method.</para>
    /// </summary>
    [Fact]
    public void Text_Method()
    {
      Assert.Throws<ArgumentNullException>(() => new TranslationRequest().Text(null));
      Assert.Throws<ArgumentException>(() => new TranslationRequest().Text(string.Empty));

      var request = new TranslationRequest();
      Assert.False(request.Parameters.ContainsKey("text"));
      Assert.True(ReferenceEquals(request, request.Text("text")));
      Assert.Equal("text", request.Parameters["text"]);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="TranslationRequest.To(string)"/> method.</para>
    /// </summary>
    [Fact]
    public void To_Method()
    {
      Assert.Throws<ArgumentNullException>(() => new TranslationRequest().To(null));
      Assert.Throws<ArgumentException>(() => new TranslationRequest().To(string.Empty));

      var request = new TranslationRequest();
      Assert.False(request.Parameters.ContainsKey("lang"));
      Assert.True(ReferenceEquals(request, request.To("to")));
      Assert.Equal("to", request.Parameters["lang"]);
      Assert.Equal("from-to", request.From("from").Parameters["lang"]);
    }
  }
}