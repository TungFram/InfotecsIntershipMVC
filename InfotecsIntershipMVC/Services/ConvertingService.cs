using InfotecsIntershipMVC.DAL.Models;
using InfotecsIntershipMVC.Services.Exceptions;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System.Globalization;
using System.Text.RegularExpressions;

namespace InfotecsIntershipMVC.Services
{
    public class ConvertingService
    {
        private readonly ILogger _logger;

        public ConvertingService(ILogger logger)
        {
            _logger = logger;
        }

        public IEnumerable<RecordEntity> ConvertFileData(
            IEnumerable<StringRecordEntity> fileData,
            string fileName)
        {
            var pattern = new Regex(@"[A-Za-z][\w ]+\.csv");
            if (!pattern.IsMatch(fileName))
            {
                throw new ArgumentException("Unacceptable file name.");
            }


            var result = new List<RecordEntity>();

            foreach (StringRecordEntity record in fileData)
            {
                try
                {
                    DateTime    convertedDateTime   = ConvertDateTime(record.DateTime);
                    int         convertedDuration   = ConvertDuration(record.Duraion);
                    float       convertedValue      = ConvertValue(record.Value);

                    var newRecord = new RecordEntity()
                    {
                        DateTime = convertedDateTime,
                        Duraion = convertedDuration,
                        Value = convertedValue,
                    };

                    result.Add(newRecord);

                } // Different catches for every type of exception for
                  // the possibility of adding flexible logic.
                catch (InvalidDateTimeException dtEx)
                {
                    _logger.LogWarning($"Row {record.ToString} has been skipped: {dtEx.Message}");
                    continue;
                }
                catch (InvalidDurationException dEx)
                {
                    _logger.LogWarning($"Row {record.ToString} has been skipped: {dEx.Message}");
                    continue;
                }
                catch (InvalidValueException vEx)
                {
                    _logger.LogWarning($"Row {record.ToString} has been skipped: {vEx.Message}");
                    continue;
                }
                catch (ArgumentNullException anEx)
                {
                    _logger.LogError($"Argument was null: {anEx.Message}. " +
                        $"Row {record.ToString} has been skipped.");
                    continue;
                }
                catch (ArgumentException aEx)
                {
                    _logger.LogError($"Argument was incorrect: {aEx.Message}. " +
                        $"Row {record.ToString} has been skipped.");
                    continue;
                }
                catch (Exception ex)
                {
                    _logger.LogCritical($"Unknown error: {ex.Message}!");
                    throw;
                }
            }

            var fileEntity = new FileEntity(fileName, result);
            // Way 1:
            // 1) добавить файл в таблицу.
            // 2) получить его оттуда.
            // 3) взять его id.
            // 4) присвоить всем (новый цикл).
            // 5) сохранить все записи.

            // Way 2:
            // 1) добавить файл в таблицу.
            // 2) ef сам увидит, что записи зависят от файла и добавит их в соответствующую таблицу.
        }

        private DateTime ConvertDateTime(string oldDateTime, string? pattern = null)
        {
            if (string.IsNullOrEmpty(oldDateTime))
            {
                throw new ArgumentNullException("File date and time field is empty.");
            }

            pattern ??= "yyyy-MM-dd_HH-mm-ss";

            var newDateTime = new DateTime();
            if (!DateTime.TryParseExact(
                oldDateTime,
                pattern,
                null,
                DateTimeStyles.None,
                out newDateTime))
            {
                throw new ArgumentException($"File DateTime is incorrect for parsing ({oldDateTime})!");
            }

            if (newDateTime < new DateTime(2000, 1, 1) || newDateTime > DateTime.Now)
            {
                throw new InvalidDateTimeException("DateTime wasn't fit the conditions of the task!");
            }

            return newDateTime;
        }

        private int ConvertDuration(string oldDuration)
        {
            if (string.IsNullOrEmpty(oldDuration))
            {
                throw new ArgumentNullException("File duration field is empty or null.");
            }

            int newDuration;
            if (!int.TryParse(oldDuration, out newDuration))
            {
                throw new ArgumentException($"Can't parse {oldDuration} to number.");
            }

            if (newDuration < 0)
            {
                throw new InvalidDurationException("Duration must be positive.");
            }

            return newDuration;
        }

        private float ConvertValue(string oldValue)
        {
            if (string.IsNullOrEmpty(oldValue))
            {
                throw new ArgumentNullException("File value field is empty or null.");
            }

            float newValue;
            if (!float.TryParse(oldValue, out newValue))
            {
                throw new ArgumentException($"Can't parse {oldValue} to float.");
            }

            if (newValue < 0)
            {
                throw new InvalidDurationException("Value must be positive.");
            }

            return newValue;
        }
    }
}
