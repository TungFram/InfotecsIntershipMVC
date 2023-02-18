using InfotecsIntershipMVC.DAL.Models;

namespace InfotecsIntershipMVC.Services.Calculaing.CalculationCommands
{
    public class CalculateAllTime : AcCalculationOperation
    {
        public CalculateAllTime(IReadOnlyCollection<RecordEntity> records, ResultEntity result)
            : base(records, result)
        {
        }

        public override ResultEntity Execute()
        {
            DateTime maxTime = _records.Max(record => record.DateTime);
            DateTime minTime = _records.Min(record => record.DateTime);
            TimeSpan difference = maxTime - minTime;
            double allTime = difference.TotalSeconds;

            _result.AllTime = Convert.ToInt32(allTime);
            ToNextOperation();
            return _result;
        }
    }
}
