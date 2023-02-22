using InfotecsIntershipMVC.DAL.Models;
using InfotecsIntershipMVC.Services.Filtering.Filters;
using System.Collections.Immutable;

namespace InfotecsIntershipMVC.Services.Filtering
{
    public interface IFilteringService<T> where T : IEntity
    {
        public FilteringService<T> WithFilters(IEnumerable<AcFilter<T>> filters);
        public FilteringService<T> WithData(ImmutableList<T> data);
        public IEnumerable<T> ApplyFileters();


    }
}
