﻿using System.Runtime.Serialization;
using Catharsis.Extensions;

namespace Yandex.Detector;

/// <summary>
///   <para>Mobile Java platform capabilities.</para>
/// </summary>
public sealed record JavaPlatform : IJavaPlatform
{
  /// <summary>
  ///   <para>Whether Java applications have access to device's camera.</para>
  /// </summary>
  public bool Camera { get; }

  /// <summary>
  ///   <para>Whether Java applications have access to device's filesystem.</para>
  /// </summary>
  public bool FileSystem { get; }

  /// <summary>
  ///   <para>Prefix of Java certificate.</para>
  /// </summary>
  public string Certificate { get; }

  /// <summary>
  ///   <para>Dimensions of Java applications icons.</para>
  /// </summary>
  public IResolution Icon { get; }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="camera"></param>
  /// <param name="fileSystem"></param>
  /// <param name="certificate"></param>
  /// <param name="icon"></param>
  public JavaPlatform(bool camera,
                      bool fileSystem,
                      string certificate = null, 
                      IResolution icon = null)
  {
    Camera = camera;
    FileSystem = fileSystem;
    Certificate = certificate;
    Icon = icon;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="info"></param>
  public JavaPlatform(Info info)
  {
    Camera = info.Camera > 0;
    FileSystem = info.FileSystem > 0;
    Certificate = info.Certificate;
    Icon = info.Icon != null && info.Icon.Contains('x') ? new Resolution { Height = info.Icon?.Split('x')[1].ToShort() ?? 0, Width = info.Icon?.Split('x')[0].ToShort() ?? 0 } : null;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="info"></param>
  public JavaPlatform(object info) : this(new Info().SetState(info))
  {
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  [DataContract(Name = "java")]
  public sealed record Info : IResultable<IJavaPlatform>
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    [DataMember(Name = "cam-access", IsRequired = true)]
    public byte? Camera { get; init; }

    /// <summary>
    ///   <para></para>
    /// </summary>
    [DataMember(Name = "fs-access", IsRequired = true)]
    public byte? FileSystem { get; init; }

    /// <summary>
    ///   <para></para>
    /// </summary>
    [DataMember(Name = "certificate-prefix", IsRequired = true)]
    public string Certificate { get; init; }

    /// <summary>
    ///   <para></para>
    /// </summary>
    [DataMember(Name = "iconsize", IsRequired = true)]
    public string Icon { get; init; }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    public IJavaPlatform ToResult() => new JavaPlatform(this);
  }
}