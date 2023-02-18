using InfotecsIntershipMVC.DAL.Models;

namespace InfotecsIntershipMVC.Services.Calculaing.CalculationCommands
{
    public class CalculateRowCount : AcCalculationOperation
    {
        public CalculateRowCount(IReadOnlyCollection<RecordEntity> records, ResultEntity result) 
            : base(records, result)
        {
        }

        public override ResultEntity Execute()
        {
            int rowCount = _records.Count;
            _result.RowCount = rowCount;

            ToNextOperation();
            return _result;
        }
    }
}
