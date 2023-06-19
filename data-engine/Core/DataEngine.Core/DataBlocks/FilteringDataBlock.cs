using DataEngine.Abstraction;
using DataEngine.Abstraction.Interfaces;
using DataEngine.Abstraction.Models;
using DataEngine.Core.DataBlocks.Interfaces;
using Newtonsoft.Json;
using System.Threading.Tasks.Dataflow;

namespace DataEngine.Core.DataBlocks;

public class FilteringProperty
{
    [JsonProperty("condition")]
    public ConditionEnum Condition { get; set; }
    [JsonProperty("field")]
    public string Field { get; set; }
    [JsonProperty("value")]
    public string Value { get; set; }
}

public class FilteringDataBlock : DataBlockBase, IFilteringDataBlock
{
    [JsonProperty("property")]
    public FilteringProperty Property { get; set; }

    public FilteringDataBlock(IDataPipeline stateMachine) : base(stateMachine)
    {
    }

    protected override IDataflowBlock CreateBlock()
        => new TransformBlock<IRowDataModel, IRowDataModel>(model =>
        {
            if (WhereIterator(model)) return model;

            return null;
        }, ExecutionOptions);

    private bool WhereIterator(IRowDataModel model)
    {
        if (Property == null) return true;

        var item = model[Property.Field];

        if (item == null)
        {
            model.Errors.Add($"Filtering Flow is not found ${Property.Field}");
            return true;
        }

        switch (Property.Condition)
        {
            case ConditionEnum.Equal: return item.Value.Equals(Property.Value);
            case ConditionEnum.NotEqual: return !item.Value.Equals(Property.Value);
            default: return true;
        }
    }

    protected override bool OnPredicate(IRowDataModel model) 
    {
        if (model == null) 
        {
            return false;
        }

        return true;
    }
    
}
