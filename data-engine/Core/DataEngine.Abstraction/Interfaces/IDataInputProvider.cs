using DataEngine.Abstraction.Models;

namespace DataEngine.Abstraction.Interfaces
{
    public interface IDataInputProvider : IDisposable
    {
        IJsonSchemaModel JsonSchema { get; }
        IEnumerable<IRowDataModel> GetData(CancellationToken cancellationToken);
        IAsyncEnumerable<IRowDataModel> GetDataAsync(CancellationToken cancellationToken);
    }
}
