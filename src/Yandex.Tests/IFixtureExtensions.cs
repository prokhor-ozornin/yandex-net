using AutoFixture.Kernel;
using AutoFixture;

namespace Yandex.Tests;

public static class IFixtureExtensions
{
  public static IFixture TypeRelay(this IFixture fixture, Type from, Type to)
  {
    if (fixture is null) throw new ArgumentNullException(nameof(fixture));
    if (from is null) throw new ArgumentNullException(nameof(from));
    if (to is null) throw new ArgumentNullException(nameof(to));

    fixture.Customizations.Add(new TypeRelay(from, to));

    return fixture;
  }

  public static IFixture TypeRelay<TFrom, TTo>(this IFixture fixture) => fixture.TypeRelay(typeof(TFrom), typeof(TTo));
}