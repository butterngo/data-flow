using DataEngine.Abstraction.Models;
using DataEngine.DataAccess.Abstraction;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DataEngine.DataAccess.CSV
{
    public class CsvDataOuputProvider : IDataOuputProvider
    {
        private const string Separator = ",";
        public IJsonSchemaModel JsonSchema { get; set; }
        private readonly Stream _stream;
        private readonly StreamWriter _writer;

        private bool IsFirstSave = true;

        public CsvDataOuputProvider(Stream stream) 
        {
            _stream = stream;
            _writer = new StreamWriter(_stream);
        }

        public async Task SinkAsync(IEnumerable<IRowDataModel> data)
        {
            foreach (var row in data)
            {
                await SinkAsync(row);
            }
        }

        public Task SinkAsync(IRowDataModel data)
        {
            lock (this)
            {
                _stream.Seek(0, SeekOrigin.End);
             
                if (IsFirstSave)
                {
                    _writer.WriteLine(GenerateCsvLine(JsonSchema.Properties));
                    IsFirstSave = false;
                }
                var line = GenerateCsvLine(data);

                _writer.WriteLine(line);
            }
            return Task.CompletedTask;
        }

        private string GenerateCsvLine(IEnumerable<IPropertyModel> properties)
        {
            var data = properties.Select(x => x.Name);

            return string.Join(Separator, data);
        }

        private string GenerateCsvLine(IRowDataModel row)
        {
            var data = JsonSchema.Properties.Select(prop => 
            {
                var item = row[prop.Name];

                if (item == null) return string.Empty;

                return item.Value;
            });
            
            return string.Join(Separator, data);
        }

        public void Dispose()
        {
            _writer.Dispose();
            _stream.Dispose();
        }
    }
}
