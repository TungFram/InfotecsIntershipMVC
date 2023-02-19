using InfotecsIntershipMVC.DAL.Models;
using InfotecsIntershipMVC.Services.Calculaing.CalculationCommands;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace InfotecsIntershipMVC.Services.Calculaing
{
    public class CalculatingService // TODO: make this service as parameter for main service
    {
        private readonly ImmutableList<RecordEntity> _records;
        private ResultEntity _result;

        private AcCalculationOperation _firstChainedOperation = null;
        private List<AcCalculationOperation> _configuration = null;

        public List<AcCalculationOperation> GetConfiguration => _configuration;

        public CalculatingService(ImmutableList<RecordEntity> records,
            ResultEntity result)
        {
            if (result == null || string.IsNullOrEmpty(result.FileName))
                throw new ArgumentNullException
                    ("Can't handle empty result or result without name of file!");
            _result = result; 

            if (records == null || records.Count == 0)
            {
                throw new ArgumentNullException
                    ("Can't perform any operations with a non-existent or empty list of records!");
            }

            _records = records;
        }

        public CalculatingService WithOperations(List<AcCalculationOperation>? configuration = null)
        {
            if (configuration == null || configuration.Count == 0) 
            {
                _configuration = NewConfiguration();
            }
            else if (configuration.Count == 1)
            {
                WithConfiguredOperations(configuration.First());
            }
            else
            {
                _configuration = configuration;
            }

            _firstChainedOperation = Configure();
            return this;
        }

        public CalculatingService WithConfiguredOperations(AcCalculationOperation firstOperationInChain)
        {
            if (firstOperationInChain == null)
            {
                throw new ArgumentNullException("Can't handle nothing");
            }
            
            _firstChainedOperation = firstOperationInChain;

            return this;
        }

        public ResultEntity CalculateValues()
        {
            _result = _firstChainedOperation.Execute();
            return new ResultEntity(_result);
        }


        private List<AcCalculationOperation> NewConfiguration()
        {
            var config = new List<AcCalculationOperation>()
            {
                new CalculateAllTime                (_records, _result),
                new CalculateFirstOperationDateTime (_records, _result),
                new CalculateAverageDuration        (_records, _result),
                new CalculateAverageValue           (_records, _result),
                new CalculateMedianOfValues         (_records, _result),
                new CalculateMaxValue               (_records, _result),
                new CalculateMinValue               (_records, _result),
                new CalculateRowCount               (_records, _result),
            };

            return config;
        }

        private AcCalculationOperation Configure()
        {
            for (int i = 0; i < _configuration.Count - 1; ++i)
            {
                _configuration[i].WithNextOperation(_configuration[i + 1]);
            }

            return _configuration.First();
        }

    }
}
