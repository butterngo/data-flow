using Newtonsoft.Json;
using System;

namespace DataEngine.Abstraction.Models
{
    public interface IPropertyModel
    {
        Guid Id { get; }
        string Name { get; set; }
        string Pattern { get; set; }
        JsonObjectTypeEnum Type { get; set; }
        string Description { get; set; }
        void Write(JsonTextWriter writer);
    }
}
