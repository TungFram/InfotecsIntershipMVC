using InfotecsIntershipMVC.DAL.Models;
using InfotecsIntershipMVC.DAL.Repositories;

namespace InfotecsIntershipMVC.Services.Calculaing.CalculationCommands
{
    public abstract class AcCalculationOperation
    {
        protected AcCalculationOperation? _nextHandler;
        protected IReadOnlyCollection<RecordEntity> _records;
        protected ResultEntity _result;

        public AcCalculationOperation(IReadOnlyCollection<RecordEntity> records, ResultEntity result)
        {
            _records = records;
            _result = result;
            if (!IsDataValid()) 
                throw new ArgumentNullException("Result entity or file doesn't exist, or file might doesn't contain any rows.");
        }

        public abstract ResultEntity Execute();

        public AcCalculationOperation? WithNextOperation(AcCalculationOperation? nextHandler)
        {
            _nextHandler = nextHandler;
            return nextHandler;
        }

        protected bool IsDataValid()
        {
            if (_result == null
                || _records == null
                || _records.Count == 0)
                return false;

            return true;
        }

        protected void ToNextOperation()
        {
            if (_nextHandler == null)
                return;
            _nextHandler.Execute();
        }
    }
}
