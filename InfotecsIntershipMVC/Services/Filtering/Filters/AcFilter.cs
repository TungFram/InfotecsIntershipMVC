using InfotecsIntershipMVC.DAL.Models;
using System.Collections.Immutable;

namespace InfotecsIntershipMVC.Services.Filtering.Filters
{
    public abstract class AcFilter<T> where T : IEntity
    {
        protected AcFilter<T> _nextFilter;
        protected ICollection<T> _results;

        public abstract IEnumerable<T> Apply();

        public AcFilter<T> SetResults(ICollection<T> results)
        {
            if (!IsDataValid(results))
                throw new ArgumentException("Can't handle nothing");
            
            _results = results;
            return this;
        }

        public AcFilter<T> WithNextFilter(AcFilter<T> filter)
        {
            _nextFilter = filter;
            return _nextFilter;
        }

        protected bool IsDataValid(IEnumerable<T> data)
        {
            return (data != null && data.Count() != 0);
        }

        protected IEnumerable<T> ToNextFilter()
        {
            if (_nextFilter== null) { return _results; }
            return _nextFilter.SetResults(_results).Apply();
        }


    }
}
