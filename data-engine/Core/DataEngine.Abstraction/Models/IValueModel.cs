using Newtonsoft.Json;

namespace DataEngine.Abstraction.Models
{
    public interface IValueModel
    {
        string Id { get; }
        IPropertyModel Property { get; }
        string Value { get; }
        string Error { get; }
        string Normalized();
        void Write(JsonTextWriter writer);
    }
}
