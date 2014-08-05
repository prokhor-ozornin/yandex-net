using Xunit;

namespace Yandex.Translator
{
  /// <summary>
  ///   <para>Tests set for class <see cref="Error"/>.</para>
  /// </summary>
  public sealed class ErrorTests : UnitTestsBase<Error>
  {
    /// <summary>
    ///   <para>Performs testing of JSON serialization/deserialization process.</para>
    /// </summary>
    [Fact]
    public void Json()
    {
      this.TestJson(new Error(), new { code = 0 });
      this.TestJson(new Error(1, "text"), new { code = 1, text = "text" });
    }

    /// <summary>
    ///   <para>Performs testing of XML serialization/deserialization process.</para>
    /// </summary>
    [Fact]
    public void Xml()
    {
      this.TestXml(new Error(), "Error", new { code = 0 });
      this.TestXml(new Error(1, "text"), "Error", new { code = 1, text = "text" });
    }

    /// <summary>
    ///   <para>Performs testing of class constructor(s).</para>
    /// </summary>
    /// <seealso cref="Error()"/>
    /// <seealso cref="Error(int, string)"/>
    [Fact]
    public void Constructors()
    {
      var error = new Error();
      Assert.Equal(0, error.Code);
      Assert.Null(error.Text);

      error = new Error(1, "text");
      Assert.Equal(1, error.Code);
      Assert.Equal("text", error.Text);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Error.Code"/> property.</para>
    /// </summary>
    [Fact]
    public void Code_Property()
    {
      Assert.Equal(1, new Error { Code = 1 }.Code);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Error.Text"/> property.</para>
    /// </summary>
    [Fact]
    public void Text_Property()
    {
      Assert.Equal("text", new Error { Text = "text" }.Text);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Error.CompareTo(IError)"/> method.</para>
    /// </summary>
    [Fact]
    public void CompareTo_Method()
    {
      this.TestCompareTo("Code", 1, 2);
    }

    /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="Error.Equals(IError)"/></description></item>
    ///     <item><description><see cref="Error.Equals(object)"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void Equals_Methods()
    {
      this.TestEquality("Code", 1, 2);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Error.GetHashCode()"/> method.</para>
    /// </summary>
    [Fact]
    public void GetHashCode_Method()
    {
      this.TestHashCode("Code", 1, 2);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="Error.ToString()"/> method.</para>
    /// </summary>
    [Fact]
    public void ToString_Method()
    {
      Assert.Equal("text", new Error(1, "text").ToString());
    }
  }
}