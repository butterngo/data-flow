using DataEngine.Abstraction.Models;
using System;
using System.Collections.Generic;
using System.Threading;

namespace DataEngine.DataAccess.Abstraction
{
    public interface IDataInputProvider : IDisposable
    {
        IJsonSchemaModel JsonSchema { get; }
        IEnumerable<IRowDataModel> GetData(CancellationToken cancellationToken);
        IAsyncEnumerable<IRowDataModel> GetDataAsync(CancellationToken cancellationToken);
    }
}
