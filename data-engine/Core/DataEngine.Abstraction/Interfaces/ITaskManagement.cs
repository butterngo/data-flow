using System.Threading;

namespace DataEngine.Abstraction.Interfaces
{
    public interface ITaskManagement
    {
        CancellationToken this[string id] { get; }
        CancellationToken CreateToken(string id);
        void Cancel(string id);
    }
}
