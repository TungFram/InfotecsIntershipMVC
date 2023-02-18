using InfotecsIntershipMVC.DAL.Models;

namespace InfotecsIntershipMVC.Services.Calculaing.CalculationCommands
{
    public class CalculateMinValue : AcCalculationOperation
    {
        public CalculateMinValue(IReadOnlyCollection<RecordEntity> records, ResultEntity result)
            : base(records, result)
        {
        }

        public override ResultEntity Execute()
        {
            float minValue = _records.Min(record => record.Value);
            _result.MinValue = minValue;

            ToNextOperation();
            return _result;
        }
    }
}
