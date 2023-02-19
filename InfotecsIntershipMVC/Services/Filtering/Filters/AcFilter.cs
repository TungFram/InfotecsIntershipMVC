using InfotecsIntershipMVC.DAL.Models;
using System.Collections.Immutable;

namespace InfotecsIntershipMVC.Services.Filtering.Filters
{
    public abstract class AcFilter
    {
        protected AcFilter _nextFilter;
        protected ICollection<ResultEntity> _results;

        public abstract IEnumerable<ResultEntity> Apply();

        public AcFilter SetResults(ICollection<ResultEntity> results)
        {
            if (!IsDataValid(results))
                throw new ArgumentException("Can't handle nothing");
            
            _results = results;
            return this;
        }

        public AcFilter WithNextFilter(AcFilter filter)
        {
            _nextFilter = filter;
            return _nextFilter;
        }

        protected bool IsDataValid(IEnumerable<ResultEntity> data)
        {
            return (data != null && data.Count() != 0);
        }

        protected IEnumerable<ResultEntity> ToNextFilter()
        {
            if (_nextFilter== null) { return _results; }
            return _nextFilter.SetResults(_results).Apply();
        }


    }
}
