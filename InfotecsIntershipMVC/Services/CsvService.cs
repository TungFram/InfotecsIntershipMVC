using CsvHelper;
using CsvHelper.Configuration;
using InfotecsIntershipMVC.DAL.Models;
using InfotecsIntershipMVC.Services.Exceptions;
using Newtonsoft.Json.Linq;
using System.Globalization;

namespace InfotecsIntershipMVC.Services
{
    public class CsvService : ICsvService
    {
        private readonly ILogger _logger;

        public CsvService(ILogger logger)
        {
            _logger = logger;
        }

        public IEnumerable<StringRecordEntity> ReadCSV(
            Stream fileStream, 
            string delimiter = ";", 
            int rowCount = 10000)
        {
            // TODO: сделать флаг хедеров в параметрах и читать хедеры в зависимости от него,
            // по-хорошему надо отдельный флаг или анализ сделать.

            var csvReaderConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false,
                Delimiter = delimiter,
            };

            var streamReader = new StreamReader(fileStream);
            var csvReader = new CsvReader(streamReader, csvReaderConfig);

            var records = new List<StringRecordEntity>();
            while (csvReader.Read())
            {
                if (records.Count >= rowCount)
                {
                    _logger.LogWarning($"File have to contain {rowCount} lines." +
                        $"Value is reached, file reading was stopped.");
                    break;
                }

                var record = new StringRecordEntity
                {
                    DateTime = csvReader.GetField(0),
                    Duraion = csvReader.GetField(1),
                    Value = csvReader.GetField(2),
                };
                records.Add(record);
            }
            return records;
        }
    }
}
