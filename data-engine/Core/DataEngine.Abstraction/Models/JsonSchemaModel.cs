using DataEngine.Abstraction.Extentions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DataEngine.Abstraction.Models
{
    public class JsonSchemaModel : IJsonSchemaModel
    {
        public JsonSchemaModel() 
        {
            Schema = "https://json-schema.org/draft/2020-12/schema";
            Type = "object";
            Id = Guid.NewGuid();
        }

        public Guid Id { get; private set; }

        public string Schema { get; set; }

        public string Title { get; set; }

        public string Type { get; set; }

        public IEnumerable<IPropertyModel> Properties { get; set; }

        public IEnumerable<string> Required { get; set; }

        public virtual string Serialize()
        {
            var sb = new StringBuilder();
            using (var sw = new StringWriter(sb))
            {
                var requiredFields = new List<string>();

                using (var writer = new JsonTextWriter(sw))
                {
                    //writer.Formatting = Formatting.Indented;

                    writer.WriteStartObject();
                    writer.WritePropertyName("$schema");
                    writer.WriteValue(Schema);
                    writer.WritePropertyName("title");
                    writer.WriteValue(Title);
                    writer.WritePropertyName("type");
                    writer.WriteValue(Type);
                    writer.WritePropertyName("properties");
                    writer.WriteStartObject();
                    foreach (var prop in Properties)
                    {
                        prop.Write(writer);
                    }
                    writer.WriteEnd();
                    if (requiredFields != null || requiredFields.Count() != 0)
                    {
                        writer.WritePropertyName("required");
                        writer.WriteStartArray();
                        requiredFields.ForEach(x => writer.WriteValue(x));
                    }
                    writer.WriteEndObject();
                }

                return sb.ToString();
            }
        }

        public static IJsonSchemaModel Deserialize(string json)
        {
            if (string.IsNullOrEmpty(json))
            {
                throw new NullReferenceException("Parameter should not be null or empty.");
            }
            var jObj = JObject.Parse(json);

            var properties = jObj.Property("properties").Value.ToList();

            var model = new JsonSchemaModel
            {
                Schema = jObj.Value<string>("$Schema"),
                Title = jObj.Value<string>("title"),
                Type = jObj.Value<string>("type"),
                Properties = properties.Select(token => 
                {
                    var name = ((JProperty)token).Name;
                    return new PropertyModel
                    {
                        Name = name,
                        Type = token.Last.Value<string>("type").ToJsonTypeEnum()
                    };
                })
            };

            return model;
        }
    }
}
