using System.Diagnostics;
using Catharsis.Commons;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;
using Yandex.Translator;

namespace Yandex.Tests.Translator;

/// <summary>
///   <para>Tests set for class <see cref="TranslationApiRequest"/>.</para>
/// </summary>
public sealed class TranslationApiRequestTest : UnitTest
{
  /// <summary>
  ///   <para>Performs testing of class constructor(s).</para>
  /// </summary>
  /// <seealso cref="TranslationApiRequest()"/>
  [Fact]
  public void Constructors()
  {
    var request = new TranslationApiRequest();
    request.Parameters.Should().BeEmpty();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TranslationApiRequest.Format(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void Format_Method()
  {
    using (new AssertionScope())
    {
      var request = new TranslationApiRequest();

      request.Parameters.Should().BeEmpty();

      request.Format(null).Should().NotBeNull().And.BeSameAs(request);
      request.Parameters["format"].Should().BeNull();

      request.Format("html").Should().NotBeNull().And.BeSameAs(request);
      request.Parameters["format"].Should().Be("html");
    }

    return;

    static void Validate()
    {

    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TranslationApiRequest.From(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void From_Method()
  {
    using (new AssertionScope())
    {
      var request = new TranslationApiRequest();

      request.Parameters.Should().BeEmpty();

      request.From(null).Should().NotBeNull().And.BeSameAs(request);
      request.Parameters["lang"].Should().BeNull();

      request.From("en").Should().NotBeNull().And.BeSameAs(request);
      request.Parameters["lang"].Should().Be("en");

      request.From("to").Should().NotBeNull().And.BeSameAs(request);
      request.Parameters["lang"].Should().Be("en-ru");
    }

    return;

    static void Validate()
    {

    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TranslationApiRequest.To(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void To_Method()
  {
    using (new AssertionScope())
    {
      var request = new TranslationApiRequest();

      request.Parameters.Should().BeEmpty();

      request.To(null).Should().NotBeNull().And.BeSameAs(request);
      request.Parameters["lang"].Should().BeNull();

      request.To("ru").Should().NotBeNull().And.BeSameAs(request);
      request.Parameters["lang"].Should().Be("ru");

      request.From("en").Should().NotBeNull().And.BeSameAs(request);
      request.Parameters["lang"].Should().Be("en-ru");
    }

    return;

    static void Validate()
    {

    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TranslationApiRequest.Text(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void Text_Method()
  {
    using (new AssertionScope())
    {
      var request = new TranslationApiRequest();

      request.Parameters.Should().BeEmpty();

      request.Text(null).Should().NotBeNull().And.BeSameAs(request);
      request.Parameters["text"].Should().BeNull();

      request.Text("text").Should().NotBeNull().And.BeSameAs(request);
      request.Parameters["text"].Should().Be("text");
    }

    return;

    static void Validate()
    {

    }
  }
}