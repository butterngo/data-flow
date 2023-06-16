using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DataEngine.Abstraction.Models
{
    public class RowDataModel : IRowDataModel
    {
        public RowDataModel() : this(Guid.NewGuid().ToString()) { }

        public RowDataModel(string id) 
        {
            Id = id;
            Errors = new List<string>();
            _values = new List<IValueModel>();
        }

        public string Id { get; private set; }

        private List<IValueModel> _values;

        public List<string> Errors { get; set; }

        public IEnumerable<IValueModel> Values => _values;

        public int Index { get; set; }

        public string GenerateKey(string[] names)
        {
            var data = GetValues(names);

            if (data == null) return string.Empty;

            return string.Join("-", data.Select(x => x.Normalized()));
        }

        public IValueModel this[string name] => GetValue(name);

        public IEnumerable<IValueModel> this[string[] names] => GetValues(names);

        public string Serialize()
        {
            var sb = new StringBuilder();
            using (var sw = new StringWriter(sb))
            {
                using (var writer = new JsonTextWriter(sw))
                {
                    //writer.Formatting = Formatting.Indented;
                    writer.WriteStartObject();
                    foreach (var item in _values) 
                    {
                        item.Write(writer);
                    }
                    writer.WriteEndObject();
                }
            }
            return sb.ToString();
        }

        public void SetValue(IValueModel value)
        {
            if (!string.IsNullOrEmpty(value.Error))
            {
                Errors.Add(value.Error);
            }

            if (_values.Any(x => x.Property.Name.Equals(value.Property.Name)))
            {
                _values.Where(x => x.Property.Name.Equals(value.Property.Name))
                    .ToList()
                    .ForEach(item => item = value);
            }
            else 
            {
                _values.Add(value);
            }
        }

        private IValueModel GetValue(string name)
        {
            if (_values == null || !_values.Any()) return null;

            return _values.FirstOrDefault(x => x.Property.Name.Equals(name));
        }

        private IEnumerable<IValueModel> GetValues(string[] names)
        {
            if (_values == null || !_values.Any()) return null;

            return _values.Where(x => names.Contains(x.Property.Name));
        }
    }
}
