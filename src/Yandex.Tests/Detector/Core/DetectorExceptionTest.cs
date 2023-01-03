﻿using FluentAssertions;
using Xunit;
using Yandex.Detector;

namespace Yandex.Tests.Detector.Core;

/// <summary>
///   <para>Tests set for class <see cref="DetectorException"/>.</para>
/// </summary>
public sealed class DetectorExceptionTest
{
  /// <summary>
  ///   <para>Performs testing of class constructor(s).</para>
  /// </summary>
  /// <seealso cref="DetectorException(string?, Exception?)"/>
  [Fact]
  public void Constructors()
  {
    var exception = new DetectorException();
    exception.InnerException.Should().BeNull();
    exception.Message.Should().BeNull();

    var inner = new Exception();
    exception = new DetectorException("message", inner);
    exception.InnerException.Should().NotBeNull().And.BeSameAs(inner);
    exception.Message.Should().Be("message");
  }
}