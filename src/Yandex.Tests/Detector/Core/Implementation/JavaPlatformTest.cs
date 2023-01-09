using FluentAssertions;
using Xunit;
using Yandex.Detector;

namespace Yandex.Tests.Detector.Core.Implementation;

/// <summary>
///   <para>Tests set for class <see cref="JavaPlatform"/>.</para>
/// </summary>
public sealed class JavaPlatformTest : EntityTest<JavaPlatform>
{
  /// <summary>
  ///   <para>Performs testing of <see cref="JavaPlatform.Camera"/> property.</para>
  /// </summary>
  [Fact]
  public void Camera_Property() { new JavaPlatform(new {Camera = true}).Camera.Should().BeTrue(); }

  /// <summary>
  ///   <para>Performs testing of <see cref="JavaPlatform.FileSystem"/> property.</para>
  /// </summary>
  [Fact]
  public void FileSystem_Property() { new JavaPlatform(new {FileSystem = true}).FileSystem.Should().BeTrue(); }

  /// <summary>
  ///   <para>Performs testing of <see cref="JavaPlatform.Certificate"/> property.</para>
  /// </summary>
  [Fact]
  public void Certificate_Property() { new JavaPlatform(new {Certificate = Guid.Empty.ToString()}).Certificate.Should().Be(Guid.Empty.ToString()); }

  /// <summary>
  ///   <para>Performs testing of <see cref="JavaPlatform.Icon"/> property.</para>
  /// </summary>
  [Fact]
  public void Icon_Property()
  {
    var resolution = new Resolution();
    new JavaPlatform(new {Resolution = resolution}.Resolution).Should().BeSameAs(resolution);
  }

  /// <summary>
  ///   <para>Performs testing of class constructor(s).</para>
  /// </summary>
  /// <seealso cref="JavaPlatform(bool, bool, string, IResolution)"/>
  /// <seealso cref="JavaPlatform(JavaPlatform.Info)"/>
  /// <seealso cref="JavaPlatform(object)"/>
  [Fact]
  public void Constructors()
  {
    var java = new JavaPlatform(true, true, "certificate", new Resolution {Width = short.MinValue, Height = short.MaxValue});
    java.Camera.Should().BeTrue();
    java.FileSystem.Should().BeTrue();
    java.Certificate.Should().Be("certificate");
    java.Icon.Should().Be(new Resolution {Width = short.MinValue, Height = short.MaxValue});

    java = new JavaPlatform(new JavaPlatform.Info());
    java.Camera.Should().BeFalse();
    java.FileSystem.Should().BeFalse();
    java.Certificate.Should().BeNull();
    java.Icon.Should().BeNull();

    java = new JavaPlatform(new {});
    java.Camera.Should().BeFalse();
    java.FileSystem.Should().BeFalse();
    java.Certificate.Should().BeNull();
    java.Icon.Should().BeNull();
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="JavaPlatform.Equals(IJavaPlatform)"/></description></item>
  ///     <item><description><see cref="JavaPlatform.Equals(object)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void Equals_Methods()
  {
    TestEquality(nameof(JavaPlatform.Camera), true, false);
    TestEquality(nameof(JavaPlatform.FileSystem), true, false);
    TestEquality(nameof(JavaPlatform.Certificate), "first", "second");
    TestEquality(nameof(JavaPlatform.Icon), new Resolution {Height = 1, Width = 1}, new Resolution {Height = 2, Width = 2});
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="JavaPlatform.GetHashCode()"/> method.</para>
  /// </summary>
  [Fact]
  public void GetHashCode_Method()
  {
    TestHashCode(nameof(JavaPlatform.Camera), true, false);
    TestHashCode(nameof(JavaPlatform.FileSystem), true, false);
    TestHashCode(nameof(JavaPlatform.Certificate), "first", "second");
    TestHashCode(nameof(JavaPlatform.Icon), new Resolution {Height = 1, Width = 1}, new Resolution {Height = 2, Width = 2});
  }
}

/// <summary>
///   <para>Tests set for class <see cref="JavaPlatform.Info"/>.</para>
/// </summary>
public sealed class JavaPlatformInfoTests : EntityTest<JavaPlatform.Info>
{
  /// <summary>
  ///   <para>Performs testing of <see cref="JavaPlatform.Info.Camera"/> property.</para>
  /// </summary>
  [Fact]
  public void Camera_Property() { new JavaPlatform.Info {Camera = byte.MaxValue}.Camera.Should().Be(byte.MaxValue); }

  /// <summary>
  ///   <para>Performs testing of <see cref="JavaPlatform.Info.FileSystem"/> property.</para>
  /// </summary>
  [Fact]
  public void FileSystem_Property() { new JavaPlatform.Info {FileSystem = byte.MaxValue}.FileSystem.Should().Be(byte.MaxValue); }

  /// <summary>
  ///   <para>Performs testing of <see cref="JavaPlatform.Info.Certificate"/> property.</para>
  /// </summary>
  [Fact]
  public void Certificate_Property() { new JavaPlatform.Info {Certificate = Guid.Empty.ToString()}.Certificate.Should().Be(Guid.Empty.ToString()); }

  /// <summary>
  ///   <para>Performs testing of <see cref="JavaPlatform.Info.Icon"/> property.</para>
  /// </summary>
  [Fact]
  public void Icon_Property() { new JavaPlatform.Info {Icon = Guid.Empty.ToString()}.Icon.Should().Be(Guid.Empty.ToString()); }

  /// <summary>
  ///   <para>Performs testing of class constructor(s).</para>
  /// </summary>
  /// <seealso cref="JavaPlatform.Info()"/>
  [Fact]
  public void Constructors()
  {
    var info = new JavaPlatform.Info();
    info.Camera.Should().BeNull();
    info.FileSystem.Should().BeNull();
    info.Certificate.Should().BeNull();
    info.Icon.Should().BeNull();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="JavaPlatform.Info.ToResult()"/> method.</para>
  /// </summary>
  [Fact]
  public void ToResult_Method()
  {
    var result = new JavaPlatform.Info().ToResult();
    result.Should().NotBeNull().And.BeOfType<JavaPlatform>();
    result.Camera.Should().BeFalse();
    result.FileSystem.Should().BeFalse();
    result.Certificate.Should().BeNull();
    result.Icon.Should().BeNull();
  }

  /// <summary>
  ///   <para>Performs testing of serialization/deserialization process.</para>
  /// </summary>
  [Fact]
  public void Serialization()
  {
    var info = new JavaPlatform.Info();
    info.Should().BeDataContractSerializable().And.BeXmlSerializable();
  }
}