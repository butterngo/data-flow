using DataEngine.Abstraction.Interfaces;
using DataEngine.Abstraction.Models;
using DataEngine.Core.DataBlocks.Interfaces;
using System.Threading.Tasks.Dataflow;

namespace DataEngine.Core.DataBlocks
{
    public class OutputDataBlock : DataBlockBase, IOutputDataBlock
    {
        public OutputDataBlock(IDataPipeline stateMachine) : base(stateMachine)
        {
        }

        public Action<IRowDataModel> OnSink { get; set; }

        protected override IDataflowBlock CreateBlock() => new ActionBlock<IRowDataModel>(item => { });
        

        protected override bool OnPredicate(IRowDataModel model) => true;
        
    }
}
