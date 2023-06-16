using DataEngine.Abstraction;
using DataEngine.Abstraction.Models;
using DataEngine.Core.DataBlocks;
using DataEngine.DataAccess.CSV;
using Moq;
using System.Threading.Tasks.Dataflow;

namespace DataEngine.Core.UnitTest.DataBlocks
{
    public class InputDataBlockTest
    {
        [Fact]
        public async Task Test1()
        {
            var raw_data = Path.Combine(Environment.CurrentDirectory, "TestData", "RawData", "raw_data.csv");

            var stream = new FileStream(raw_data, FileMode.Open);

            var dataProvider = new CsvDataInputProvider(stream);
            
            var stateMachine = new Mock<IStateMachine>();

            var linkOptions = new DataflowLinkOptions { PropagateCompletion = true };

            var inputBlock = new InputDataBlock(stateMachine.Object);

            var filteringDataBlock = new FilteringDataBlock(stateMachine.Object)
            {
                Property = new FilteringProperty
                {
                    Field = "TmStatus",
                    Value = "Unknow",
                    Type = ConditionEnum.NotEqual
                }
            };

            var actionBlock = new ActionBlock<IRowDataModel>(item =>
            {

            });

            inputBlock.LinkTo(filteringDataBlock, linkOptions);

            filteringDataBlock.LinkTo(actionBlock, linkOptions);

            foreach (var data in dataProvider.GetData(CancellationToken.None)) 
            {
                await inputBlock.SendAsync(data);
            }

            inputBlock.Complete();

            await actionBlock.Completion;
        }
    }
}