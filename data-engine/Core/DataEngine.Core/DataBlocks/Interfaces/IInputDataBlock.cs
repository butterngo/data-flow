using DataEngine.Abstraction.Models;

namespace DataEngine.Core.DataBlocks.Interfaces;

public interface IInputDataBlock : IDataBlock
{
    Task SendAsync(IRowDataModel data);
}
