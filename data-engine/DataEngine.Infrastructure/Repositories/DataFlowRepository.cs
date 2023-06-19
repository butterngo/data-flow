using DataEngine.Domain.Entities;
using DataEngine.Domain.Repositories;

namespace DataEngine.Infrastructure.Repositories;

internal class DataFlowRepository : IDataFlowRepository
{
    private readonly string _path;

    public DataFlowRepository() 
    {
        _path = Path.Combine("Data","data_flows.json");
    }

    public Task CreateAsync(DataFlow entity)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<DataFlow>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<DataFlow> GetByAsync(string id)
    {
        throw new NotImplementedException();
    }
}
