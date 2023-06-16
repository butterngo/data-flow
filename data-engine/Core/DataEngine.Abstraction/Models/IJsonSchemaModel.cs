using System;
using System.Collections.Generic;

namespace DataEngine.Abstraction.Models
{
    public interface IJsonSchemaModel
    {
        Guid Id { get; }
        string Schema { get; set; }
        string Title { get; set; }
        string Type { get; set; }
        IEnumerable<IPropertyModel> Properties { get; set; }
        IEnumerable<string> Required { get; set; }
        string Serialize();
    }
}
