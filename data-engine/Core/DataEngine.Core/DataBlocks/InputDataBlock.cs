using DataEngine.Abstraction;
using DataEngine.Abstraction.Models;
using DataEngine.Core.DataBlocks.Interfaces;
using System.Threading.Tasks.Dataflow;

namespace DataEngine.Core.DataBlocks;

public class InputDataBlock : DataBlockBase, IInputDataBlock
{
    private int _rowIndex = 1;

    public InputDataBlock(IStateMachine stateMachine) 
        : base(stateMachine)
    {
    }

    /// <summary>
    /// https://www.blinkingcaret.com/2019/05/15/tpl-dataflow-in-net-core-in-depth-part-1/
    /// </summary>
    /// <returns></returns>
    public async Task SendAsync(IRowDataModel data)
    {
        data.Index = _rowIndex;

        await Target.SendAsync(data);

        _rowIndex++;
    }

    protected override IDataflowBlock CreateBlock() => new BufferBlock<IRowDataModel>(ExecutionOptions);

    protected override bool Predicate(IRowDataModel model) => true;
}
