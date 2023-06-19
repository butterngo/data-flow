using DataEngine.Abstraction.Models;

namespace DataEngine.Core.DataBlocks.Interfaces;

public interface IOutputDataBlock : IDataBlock
{
    Action<IRowDataModel> OnSink { get; set; }
}
