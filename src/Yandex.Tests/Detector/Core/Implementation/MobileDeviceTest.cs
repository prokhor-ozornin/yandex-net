using Catharsis.Commons;
using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Json;
using Xunit;
using Yandex.Detector;

namespace Yandex.Tests.Detector.Core.Implementation;

/// <summary>
///   <para>Tests set for class <see cref="MobileDevice"/>.</para>
/// </summary>
public sealed class MobileDeviceTest : ClassTest<MobileDevice>
{
  /// <summary>
  ///   <para>Performs testing of class constructor(s).</para>
  /// </summary>
  /// <seealso cref="MobileDevice(string, string, string, string, IResolution, IJavaPlatform)"/>
  /// <seealso cref="MobileDevice(MobileDevice.Info)"/>
  /// <seealso cref="MobileDevice(object)"/>
  [Fact]
  public void Constructors()
  {
    typeof(MobileDevice).Should().BeDerivedFrom<object>().And.Implement<IMobileDevice>();

    var device = new MobileDevice("name", "deviceClass", "vendor", "description", new Resolution {Width = short.MinValue, Height = short.MaxValue});
    device.Name.Should().Be("name");
    device.DeviceClass.Should().Be("deviceClass");
    device.Vendor.Should().Be("vendor");
    device.Description.Should().Be("description");
    device.Screen.Should().Be(new Resolution {Width = short.MinValue, Height = short.MaxValue});
    device.JavaPlatform.Should().BeNull();

    device = new MobileDevice(new MobileDevice.Info());
    device.Name.Should().BeEmpty();
    device.DeviceClass.Should().BeEmpty();
    device.Vendor.Should().BeEmpty();
    device.Description.Should().BeEmpty();
    device.Screen.Should().Be(new Resolution());
    device.JavaPlatform.Should().BeNull();

    device = new MobileDevice(new {});
    device.Name.Should().BeEmpty();
    device.DeviceClass.Should().BeEmpty();
    device.Vendor.Should().BeEmpty();
    device.Description.Should().BeEmpty();
    device.Screen.Should().Be(new Resolution());
    device.JavaPlatform.Should().BeNull();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="MobileDevice.Name"/> property.</para>
  /// </summary>
  [Fact]
  public void Name_Property()
  {
    new MobileDevice(new { Name = "name" }).Name.Should().Be("name");
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="MobileDevice.DeviceClass"/> property.</para>
  /// </summary>
  [Fact]
  public void DeviceClass_Property()
  {
    new MobileDevice(new { DeviceClass = "deviceClass" }).DeviceClass.Should().Be("deviceClass");
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="MobileDevice.Vendor"/> property.</para>
  /// </summary>
  [Fact]
  public void Vendor_Property()
  {
    new MobileDevice(new { Vendor = "vendor" }).Vendor.Should().Be("vendor");
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="MobileDevice.Description"/> property.</para>
  /// </summary>
  [Fact]
  public void Description_Property()
  {
    new MobileDevice(new { Description = "description" }).Description.Should().Be("description");
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="MobileDevice.Screen"/> property.</para>
  /// </summary>
  [Fact]
  public void Screen_Property()
  {
    var screen = new Resolution();
    new MobileDevice(new { Screen = "screen" }).Screen.Should().BeSameAs(screen);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="MobileDevice.JavaPlatform"/> property.</para>
  /// </summary>
  [Fact]
  public void JavaPlatform_Property()
  {
    var javaPlatform = new JavaPlatform(new { });
    new MobileDevice(new { JavaPlatform = javaPlatform }.JavaPlatform).Should().BeSameAs(javaPlatform);
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="MobileDevice.Equals(IMobileDevice)"/></description></item>
  ///     <item><description><see cref="MobileDevice.Equals(object)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void Equals_Methods()
  {
    TestEquality(nameof(MobileDevice.Name), "first", "second");
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="MobileDevice.GetHashCode()"/> method.</para>
  /// </summary>
  [Fact]
  public void GetHashCode_Method()
  {
    TestHashCode(nameof(MobileDevice.Name), "first", "second");
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="MobileDevice.ToString()"/> method.</para>
  /// </summary>
  [Fact]
  public void ToString_Method()
  {
    using (new AssertionScope())
    {
      Validate(string.Empty, new MobileDevice(new {}));
      Validate(string.Empty, new MobileDevice(new { Name = string.Empty }));
      Validate("name", new MobileDevice(new { Name = "name" }));
    }

    return;

    static void Validate(string value, object instance) => instance.ToString().Should().Be(value);
  }
}

/// <summary>
///   <para>Tests set for class <see cref="MobileDevice.Info"/>.</para>
/// </summary>
public sealed class MobileDeviceInfoTest : ClassTest<MobileDevice.Info>
{
  /// <summary>
  ///   <para>Performs testing of class constructor(s).</para>
  /// </summary>
  /// <seealso cref="MobileDevice.Info()"/>
  [Fact]
  public void Constructors()
  {
    typeof(MobileDevice.Info).Should().BeDerivedFrom<object>().And.Implement<IResultable<IMobileDevice>>();

    var info = new MobileDevice.Info();
    info.Name.Should().BeNull();
    info.DeviceClass.Should().BeNull();
    info.Vendor.Should().BeNull();
    info.Description.Should().BeNull();
    info.ScreenWidth.Should().BeNull();
    info.ScreenHeight.Should().BeNull();
    info.JavaPlatform.Should().BeNull();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="MobileDevice.Info.Name"/> property.</para>
  /// </summary>
  [Fact]
  public void Name_Property()
  {
    new MobileDevice.Info { Name = "name" }.Name.Should().Be("name");
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="MobileDevice.Info.DeviceClass"/> property.</para>
  /// </summary>
  [Fact]
  public void DeviceClass_Property()
  {
    new MobileDevice.Info { DeviceClass = "deviceClass" }.DeviceClass.Should().Be("deviceClass");
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="MobileDevice.Info.Vendor"/> property.</para>
  /// </summary>
  [Fact]
  public void Vendor_Property()
  {
    new MobileDevice.Info { Vendor = "vendor" }.Vendor.Should().Be("vendor");
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="MobileDevice.Info.Description"/> property.</para>
  /// </summary>
  [Fact]
  public void Description_Property()
  {
    new MobileDevice.Info { Description = "description" }.Description.Should().Be("description");
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="MobileDevice.Info.ScreenWidth"/> property.</para>
  /// </summary>
  [Fact]
  public void ScreenWidth_Property()
  {
    new MobileDevice.Info { ScreenWidth = short.MaxValue }.ScreenWidth.Should().Be(short.MaxValue);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="MobileDevice.Info.ScreenHeight"/> property.</para>
  /// </summary>
  [Fact]
  public void ScreenHeight_Property()
  {
    new MobileDevice.Info { ScreenHeight = short.MaxValue }.ScreenHeight.Should().Be(short.MaxValue);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="MobileDevice.Info.JavaPlatform"/> property.</para>
  /// </summary>
  [Fact]
  public void JavaPlatform_Property()
  {
    var javaPlatform = new JavaPlatform(new { });
    new MobileDevice.Info { JavaPlatform = javaPlatform }.JavaPlatform.Should().BeSameAs(javaPlatform);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="MobileDevice.Info.ToResult()"/> method.</para>
  /// </summary>
  [Fact]
  public void ToResult_Method()
  {
    using (new AssertionScope())
    {
      var result = new MobileDevice.Info().ToResult();
      result.Should().NotBeNull().And.BeOfType<MobileDevice>();
      result.Name.Should().BeEmpty();
      result.DeviceClass.Should().BeEmpty();
      result.Vendor.Should().BeEmpty();
      result.Description.Should().BeEmpty();
      result.Screen.Should().BeNull();
      result.JavaPlatform.Should().BeNull();
    }

    return;

    static void Validate(MobileDevice.Info info)
    {
      var result = info.ToResult();

      result.Should().BeOfType<JavaPlatform>();
      result.Name.Should().Be(info.Name ?? string.Empty);
      result.DeviceClass.Should().Be(info.DeviceClass ?? string.Empty);
      result.Vendor.Should().Be(info.Vendor ?? string.Empty);
      result.Description.Should().Be(info.Description ?? string.Empty);
      result.Screen.Should().BeOfType<Resolution>();
      result.Screen.Height.Should().Be(info.ScreenHeight ?? 0);
      result.Screen.Width.Should().Be(info.ScreenWidth ?? 0);
      result.JavaPlatform.Should().BeSameAs(info.JavaPlatform);
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
      Validate(new MobileDevice.Info());
    }

    return;

    static void Validate(object instance) => instance.Should().BeDataContractSerializable().And.BeXmlSerializable().And.BeJsonSerializable();
  }
}