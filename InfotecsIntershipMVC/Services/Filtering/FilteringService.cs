using InfotecsIntershipMVC.DAL.Models;
using InfotecsIntershipMVC.Services.Filtering.Filters;
using System.Collections.Immutable;
using System.Collections.ObjectModel;

namespace InfotecsIntershipMVC.Services.Filtering
{
    public class FilteringService : IFilteringService
    {
        private ImmutableList<ResultEntity> _results;
        private IEnumerable<AcFilter> _filters;
        private AcFilter _firstChainedFilter;

        public FilteringService WithFilters(IEnumerable<AcFilter> filters)
        {
            if (filters == null || filters.Count() == 0)
                throw new ArgumentNullException($"Filtering service must contain any filters");

            _filters = filters;
            Configure();
            return this;
        }

        public FilteringService WithData(ImmutableList<ResultEntity> data)
        {
            if (data == null || data.Count() == 0)
                throw new ArgumentNullException($"Can't filter nothing");

            _results = data;
            return this;
        }

        public IEnumerable<ResultEntity> ApplyFileters()
        {
            IEnumerable<ResultEntity> filteredResuls = _firstChainedFilter
                .SetResults(new List<ResultEntity>(_results))
                .Apply();
            return filteredResuls;
        }

        private void Configure()
        {
            if (_filters.Count() != 1)
            {
                for (int i = 0; i < _filters.Count() - 1; ++i)
                {
                    _filters.ElementAt(i).WithNextFilter(_filters.ElementAt(i + 1));
                }
            }
                
            _firstChainedFilter = _filters.First();
        }
    }
}
