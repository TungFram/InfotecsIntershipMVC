using InfotecsIntershipMVC.DAL.Models;

namespace InfotecsIntershipMVC.Services.Calculaing.CalculationCommands
{
    public class CalculateMedianOfValues : AcCalculationOperation
    {
        public CalculateMedianOfValues(IReadOnlyCollection<RecordEntity> records, ResultEntity result) 
            : base(records, result)
        {
        }

        public override ResultEntity Execute()
        {
            var recordsSortedByValue = _records.OrderByDescending(record => record.Value);
            float median;

            int middleOfCollectionIndex = recordsSortedByValue.Count() / 2;
            if (recordsSortedByValue.Count() % 2 == 0) 
            {
                var rightPart = recordsSortedByValue.ElementAt(middleOfCollectionIndex).Value;
                var leftPart = recordsSortedByValue.ElementAt(middleOfCollectionIndex - 1).Value;
                median = (rightPart + leftPart) / 2;
            }
            else
            {
                median = recordsSortedByValue.ElementAt(middleOfCollectionIndex + 1).Value;
            }

            _result.MedianByValue = median;

            ToNextOperation();
            return _result;
        }
    }
}
