﻿namespace Yandex.Detector;

/// <summary>
///   <para>Representation of access point for different types of W3C content validators.</para>
/// </summary>
/// <summary>
///   <para>Voting search results.</para>
/// </summary>
public interface IResultable<T> where T: class
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <returns></returns>
  T ToResult();
}