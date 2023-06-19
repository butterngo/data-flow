using DataEngine.Core.DataBlocks.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks.Dataflow;

namespace DataEngine.Core;

public interface IDataFlowFactory
{
    (IDataBlock input, IDataBlock output) Create(string json);
}

public class DataFlowFactory : IDataFlowFactory
{
    private readonly IServiceProvider _provider;

    public DataFlowFactory(IServiceProvider provider) 
        => _provider = provider;

    private static Lazy<Dictionary<string, Type>> _intance = new Lazy<Dictionary<string, Type>>(() => 
    {
        var path = Path.Combine("Data", "data-blocks.json");

        var json = File.ReadAllText(path);

        return JsonConvert.DeserializeObject<Dictionary<string, Type>>(json);
    });

    public static Type GetType(string name) => _intance.Value[name];

    public (IDataBlock input, IDataBlock output) Create(string json) 
    {
        var items = JObject.Parse(json);

        var instances = new List<IDataBlock>();

        var linkOptions = new DataflowLinkOptions { PropagateCompletion = true };

        foreach (var item in items)
        {
            var block = Create(item);

            instances.Add(block);
        }

        foreach (var instance in instances)
        {
            var target = instances.FirstOrDefault(x => x.Id.Equals(instance.LinkId));

            if (target != null) 
            {
                instance.LinkTo(target, linkOptions);
            }
        }

        return (instances.First(), instances.Last());
    }

    private IDataBlock Create(KeyValuePair<string, JToken> item)
    {
        var instance = _provider.GetService(GetType(item.Key)) as IDataBlock;

        instance.Id = item.Value.Value<string>("id");

        instance.Name = item.Value.Value<string>("name");
        
        instance.LinkId = item.Value.Value<string>("linkId");

        return instance;
    }
}
