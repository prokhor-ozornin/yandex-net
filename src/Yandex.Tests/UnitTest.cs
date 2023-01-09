namespace Yandex.Tests;

public abstract class UnitTest : IDisposable
{
  protected CancellationToken Cancellation { get; } = new(true);

  protected Random Randomizer { get; } = new();

  public virtual void Dispose()
  {
  }
}