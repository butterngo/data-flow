using DataEngine.Abstraction.Models;

namespace DataEngine.Abstraction.Convertors
{
    public interface IDataEngineParser
    {
        bool TryPase(IValueModel value, out object result, out string[] error);
    }
}
