using DataEngine.Domain.Entities;

namespace DataEngine.Domain.Repositories
{
    public interface IDataFlowRepository
    {
        Task CreateAsync(DataFlow entity);
        Task<DataFlow> GetByAsync(string id);
        Task<IEnumerable<DataFlow>> GetAllAsync();
    }
}
