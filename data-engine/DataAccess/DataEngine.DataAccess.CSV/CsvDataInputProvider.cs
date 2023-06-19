using System.Linq;
using System.Collections.Generic;
using System.IO;
using DataEngine.Abstraction.Models;
using System.Threading;
using DataEngine.Abstraction.Interfaces;

namespace DataEngine.DataAccess.CSV
{
    public class CsvDataInputProvider : IDataInputProvider
    {
        private const string Separator = ",";
        
        private readonly Stream _stream;

        private readonly StreamReader _reader;

        public IJsonSchemaModel JsonSchema { get; private set; }

        public CsvDataInputProvider(Stream stream) 
        {
            _stream = stream;
            _reader = new StreamReader(_stream);
            SetJsonSchema();
        }

        public IEnumerable<IRowDataModel> GetData(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            string line = null;

            while ((line = _reader.ReadLine()) != null)
            {
                var model = new RowDataModel();

                var fields = line.Split(Separator);

                var properties = JsonSchema.Properties.ToArray();

                var length = fields.Length;

                for (var index = 0; index < length; index++)
                {
                    model.SetValue(new ValueModel(properties[index], fields[index]));
                }

                yield return model;
            }

            yield break;
        }

        public IAsyncEnumerable<IRowDataModel> GetDataAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            throw new System.NotImplementedException();
        }

        private void SetJsonSchema()
        {
            var line = _reader.ReadLine();

            var fields = line.Split(Separator);

            JsonSchema = new JsonSchemaModel
            {
                Title = "ModelInput",
                Properties = fields.Select(name => new PropertyModel(name))
            };
        }

        public void Dispose()
        {
            _reader.Dispose();
            _stream.Dispose();
        }
    }
}
