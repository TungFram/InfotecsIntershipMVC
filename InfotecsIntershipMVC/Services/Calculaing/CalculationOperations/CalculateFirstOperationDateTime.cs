using InfotecsIntershipMVC.DAL.Models;

namespace InfotecsIntershipMVC.Services.Calculaing.CalculationCommands
{
    public class CalculateFirstOperationDateTime : AcCalculationOperation
    {
        public CalculateFirstOperationDateTime(IReadOnlyCollection<RecordEntity> records, ResultEntity result)
            : base(records, result)
        {
        }

        public override ResultEntity Execute()
        {
            DateTime firstOperation = _records.Min(record => record.DateTime);
            _result.FirstOperation = firstOperation;

            ToNextOperation();
            return _result;
        }
    }
}
