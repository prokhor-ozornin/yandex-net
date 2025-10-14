using FluentAssertions;
using Xunit;

namespace Yandex.Tests;

/// <summary>
///   <para>Tests set for class <see cref="Yandex"/>.</para>
/// </summary>
/// <seealso cref="Yandex"/>
public sealed class YandexTest : Test
{
  /// <summary>
  ///   <para>Performs testing of <see cref="Yandex.Api"/> property.</para>
  /// </summary>
  [Fact]
  public void Api_Property() => Yandex.Api.Should().NotBeNull().And.BeSameAs(Yandex.Api);
}