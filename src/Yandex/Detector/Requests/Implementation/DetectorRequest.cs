using Catharsis.Extensions;

namespace Yandex.Detector;

internal sealed class DetectorRequest : IDetectorRequest
{
  private readonly Dictionary<string, object> _headers = [];

  public IReadOnlyDictionary<string, object> Headers => _headers;

  public IDetectorRequest WithHeader(string name, object value)
  {
    if (name is null) throw new ArgumentNullException(nameof(name));
    if (name.IsEmpty()) throw new ArgumentException(nameof(name));

    _headers[name] = value;
    
    return this;
  }
} 