using DataEngine.Abstraction.Models;

namespace DataEngine.Abstraction
{
    public interface IStateMachine
    {
        public string StateId { get; }
        ITaskManagement TaskManagement { get; }
        void Save(StateMachineModel state);
    }
}
