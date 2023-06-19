using DataEngine.Abstraction.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DataEngine.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class DataPipelineController : ControllerBase
{
    private readonly IDataPipeline _dataPipeline;

    public DataPipelineController(IDataPipeline dataPipeline) => _dataPipeline = dataPipeline;

    [HttpGet("sse")]
    public async Task SSE()
    {
        Response.ContentType = "text/event-stream";

        _dataPipeline.OnConsume = async item =>
        {
            string json = $"data: {item.Data.Serialize()}\n\n";

            await Task.Delay(500);

            await Response.WriteAsync(json);

            await Response.Body.FlushAsync();
        };
    }
}
