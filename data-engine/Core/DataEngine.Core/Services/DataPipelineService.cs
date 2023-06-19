using DataEngine.Abstraction.Models;
using System.Threading.Channels;
using DataEngine.Abstraction.Interfaces;

namespace DataEngine.Core.Services;

public class DataPipelineService : IDataPipeline
{
    protected readonly Channel<StateMachineModel> _channel;

    public string StateId { get; private set; } = string.Empty;

    public ITaskManagement TaskManagement { get; private set; }

    public Func<StateMachineModel,Task> OnConsume { get; set; }

    private const int Capacity = 100000;

    public DataPipelineService(ITaskManagement taskManagementProcessor)
    {
        _channel = Channel.CreateUnbounded<StateMachineModel>();

        TaskManagement = taskManagementProcessor;
        StateId = Guid.NewGuid().ToString();
        Task.Run(async () =>
        {
            try
            {
                while (true)
                {
                    if (_channel.Reader.TryRead(out var data)) 
                    {
                        await SaveAsync(data);
                    }
                }
            }
            catch (TaskCanceledException)
            {
                Complete();
            }
        });
    }

    public async Task Publish(StateMachineModel state)
    {
        if (string.IsNullOrEmpty(StateId))
        {
            throw new InvalidOperationException("State is not initial");
        }

        if (TaskManagement[StateId].IsCancellationRequested)
        {
            Complete();
            return;
        }

        await _channel.Writer.WriteAsync(state);
    }

    protected Task SaveAsync(StateMachineModel state)
    {
        if (OnConsume is not null) 
        {
            OnConsume(state);
        }
        return Task.CompletedTask;
    }

    private void Complete() => _channel.Writer.Complete();
}
