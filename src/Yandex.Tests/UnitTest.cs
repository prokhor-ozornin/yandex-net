using Catharsis.Commons;
using FluentAssertions;

namespace Yandex.Tests;

public abstract class UnitTest<T>
{
  private CancellationToken Cancellation { get; } = new(true);

  private Random Randomizer { get; } = new();

  protected void TestCompareTo<TProperty>(string property, TProperty lower, TProperty greater, Func<T>? constructor = null)
  {
    constructor ??= () => typeof(T).Instance().To<T>();

    var first = constructor().To<IComparable<T>>();
    var second = constructor().To<T>();

    first.Property(property, lower);
    second.Property(property, lower);

    first.CompareTo(second).Should().Be(0);
    second.Property(property, greater);
    first.CompareTo(second).Should().BeLessThan(0);
  }

  protected void TestEquality<TProperty>(string property, TProperty oldValue, TProperty newValue, Func<T>? constructor = null)
  {
    constructor ??= () => typeof(T).Instance().To<T>();
    var entity = constructor();

    entity.Equals(null).Should().BeFalse();
    entity.Equals(entity).Should().BeTrue();
    entity.Equals(constructor()).Should().BeTrue();

    constructor().Property(property, oldValue).Equals(constructor().Property(property, oldValue)).Should().BeTrue();
    constructor().Property(property, oldValue).Equals(constructor().Property(property, newValue)).Should().BeFalse();
  }

  protected void TestHashCode<TProperty>(string property, TProperty oldValue, TProperty newValue, Func<T>? constructor = null)
  {
    constructor ??= () => typeof(T).Instance().To<T>();
    var entity = constructor();

    entity.GetHashCode().Should().Be(entity.GetHashCode());
    entity.GetHashCode().Should().Be(constructor().GetHashCode());

    constructor().Property(property, oldValue).GetHashCode().Should().Be(constructor().Property(property, oldValue).GetHashCode());
    constructor().Property(property, oldValue).GetHashCode().Should().NotBe(constructor().Property(property, newValue).GetHashCode());
  }
}