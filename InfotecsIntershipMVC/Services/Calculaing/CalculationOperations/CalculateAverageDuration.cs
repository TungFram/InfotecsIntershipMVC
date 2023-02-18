using InfotecsIntershipMVC.DAL.Models;

namespace InfotecsIntershipMVC.Services.Calculaing.CalculationCommands
{
    public class CalculateAverageDuration : AcCalculationOperation
    {
        public CalculateAverageDuration(IReadOnlyCollection<RecordEntity> records, ResultEntity result)
            : base(records, result)
        {
        }

        public override ResultEntity Execute()
        {
            int allDuration = _records.Select(record => record.Duraion).Sum();
            int count = _records.Count();
            _result.AverageDuration = allDuration / count;

            ToNextOperation();
            return _result;
        }
    }
}
