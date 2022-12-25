using FluentAssertions;
using Xunit;
using Yandex.Detector;

namespace Yandex.Tests.Detector.Core.Implementation;

/// <summary>
///   <para>Tests set for class <see cref="MobileDevice"/>.</para>
/// </summary>
public sealed class MobileDeviceTest : UnitTest<MobileDevice>
{
  /// <summary>
  ///   <para>Performs testing of <see cref="MobileDevice.Name"/> property.</para>
  /// </summary>
  [Fact]
  public void Name_Property() { new MobileDevice(new {Name = Guid.Empty.ToString()}).Name.Should().Be(Guid.Empty.ToString()); }

  /// <summary>
  ///   <para>Performs testing of <see cref="MobileDevice.DeviceClass"/> property.</para>
  /// </summary>
  [Fact]
  public void DeviceClass_Property() { new MobileDevice(new {DeviceClass = Guid.Empty.ToString()}).DeviceClass.Should().Be(Guid.Empty.ToString()); }

  /// <summary>
  ///   <para>Performs testing of <see cref="MobileDevice.Vendor"/> property.</para>
  /// </summary>
  [Fact]
  public void Vendor_Property() { new MobileDevice(new {Vendor = Guid.Empty.ToString()}).Vendor.Should().Be(Guid.Empty.ToString()); }

  /// <summary>
  ///   <para>Performs testing of <see cref="MobileDevice.Description"/> property.</para>
  /// </summary>
  [Fact]
  public void Description_Property() { new MobileDevice(new {Description = Guid.Empty.ToString()}).Description.Should().Be(Guid.Empty.ToString()); }

  /// <summary>
  ///   <para>Performs testing of <see cref="MobileDevice.Screen"/> property.</para>
  /// </summary>
  [Fact]
  public void Screen_Property()
  {
    var screen = new Resolution();
    new MobileDevice(new {Screen = Guid.Empty.ToString()}).Screen.Should().BeSameAs(screen);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="MobileDevice.JavaPlatform"/> property.</para>
  /// </summary>
  [Fact]
  public void JavaPlatform_Property()
  {
    var javaPlatform = new JavaPlatform(new {});
    new MobileDevice(new {JavaPlatform = javaPlatform}.JavaPlatform).Should().BeSameAs(javaPlatform);
  }

  /// <summary>
  ///   <para>Performs testing of class constructor(s).</para>
  /// </summary>
  /// <seealso cref="MobileDevice(string, string, string, string, IResolution?, IJavaPlatform?)"/>
  /// <seealso cref="MobileDevice(MobileDevice.Info)"/>
  /// <seealso cref="MobileDevice(object)"/>
  [Fact]
  public void Constructors()
  {
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
    device.Screen.Should().BeNull();
    device.JavaPlatform.Should().BeNull();

    device = new MobileDevice(new {});
    device.Name.Should().BeEmpty();
    device.DeviceClass.Should().BeEmpty();
    device.Vendor.Should().BeEmpty();
    device.Description.Should().BeEmpty();
    device.Screen.Should().BeNull();
    device.JavaPlatform.Should().BeNull();
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="MobileDevice.Equals(IMobileDevice)"/></description></item>
  ///     <item><description><see cref="MobileDevice.Equals(object)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void Equals_Methods() { TestEquality(nameof(MobileDevice.Name), "first", "second"); }

  /// <summary>
  ///   <para>Performs testing of <see cref="MobileDevice.GetHashCode()"/> method.</para>
  /// </summary>
  [Fact]
  public void GetHashCode_Method() { TestHashCode(nameof(MobileDevice.Name), "first", "second"); }

  /// <summary>
  ///   <para>Performs testing of <see cref="MobileDevice.ToString()"/> method.</para>
  /// </summary>
  [Fact]
  public void ToString_Method() { new MobileDevice(new {Name = "name"}).ToString().Should().Be("name"); }
}

/// <summary>
///   <para>Tests set for class <see cref="MobileDevice.Info"/>.</para>
/// </summary>
public sealed class MobileDeviceInfoTests : UnitTest<MobileDevice.Info>
{
  /// <summary>
  ///   <para>Performs testing of <see cref="MobileDevice.Info.Name"/> property.</para>
  /// </summary>
  [Fact]
  public void Name_Property() { new MobileDevice.Info {Name = Guid.Empty.ToString()}.Name.Should().Be(Guid.Empty.ToString()); }

  /// <summary>
  ///   <para>Performs testing of <see cref="MobileDevice.Info.DeviceClass"/> property.</para>
  /// </summary>
  [Fact]
  public void DeviceClass_Property() { new MobileDevice.Info {DeviceClass = Guid.Empty.ToString()}.DeviceClass.Should().Be(Guid.Empty.ToString()); }

  /// <summary>
  ///   <para>Performs testing of <see cref="MobileDevice.Info.Vendor"/> property.</para>
  /// </summary>
  [Fact]
  public void Vendor_Property() { new MobileDevice.Info {Vendor = Guid.Empty.ToString()}.Vendor.Should().Be(Guid.Empty.ToString()); }

  /// <summary>
  ///   <para>Performs testing of <see cref="MobileDevice.Info.Description"/> property.</para>
  /// </summary>
  [Fact]
  public void Description_Property() { new MobileDevice.Info {Description = Guid.Empty.ToString()}.Description.Should().Be(Guid.Empty.ToString()); }

  /// <summary>
  ///   <para>Performs testing of <see cref="MobileDevice.Info.ScreenWidth"/> property.</para>
  /// </summary>
  [Fact]
  public void ScreenWidth_Property() { new MobileDevice.Info {ScreenWidth = short.MaxValue}.ScreenWidth.Should().Be(short.MaxValue); }

  /// <summary>
  ///   <para>Performs testing of <see cref="MobileDevice.Info.ScreenHeight"/> property.</para>
  /// </summary>
  [Fact]
  public void ScreenHeight_Property() { new MobileDevice.Info {ScreenHeight = short.MaxValue}.ScreenHeight.Should().Be(short.MaxValue); }

  /// <summary>
  ///   <para>Performs testing of <see cref="MobileDevice.Info.JavaPlatform"/> property.</para>
  /// </summary>
  [Fact]
  public void JavaPlatform_Property()
  {
    var javaPlatform = new JavaPlatform(new {});
    new MobileDevice.Info {JavaPlatform = javaPlatform}.JavaPlatform.Should().BeSameAs(javaPlatform);
  }

  /// <summary>
  ///   <para>Performs testing of class constructor(s).</para>
  /// </summary>
  /// <seealso cref="MobileDevice.Info()"/>
  [Fact]
  public void Constructors()
  {
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
  ///   <para>Performs testing of <see cref="MobileDevice.Info.Result()"/> method.</para>
  /// </summary>
  [Fact]
  public void Result_Method()
  {
    var result = new MobileDevice.Info().Result();
    result.Should().NotBeNull().And.BeOfType<MobileDevice>();
    result.Name.Should().BeEmpty();
    result.DeviceClass.Should().BeEmpty();
    result.Vendor.Should().BeEmpty();
    result.Description.Should().BeEmpty();
    result.Screen.Should().BeNull();
    result.JavaPlatform.Should().BeNull();
  }

  /// <summary>
  ///   <para>Performs testing of serialization/deserialization process.</para>
  /// </summary>
  [Fact]
  public void Serialization()
  {
    var info = new MobileDevice.Info();
    info.Should().BeDataContractSerializable().And.BeXmlSerializable();
  }
}