using DataEngine.Abstraction;
using DataEngine.Abstraction.Models;
using DataEngine.Core.DataBlocks.Interfaces;
using System;
using System.Threading.Tasks.Dataflow;

namespace DataEngine.Core.DataBlocks
{
    public abstract class DataBlockBase : IDataBlock
    {
        public ITargetBlock<IRowDataModel> Target => Block as ITargetBlock<IRowDataModel>;
        protected ISourceBlock<IRowDataModel> Source => Block as ISourceBlock<IRowDataModel>;
        private IDataflowBlock Block { get; set; }
        protected IStateMachine StateMachine { get; private set; }
        protected ExecutionDataflowBlockOptions ExecutionOptions { get; private set; } = new();

        protected DataBlockBase(IStateMachine stateMachine)
        {
            Id = Guid.NewGuid().ToString();
            StateMachine = stateMachine;
            Block = CreateBlock();
            Name = this.GetType().Name;
        }

        public string Id { get; private set; }

        public string Name { get; set; }

        public bool IsCancellationRequested = false;

        protected abstract IDataflowBlock CreateBlock();

        protected abstract bool Predicate(IRowDataModel model);

        public void LinkTo(IDataflowBlock block, DataflowLinkOptions linkOptions)
        {
            var dataBlock = block as IDataBlock;

            if (dataBlock is not null)
            {
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
