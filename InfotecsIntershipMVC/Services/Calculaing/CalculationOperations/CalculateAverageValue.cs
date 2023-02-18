using InfotecsIntershipMVC.DAL.Models;

namespace InfotecsIntershipMVC.Services.Calculaing.CalculationCommands
{
    public class CalculateAverageValue : AcCalculationOperation
    {
        public CalculateAverageValue(IReadOnlyCollection<RecordEntity> records, ResultEntity result)
            : base(records, result)
        {
        }

        public override ResultEntity Execute()
        {
            float value = _records.Average(record => record.Value);
            _result.AverageValue = value;

            ToNextOperation();
            return _result;
        }
    }
}
