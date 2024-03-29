﻿using CsvHelper;
using CsvHelper.Configuration;
using InfotecsIntershipMVC.DAL.Models;
using Newtonsoft.Json.Linq;
using System.Globalization;

namespace InfotecsIntershipMVC.Services.CSV
{
    public class CsvService : ICsvService
    {
        private readonly ILogger<CsvService> _logger;

        public CsvService(ILogger<CsvService> logger)
        {
            _logger = logger;
        }

        public IEnumerable<StringRecordEntity> ReadCSV(
            Stream fileStream,
            string delimiter = ";",
            int rowLimit = 10000)
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
                if (records.Count >= rowLimit)
                {
                    _logger.LogWarning($"File have to contain {rowLimit} lines." +
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
