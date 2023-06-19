using DataEngine.Abstraction.Models;

namespace DataEngine.Abstraction.Interfaces
{
    public interface IDataPipeline
    {
        public string StateId { get; }
        ITaskManagement TaskManagement { get; }
        Task Publish(StateMachineModel state);
        Func<StateMachineModel,Task> OnConsume { get; set; }
    }
}
