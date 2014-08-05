using System.Collections.Generic;

namespace Yandex.Translator
{
  internal abstract class Request : IRequest
  {
    private readonly IDictionary<string, object> parameters = new Dictionary<string, object>();

    public IDictionary<string, object> Parameters
    {
      get { return this.parameters; }
    }
  }
}