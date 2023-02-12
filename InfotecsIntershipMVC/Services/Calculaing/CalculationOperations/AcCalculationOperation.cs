using InfotecsIntershipMVC.DAL.Models;

namespace InfotecsIntershipMVC.Services.Calculaing.CalculationCommands
{
    public abstract class AcCalculationOperation
    {
        private AcCalculationOperation? _nextHandler;
        public abstract ResultEntity Execute(IReadOnlyCollection<RecordEntity> records, ResultEntity result);

        public AcCalculationOperation WithNextOperation(AcCalculationOperation nextHandler)
        {
            _nextHandler = nextHandler;
            return this;
        }

        /*public void ToNextOperation()
        {
            if (_nextHandler == null)
                return;
            _nextHandler.Execute(re);
        }*/
    }
}
