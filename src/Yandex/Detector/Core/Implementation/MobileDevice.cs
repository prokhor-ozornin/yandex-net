using System.Runtime.Serialization;
using Catharsis.Extensions;

namespace Yandex.Detector;

/// <summary>
///   <para>Contains information about mobile device and its features and capabilities.</para>
/// </summary>
public sealed class MobileDevice : IMobileDevice
{
  /// <summary>
  ///   <para>Device's model name.</para>
  /// </summary>
  public string Name { get; }

  /// <summary>
  ///   <para>Class of devices.</para>
  /// </summary>
  public string DeviceClass { get; }

  /// <summary>
  ///   <para>Name of device's vendor.</para>
  /// </summary>
  public string Vendor { get; } 

  /// <summary>
  ///   <para>Description of device's class.</para>
  /// </summary>
  public string Description { get; } 

  /// <summary>
  ///   <para>Device's screen resolution (px).</para>
  /// </summary>
  public IResolution Screen { get; }

  /// <summary>
  ///   <para>Device's installed Java platform capabilities.</para>
  /// </summary>
  public IJavaPlatform JavaPlatform { get; }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="name"></param>
  /// <param name="deviceClass"></param>
  /// <param name="vendor"></param>
  /// <param name="description"></param>
  /// <param name="screen"></param>
  /// <param name="javaPlatform"></param>
  public MobileDevice(string name,
                      string deviceClass,
                      string vendor,
                      string description,
                      IResolution screen = null,
                      IJavaPlatform javaPlatform = null)
  {
    Name = name;
    DeviceClass = deviceClass;
    Vendor = vendor;
    Description = description;
    Screen = screen;
    JavaPlatform = javaPlatform;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="info"></param>
  public MobileDevice(Info info)
  {
    Name = info.Name ?? string.Empty;
    DeviceClass = info.DeviceClass ?? string.Empty;
    Vendor = info.Vendor ?? string.Empty;
    Description = info.Description ?? string.Empty;
    Screen = new Resolution { Height = info.ScreenHeight ?? 0, Width = info.ScreenWidth ?? 0 };
    JavaPlatform = info.JavaPlatform;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="info"></param>
  public MobileDevice(object info) : this(new Info().SetState(info))
  {
  }

  /// <summary>
  ///   <para>Determines whether two <see cref="IMobileDevice"/> instances are equal.</para>
  /// </summary>
  /// <param name="other">The instance to compare with the current one.</param>
  /// <returns><c>true</c> if specified instance is equal to the current, <c>false</c> otherwise.</returns>
  public bool Equals(IMobileDevice other) => this.Equality(other, nameof(Name));

  /// <summary>
  ///   <para>Determines whether the specified <see cref="object"/> is equal to the current <see cref="object"/>.</para>
  /// </summary>
  /// <param name="other">The object to compare with the current object.</param>
  /// <returns><c>true</c> if the specified object is equal to the current object, <c>false</c>.</returns>
  public override bool Equals(object other) => Equals(other as IMobileDevice);

  /// <summary>
  ///   <para>Returns hash code for the current object.</para>
  /// </summary>
  /// <returns>Hash code of current instance.</returns>
  public override int GetHashCode() => this.HashCode(nameof(Name));

  /// <summary>
  ///   <para>Returns a <see cref="string"/> that represents the current <see cref="MobileDevice"/> instance.</para>
  /// </summary>
  /// <returns>A string that represents the current <see cref="MobileDevice"/>.</returns>
  public override string ToString() => Name;

  /// <summary>
  ///   <para></para>
  /// </summary>
  [DataContract(Name = "yandex-mobile-info")]
  public sealed record Info : IResultable<IMobileDevice>
  {
    /// <summary>
    ///   <para>Device's model name.</para>
    /// </summary>
    [DataMember(Name = "name", IsRequired = true)]
    public string Name { get; init; }

    /// <summary>
    ///   <para>Class of devices.</para>
    /// </summary>
    [DataMember(Name = "device-class", IsRequired = true)]
    public string DeviceClass { get; init; }

    /// <summary>
    ///   <para>Name of device's vendor.</para>
    /// </summary>
    [DataMember(Name = "vendor", IsRequired = true)]
    public string Vendor { get; init; }

    /// <summary>
    ///   <para>Description of device's class.</para>
    /// </summary>
    [DataMember(Name = "device-class-desc", IsRequired = true)]
    public string Description { get; init; }

    /// <summary>
    ///   <para>Device's screen horizontal width (px).</para>
    /// </summary>
    [DataMember(Name = "screenx", IsRequired = true)]
    public short? ScreenWidth { get; init; }

    /// <summary>
    ///   <para>Device's screen vertical height (px).</para>
    /// </summary>
    [DataMember(Name = "screeny", IsRequired = true)]
    public short? ScreenHeight { get; init; }

    /// <summary>
    ///   <para>Device's installed Java platform capabilities.</para>
    /// </summary>
    [DataMember(Name = "java", IsRequired = true)]
    public JavaPlatform JavaPlatform { get; init; }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    public IMobileDevice ToResult() => new MobileDevice(this);
  }
}