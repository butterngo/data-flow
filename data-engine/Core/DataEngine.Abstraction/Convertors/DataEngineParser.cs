using DataEngine.Abstraction.Convertors;
using DataEngine.Abstraction.Models;
using System;

namespace DataEngine.Abstraction.Convertors
{
    public class DataEngineParser : IDataEngineParser
    {
        public bool TryPase(IValueModel value, out object result, out string[] error)
        {
            throw new NotImplementedException();
        }
    }
}
