using DataEngine.Core.DataBlocks.Interfaces;
using DataEngine.Core;
using DataEngine.DataAccess.CSV;
using Microsoft.AspNetCore.Mvc;
using DataEngine.Abstraction.Interfaces;

namespace DataEngine.Apis.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DataFlowsController : ControllerBase
    {
        private static readonly string _equipment_class = Path.Combine(Environment.CurrentDirectory, "TestData", "RawData", "equipment_class.csv");
        private static readonly string _categories = Path.Combine(Environment.CurrentDirectory, "TestData", "RawData", "categories.csv");

        private readonly ILogger<DataFlowsController> _logger;

        private readonly IDataFlowFactory _dataFlowFactory;

        private readonly ITaskManagement _taskManagement;

        private readonly IDataPipeline _dataPipeline;

        public DataFlowsController(ILogger<DataFlowsController> logger, IDataFlowFactory dataFlowFactory,
            ITaskManagement taskManagement, IDataPipeline dataPipeline)
        {
            _logger = logger;
            _dataFlowFactory = dataFlowFactory;
            _taskManagement = taskManagement;
            _dataPipeline = dataPipeline;
        }

        [HttpPost]
        public async Task Upload(IFormFile file)
        {
            var json = @"{
  ""InputDataBlock"": {
    ""id"": ""4e3855e4-0c1b-11ee-be56-0242ac120002"",
    ""name"": ""Load data from csv"",
    ""linkId"": ""58c2b37e-0c1b-11ee-be56-0242ac120002""
  },
  ""FilteringDataBlock"": {
    ""id"": ""58c2b37e-0c1b-11ee-be56-0242ac120002"",
    ""name"": ""Filter data Unknow"",
    ""linkId"": ""6fda3908-0c1e-11ee-be56-0242ac120002"",
    ""property"": {
      ""field"": ""TmStatus"",
      ""Value"": ""Unknow"",
      ""condition"": ""NotEqual""
    }
  },
  ""OutputDataBlock"": {
    ""id"": ""6fda3908-0c1e-11ee-be56-0242ac120002"",
    ""name"": ""Sink data to console"",
    ""linkId"": null
  }
}
";
            _taskManagement.CreateToken(_dataPipeline.StateId);

            var dataProvider = new CsvDataInputProvider(file.OpenReadStream());

            var (input, output) = _dataFlowFactory.Create(json);

            foreach (var item in dataProvider.GetData(CancellationToken.None))
            {
                await (input as IInputDataBlock).SendAsync(item);
            }

            input.Complete();

            await output.Completion;
        }
    }
}
