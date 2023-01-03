using Catharsis.Commons;

namespace Yandex.Detector;

/// <summary>
///   <para>Contains information about mobile device and its features and capabilities.</para>
/// </summary>
public interface IMobileDevice : IEquatable<IMobileDevice>
{
  /// <summary>
  ///   <para>Device's model name.</para>
  /// </summary>
  string Name { get; }

  /// <summary>
  ///   <para>Class of devices.</para>
  /// </summary>
  string DeviceClass { get; }

  /// <summary>
  ///   <para>Name of device's vendor.</para>
  /// </summary>
  string Vendor { get; }

  /// <summary>
  ///   <para>Description of device's class.</para>
  /// </summary>
  string Description { get; }

  /// <summary>
  ///   <para>Device's screen resolution (px).</para>
  /// </summary>
  IResolution Screen { get; }

  /// <summary>
  ///   <para>Device's installed _Java platform capabilities.</para>
  /// </summary>
  IJavaPlatform JavaPlatform { get; }

  /// <summary>
  ///   <para>Returns class of devices as instance of <see cref="MobileDevicesClass"/> enumeration.</para>
  /// </summary>
  /// <returns>Class of devices.</returns>
  MobileDevicesClass? MobileDeviceClass => DeviceClass.IsEmpty() ? null : Enum.Parse<MobileDevicesClass>(DeviceClass, true);
}