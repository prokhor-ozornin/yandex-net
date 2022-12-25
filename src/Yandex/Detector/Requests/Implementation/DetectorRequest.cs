namespace Yandex.Detector;

internal sealed class DetectorRequest : IDetectorRequest
{
  public IDictionary<string, object?> Headers { get; } = new Dictionary<string, object?>();

  public IDetectorRequest Header(string name, object? value)
  {
    Headers[name] = value;

    return this;
  }
} 