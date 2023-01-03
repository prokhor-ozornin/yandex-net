namespace Yandex.Detector;

internal sealed class DetectorRequest : IDetectorRequest
{
  private readonly Dictionary<string, object> headers = new();

  public IReadOnlyDictionary<string, object> Headers => headers;

  public IDetectorRequest WithHeader(string name, object value)
  {
    headers[name] = value;
    return this;
  }
} 