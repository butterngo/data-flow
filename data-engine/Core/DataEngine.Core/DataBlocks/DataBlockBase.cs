using DataEngine.Abstraction.Interfaces;
using DataEngine.Abstraction.Models;
using DataEngine.Core.DataBlocks.Interfaces;
using System.Threading.Tasks.Dataflow;

namespace DataEngine.Core.DataBlocks
{
    public abstract class DataBlockBase : IDataBlock
    {
        public ITargetBlock<IRowDataModel> Target => Block as ITargetBlock<IRowDataModel>;
        protected ISourceBlock<IRowDataModel> Source => Block as ISourceBlock<IRowDataModel>;
        private IDataflowBlock Block { get; set; }
        protected IDataPipeline DataPipeline { get; private set; }
        protected ExecutionDataflowBlockOptions ExecutionOptions { get; private set; } = new();

        protected DataBlockBase(IDataPipeline dataPipeline)
        {
            Id = Guid.NewGuid().ToString();
            DataPipeline = dataPipeline;
            Block = CreateBlock();
            Name = this.GetType().Name;
        }

        public string Id { get; set; }

        public string LinkId { get; set; }

        public string Name { get; set; }

        public bool IsCancellationRequested = false;

        protected abstract IDataflowBlock CreateBlock();

        protected abstract bool OnPredicate(IRowDataModel model);

        private bool Predicate(IRowDataModel model) 
        {
            if (model != null)
            {
                DataPipeline.Publish(new StateMachineModel 
                {
                    Id = Id,
                    Name= Name,
                    Data = model,
                });
            }
            return OnPredicate(model);
        }

        public void LinkTo(IDataflowBlock block, DataflowLinkOptions linkOptions)
        {
            var dataBlock = block as IDataBlock;

            if (dataBlock is not null)
            {
                LinkId = dataBlock.Id;

                Source.LinkTo(dataBlock.Target, linkOptions, Predicate);
            }
            else 
            {
                Source.LinkTo(block as ITargetBlock<IRowDataModel>, linkOptions, Predicate);
            }

            Source.LinkTo(DataflowBlock.NullTarget<IRowDataModel>(), linkOptions);
        }

        public void Complete() => Block.Complete();
        
        public void Fault(Exception exception) => Block.Fault(exception);

        public Task Completion => Block.Completion;
    }
}
