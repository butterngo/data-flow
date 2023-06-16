using Newtonsoft.Json;
using System;

namespace DataEngine.Abstraction.Models
{
    public class ValueModel : IValueModel
    {
        public ValueModel(IPropertyModel property, string value) 
        {
            Id = Guid.NewGuid().ToString();
            Property = property;
            Value = value;
        }

        public string Id { get; private set; }

        public IPropertyModel Property { get; private set; }

        private object _value;

        public string Value 
        {
            get => _value?.ToString();
            set 
            {
                _value = value;
            }
        }

        public string Error { get; private set; }

        public string Normalized()
        {
            if (string.IsNullOrEmpty(Value)) return Value.ToLower();

            return Value.ToLower().Replace(" ","");
        }

        public virtual void Write(JsonTextWriter writer)
        {
            try 
            {
                writer.WritePropertyName(Property.Name);
                switch (Property.Type)
                {
                    case JsonObjectTypeEnum.None:
                        {
                            writer.WriteValue(Value);
                            break;
                        }
                    case JsonObjectTypeEnum.Array:
                        {
                            throw new NotImplementedException("Not implemnet JsonObjectType.Array yet.");
                        }
                    case JsonObjectTypeEnum.Boolean:
                        {
                            if (bool.TryParse(Value, out var result))
                            {
                                writer.WriteValue(result);
                            }
                            break;
                        }
                    case JsonObjectTypeEnum.Integer:
                        {
                            if (int.TryParse(Value, out var result))
                            {
                                writer.WriteValue(result);
                            }
                            break;
                        }
                    case JsonObjectTypeEnum.Null:
                        {
                            writer.WriteValue(Value);
                            break;
                        }
                    case JsonObjectTypeEnum.Number:
                        {
                            if (double.TryParse(Value, out var result))
                            {
                                writer.WriteValue(result);
                            }
                            break;
                        }
                    case JsonObjectTypeEnum.Object:
                        {
                            throw new NotImplementedException("Not implemnet JsonObjectType.Object yet.");
                        }
                    case JsonObjectTypeEnum.String:
                        {
                            writer.WriteValue(Value);
                            break;
                        }
                    case JsonObjectTypeEnum.Date:
                        {
                            if (DateTime.TryParse(Value, out var result))
                            {
                                writer.WriteValue(result);
                            }
                            break;
                        }
                    case JsonObjectTypeEnum.DateTime:
                        {
                            if (DateTime.TryParse(Value, out var result))
                            {
                                writer.WriteValue(result);
                            }
                            break;
                        }
                    case JsonObjectTypeEnum.Time:
                        {
                            writer.WriteValue(Value);
                            break;
                        }
                    default: throw new NotImplementedException($"Unknow Type {Property.Type.ToString()}");
                }
            }
           catch(Exception ex) 
            {
            }
        }
    }
}
