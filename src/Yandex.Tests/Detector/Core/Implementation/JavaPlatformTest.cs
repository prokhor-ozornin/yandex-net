using System.Runtime.Serialization;
using Catharsis.Commons;
using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Json;
using Xunit;
using Yandex.Detector;

namespace Yandex.Tests.Detector.Core.Implementation;

/// <summary>
///   <para>Tests set for class <see cref="JavaPlatform"/>.</para>
/// </summary>
public sealed class JavaPlatformTest : ClassTest<JavaPlatform>
{
  /// <summary>
  ///   <para>Performs testing of class constructor(s).</para>
  /// </summary>
  /// <seealso cref="JavaPlatform(bool, bool, string, IResolution)"/>
  /// <seealso cref="JavaPlatform(JavaPlatform.Info)"/>
  /// <seealso cref="JavaPlatform(object)"/>
  [Fact]
  public void Constructors()
  {
    typeof(JavaPlatform).Should().BeDerivedFrom<object>().And.Implement<IJavaPlatform>();

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
  ///   <para>Performs testing of <see cref="JavaPlatform.Camera"/> property.</para>
  /// </summary>
  [Fact]
  public void Camera_Property()
  {
    new JavaPlatform(new { Camera = (byte) 1 }).Camera.Should().BeTrue();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="JavaPlatform.FileSystem"/> property.</para>
  /// </summary>
  [Fact]
  public void FileSystem_Property()
  {
    new JavaPlatform(new { FileSystem = (byte) 1 }).FileSystem.Should().BeTrue();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="JavaPlatform.Certificate"/> property.</para>
  /// </summary>
  [Fact]
  public void Certificate_Property()
  {
    new JavaPlatform(new { Certificate = "certificate" }).Certificate.Should().Be("certificate");
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="JavaPlatform.Icon"/> property.</para>
  /// </summary>
  [Fact]
  public void Icon_Property()
  {
    new JavaPlatform(new { Icon = "32x32" }).Icon.ToString().Should().Be("32x32");
  }
}

/// <summary>
///   <para>Tests set for class <see cref="JavaPlatform.Info"/>.</para>
/// </summary>
public sealed class JavaPlatformInfoTest : ClassTest<JavaPlatform.Info>
{
  /// <summary>
  ///   <para>Performs testing of class constructor(s).</para>
  /// </summary>
  /// <seealso cref="JavaPlatform.Info()"/>
  [Fact]
  public void Constructors()
  {
    typeof(JavaPlatform.Info).Should().BeDerivedFrom<object>().And.Implement<IResultable<IJavaPlatform>>().And.BeDecoratedWith<DataContractAttribute>();

    var info = new JavaPlatform.Info();
    info.Camera.Should().BeNull();
    info.FileSystem.Should().BeNull();
    info.Certificate.Should().BeNull();
    info.Icon.Should().BeNull();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="JavaPlatform.Info.Camera"/> property.</para>
  /// </summary>
  [Fact]
  public void Camera_Property()
  {
    new JavaPlatform.Info { Camera = byte.MaxValue }.Camera.Should().Be(byte.MaxValue);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="JavaPlatform.Info.FileSystem"/> property.</para>
  /// </summary>
  [Fact]
  public void FileSystem_Property()
  {
    new JavaPlatform.Info { FileSystem = byte.MaxValue }.FileSystem.Should().Be(byte.MaxValue);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="JavaPlatform.Info.Certificate"/> property.</para>
  /// </summary>
  [Fact]
  public void Certificate_Property()
  {
    new JavaPlatform.Info { Certificate = Guid.Empty.ToString() }.Certificate.Should().Be(Guid.Empty.ToString());
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="JavaPlatform.Info.Icon"/> property.</para>
  /// </summary>
  [Fact]
  public void Icon_Property()
  {
    new JavaPlatform.Info { Icon = Guid.Empty.ToString() }.Icon.Should().Be(Guid.Empty.ToString());
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="JavaPlatform.Info.ToResult()"/> method.</para>
  /// </summary>
  [Fact]
  public void ToResult_Method()
  {
    using (new AssertionScope())
    {
      Validate(new JavaPlatform.Info());
    }

    return;

    static void Validate(JavaPlatform.Info info)
    {
      var result = info.ToResult();

      result.Should().BeOfType<JavaPlatform>();
      result.Camera.Should().Be(info.Camera > 0);
      result.FileSystem.Should().Be(info.FileSystem > 0);
      result.Certificate.Should().BeSameAs(info.Certificate);
      //result.Icon.Should().BeOfType<Resolution>();
      //result.Icon.Height.Should().Be(info.Icon?.Split('x')[1].ToShort() ?? 0);
      //result.Icon.Width.Should().Be(info.Icon?.Split('x')[0].ToShort() ?? 0);
    }
  }

  /// <summary>
  ///   <para>Performs testing of serialization/deserialization process.</para>
  /// </summary>
  [Fact]
  public void Serialization()
  {
    using (new AssertionScope())
    {
      Validate(new JavaPlatform.Info());
    }

    return;

    static void Validate(object instance) => instance.Should().BeDataContractSerializable().And.BeXmlSerializable().And.BeJsonSerializable();
  }
}