using DataEngine.Abstraction.Models;

namespace DataEngine.Abstraction.Interfaces
{
    public interface IDataOuputProvider : IDisposable
    {
        IJsonSchemaModel JsonSchema { get; set; }
        Task SinkAsync(IEnumerable<IRowDataModel> data);

        Task SinkAsync(IRowDataModel data);
    }
}
