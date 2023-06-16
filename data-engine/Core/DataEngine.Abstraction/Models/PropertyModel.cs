using DataEngine.Abstraction.Extentions;
using Newtonsoft.Json;
using System;

namespace DataEngine.Abstraction.Models
{
    public class PropertyModelBase  
    {
        public PropertyModelBase() => Id = Guid.NewGuid();
      
        public PropertyModelBase(string name) 
            : this()
        {
            Name = name;
            Type = JsonObjectTypeEnum.None;
        }

        public PropertyModelBase(string name, JsonObjectTypeEnum type) 
            : this(name)
        {
            Type = type;
        }

        public PropertyModelBase(string name, JsonObjectTypeEnum type, string pattern) 
            : this(name, type)
        {
            Pattern = pattern;
        }

        [JsonIgnore]
        public Guid Id { get; private set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public JsonObjectTypeEnum Type { get; set; }

        [JsonProperty("pattern")]
        public string Pattern { get; set; }
    }

    public class PropertyModel : PropertyModelBase, IPropertyModel
    {
        public PropertyModel() : base() { }

        public PropertyModel(string name) : base()
        {
            Name = name;
            Type = JsonObjectTypeEnum.None;
        }

        public PropertyModel(string name, JsonObjectTypeEnum type) : base(name)
        {
            Type = type;
        }

        public PropertyModel(string name, JsonObjectTypeEnum type, string pattern) : base(name, type)
        {
            Pattern = pattern;
        }

        public string Description { get; set; }
        
        public virtual void Write(JsonTextWriter writer)
        {
            writer.WritePropertyName(Name);
            writer.WriteStartObject();
            writer.WritePropertyName("type");
            writer.WriteValue(Type.ToJsonTypeString());
            writer.WritePropertyName("description");
            writer.WriteValue(Description);
            writer.WriteEnd();
        }
    }
}
