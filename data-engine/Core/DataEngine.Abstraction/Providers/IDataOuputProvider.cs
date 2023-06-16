using DataEngine.Abstraction.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataEngine.DataAccess.Abstraction
{
    public interface IDataOuputProvider : IDisposable
    {
        IJsonSchemaModel JsonSchema { get; set; }
        Task SinkAsync(IEnumerable<IRowDataModel> data);

        Task SinkAsync(IRowDataModel data);
    }
}
