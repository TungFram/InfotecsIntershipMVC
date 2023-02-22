using InfotecsIntershipMVC.DAL.Models;
using InfotecsIntershipMVC.Services.Filtering.Filters;
using System.Collections.Immutable;
using System.Collections.ObjectModel;

namespace InfotecsIntershipMVC.Services.Filtering
{
    public class FilteringService<T> : IFilteringService<T> where T : IEntity
    {
        private ImmutableList<T> _results;
        private IEnumerable<AcFilter<T>> _filters;
        private AcFilter<T> _firstChainedFilter;

        public FilteringService<T> WithFilters(IEnumerable<AcFilter<T>> filters)
        {
            if (filters == null || filters.Count() == 0)
                throw new ArgumentNullException($"Filtering service must contain any filters");

            _filters = filters;
            Configure();
            return this;
        }

        public FilteringService<T> WithData(ImmutableList<T> data)
        {
            if (data == null || data.Count() == 0)
                throw new ArgumentNullException($"Can't filter nothing");

            _results = data;
            return this;
        }

        public IEnumerable<T> ApplyFileters()
        {
            IEnumerable<T> filteredResuls = _firstChainedFilter
                .SetResults(new List<T>(_results))
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
