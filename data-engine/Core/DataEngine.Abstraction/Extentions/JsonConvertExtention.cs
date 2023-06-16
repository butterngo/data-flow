namespace DataEngine.Abstraction.Extentions
{
    public static class JsonConvertExtention
    {
        public static JsonObjectTypeEnum ToJsonTypeEnum(this string value) 
        {
            switch (value)
            {
                case "none": return JsonObjectTypeEnum.None;
                case "array": return JsonObjectTypeEnum.Array;
                case "boolean": return JsonObjectTypeEnum.Boolean;
                case "integer": return JsonObjectTypeEnum.Integer;
                case "null": return JsonObjectTypeEnum.Null;
                case "number": return JsonObjectTypeEnum.Number;
                case "object": return JsonObjectTypeEnum.Object;
                case "string": return JsonObjectTypeEnum.String;
                case "date-time": return JsonObjectTypeEnum.DateTime;
                case "date": return JsonObjectTypeEnum.Date;
                case "time": return JsonObjectTypeEnum.Time;
                default: return JsonObjectTypeEnum.Null;
            }
        }

        public static string ToJsonTypeString(this JsonObjectTypeEnum type)
        {
            switch (type)
            {
                case JsonObjectTypeEnum.None: return "none";
                case JsonObjectTypeEnum.Array: return "array";
                case JsonObjectTypeEnum.Boolean: return "boolean";
                case JsonObjectTypeEnum.Integer: return "integer";
                case JsonObjectTypeEnum.Null: return "null";
                case JsonObjectTypeEnum.Number: return "number";
                case JsonObjectTypeEnum.Object: return "object";
                case JsonObjectTypeEnum.String: return "string";
                case JsonObjectTypeEnum.DateTime: return "date-time";
                case JsonObjectTypeEnum.Date: return "date";
                case JsonObjectTypeEnum.Time: return "time";
                default: return "null";
            }
        }
    }
}
