using InfotecsIntershipMVC.DAL.Models;
using InfotecsIntershipMVC.Services.Filtering.Filters;
using System.Collections.Immutable;

namespace InfotecsIntershipMVC.Services.Filtering
{
    public interface IFilteringService
    {
        public FilteringService WithFilters(IEnumerable<AcFilter> filters);
        public FilteringService WithData(ImmutableList<ResultEntity> data);
        public IEnumerable<ResultEntity> ApplyFileters();


    }
}
