using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;
using Yandex.Detector;

namespace Yandex.Tests.Detector;

/// <summary>
///   <para>Tests set for class <see cref="Resolution"/>.</para>
/// </summary>
/// <seealso cref="Resolution"/>
public sealed class ResolutionTest : Test
{
  /// <summary>
  ///   <para>Performs testing of class constructor(s).</para>
  /// </summary>
  /// <seealso cref="Resolution()"/>
  [Fact]
  public void Constructors()
  {
    typeof(Resolution).Should().BeDerivedFrom<object>().And.Implement<IResolution>();

    using (new AssertionScope())
    {
      var resolution = new Resolution();

      resolution.Height.Should().Be(0);
      resolution.Width.Should().Be(0);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="Resolution.Height"/> property.</para>
  /// </summary>
  [Fact]
  public void Height_Property() => new Resolution { Height = short.MaxValue }.Height.Should().Be(short.MaxValue);

  /// <summary>
  ///   <para>Performs testing of <see cref="Resolution.Width"/> property.</para>
  /// </summary>
  [Fact]
  public void Width_Property() => new Resolution { Width = short.MaxValue }.Width.Should().Be(short.MaxValue);

  /// <summary>
  ///   <para>Performs testing of <see cref="Resolution.ToString()"/> method.</para>
  /// </summary>
  [Fact]
  public void ToString_Method()
  {
    using (new AssertionScope())
    {
      Test("0x0", new Resolution());
      Test($"{short.MinValue}x{short.MaxValue}", new Resolution { Width = short.MinValue, Height = short.MaxValue });
    }

    return;

    static void Test(string result, Resolution resolution) => resolution.ToString().Should().Be(result);
  }
}