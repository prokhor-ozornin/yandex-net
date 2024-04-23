using Catharsis.Extensions;

namespace Yandex.Detector;

internal sealed class DetectorRequest : IDetectorRequest
{
  private readonly Dictionary<string, object> headers = [];

  public IReadOnlyDictionary<string, object> Headers => headers;

  public IDetectorRequest WithHeader(string name, object value)
  {
    if (name is null) throw new ArgumentNullException(nameof(name));
    if (name.IsEmpty()) throw new ArgumentException(nameof(name));

    headers[name] = value;
    
    return this;
  }
} 