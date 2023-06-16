using DataEngine.Abstraction.Models;
using DataEngine.DataAccess.CSV;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Linq;

namespace DataEngine.DataAccess.UnitTest
{
    [TestClass]
    public class CsvDataProviderTest
    {
        private readonly string _jsonSchemaFile;

        private readonly string _csvFile;

        public CsvDataProviderTest() 
        {
            _jsonSchemaFile = Path.Combine(Environment.CurrentDirectory, "TestData", "json_schema.json");
            _csvFile = Path.Combine(Environment.CurrentDirectory, "TestData", "raw_data.csv");
        }

        [TestMethod]
        public void Serialize_String_To_JsonSchema_Return_Successful()
        {
            var fileStream = new FileStream(_csvFile, FileMode.Open);

            using (var dataPovider = new CsvDataInputProvider(fileStream))
            {
                dataPovider.JsonSchema.Should().NotBeNull();

                dataPovider.JsonSchema.Properties.Should().NotBeEmpty().And.HaveCount(8);

                var expectedJsonSchema = JToken.Parse(File.ReadAllText(_jsonSchemaFile));

                var actualJsonSchema = JToken.Parse(dataPovider.JsonSchema.Serialize());

                expectedJsonSchema.Should().BeEquivalentTo(actualJsonSchema);
            }
        }

        [TestMethod]
        public void Deserialize_String_To_JsonSchema_Return_Successful()
        {
            var jsonSchema = JsonSchemaModel.Deserialize(File.ReadAllText(_jsonSchemaFile));

            jsonSchema.Title.Should().Equals("test_model");

            jsonSchema.Type.Should().Equals("object");

            jsonSchema.Schema.Should().Equals("https://json-schema.org/draft/2020-12/schema");

            jsonSchema.Properties.Should().NotBeEmpty().And.HaveCount(8);
        }

        [TestMethod]
        public void GetData_Return_Successful()
        {
            var fileStream = new FileStream(_csvFile, FileMode.Open);

            using (var dataPovider = new CsvDataInputProvider(fileStream))
            {
                var result = dataPovider.GetData().ToList();

                result.Should().NotBeEmpty().And.HaveCount(28);

                result.ForEach(item => 
                {
                    item.Values.Should().NotBeEmpty().And.HaveCount(8);
                    item.Serialize().Should().NotBeNull();
                });
            }
        }
    }
}
