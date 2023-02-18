using InfotecsIntershipMVC.DAL.Models;

namespace InfotecsIntershipMVC.Services.Calculaing.CalculationCommands
{
    public class CalculateMaxValue : AcCalculationOperation
    {
        public CalculateMaxValue(IReadOnlyCollection<RecordEntity> records, ResultEntity result)
            : base(records, result)
        {
        }

        public override ResultEntity Execute()
        {
            float maxValue = _records.Max(record => record.Value);
            _result.MaxValue = maxValue;

            ToNextOperation();
            return _result;
        }
    }
}
