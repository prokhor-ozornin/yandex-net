using Catharsis.Commons;

namespace Yandex.Detector;

internal sealed class MobileDetector : IMobileDetector
{
  private bool disposed;
  
  private Uri EndpointUrl { get; } = "http://phd.yandex.net/detect/".ToUri();
  private HttpClient HttpClient { get; } = new();

  public async Task<IMobileDevice> DetectAsync(IReadOnlyDictionary<string, object> headers, CancellationToken cancellation = default)
  {
    if (!headers.Any())
    {
      throw new DetectorException("No HTTP headers were specified");
    }

    string response;

    try
    {
      response = await HttpClient.WithHeaders(headers).ToTextAsync(EndpointUrl, cancellation).ConfigureAwait(false);
    }
    catch (Exception exception)
    {
      throw new DetectorException("Network communication error", exception);
    }

    try
    {
      var error = response.DeserializeAsXml<Error.Info>().ToResult();
      throw new DetectorException(error.Text);
    }
    catch (InvalidOperationException)
    {
    }

    try
    {
      return response.DeserializeAsXml<MobileDevice.Info>().ToResult();
    }
    catch (Exception exception)
    {
      throw new DetectorException("Failed to understand service's response", exception);
    }
  }

  public void Dispose()
  {
    Dispose(true);
    GC.SuppressFinalize(this);
  }

  private void Dispose(bool disposing)
  {
    if (!disposing || disposed)
    {
      return;
    }

    HttpClient.Dispose();

    disposed = true;
  }
}