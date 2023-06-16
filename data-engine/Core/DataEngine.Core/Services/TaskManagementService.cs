using DataEngine.Abstraction;
using System.Collections.Concurrent;

namespace DataEngine.Core.Services;

public class TaskManagementService : ITaskManagement
{
    private readonly ConcurrentDictionary<string, CancellationTokenSource> _dic;

    public TaskManagementService()
    {
        _dic = new ConcurrentDictionary<string, CancellationTokenSource>();
    }

    public CancellationToken this[string id] => _dic[id].Token;

    public void Cancel(string id)
    {
        if (!_dic.TryGetValue(id, out var cancellationTokenSource))
        {
            throw new ArgumentException($"Not found token: {id}");
        }

        cancellationTokenSource.Cancel();
    }

    public CancellationToken CreateToken(string id)
    {
        if (_dic.TryGetValue(id, out var cancellationTokenSource))
        {
            return cancellationTokenSource.Token;
        }

        cancellationTokenSource = new CancellationTokenSource();

        if (_dic.TryAdd(id, cancellationTokenSource))
        {
            return cancellationTokenSource.Token;
        }

        throw new Exception("can't create CancellationTokenSource");
    }
}
