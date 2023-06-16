using System.Collections.Generic;

namespace DataEngine.Abstraction.Models
{
    public interface IRowDataModel
    {
        string Id { get; }
        int Index { get; set; }
        IEnumerable<IValueModel> Values { get; }
        IValueModel this[string name] { get; }
        IEnumerable<IValueModel> this[string[] names] { get; }
        List<string> Errors { get; set; }
        string GenerateKey(string[] names);
        void SetValue(IValueModel value);
        string Serialize();
    }
}
