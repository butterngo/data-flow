using DataEngine.Abstraction.Models;
using DataEngine.Abstraction;
using System.Threading.Channels;

namespace DataEngine.Core.Services
{
    public class StateMachineService : IStateMachine
    {
        protected readonly Channel<StateMachineModel> _channel;

        public string StateId { get; private set; } = string.Empty;

        public ITaskManagement TaskManagement { get; private set; }

        public StateMachineService(ITaskManagement taskManagementProcessor)
        {
            _channel = Channel.CreateUnbounded<StateMachineModel>();
            TaskManagement = taskManagementProcessor;
            StateId = Guid.NewGuid().ToString();
            Task.Run(async () =>
            {
                try
                {
                    while (await _channel.Reader.WaitToReadAsync(TaskManagement[StateId]))
                    {
                        await SaveAsync(await _channel.Reader.ReadAsync());
                    }
                }
                catch (TaskCanceledException)
                {
                    Complete();
                }
            });
        }

        public void Save(StateMachineModel state)
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

            if (!_channel.Writer.TryWrite(state))
            {
                //TODO
            }
        }

        protected Task SaveAsync(StateMachineModel state)
        {
            return Task.CompletedTask;
        }

        private void Complete() => _channel.Writer.Complete();
    }
}
