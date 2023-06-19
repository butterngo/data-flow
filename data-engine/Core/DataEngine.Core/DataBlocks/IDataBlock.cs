using DataEngine.Abstraction.Models;
using System.Threading.Tasks.Dataflow;

namespace DataEngine.Core.DataBlocks;

public interface IDataBlock : IDataflowBlock
{
    string Id { get; }
    string Name { get; set; }
    string LinkId { get; set; }
    ITargetBlock<IRowDataModel> Target { get; }
    void LinkTo(IDataflowBlock block, DataflowLinkOptions linkOptions);
}
