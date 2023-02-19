using InfotecsIntershipMVC.DAL.Models;

namespace InfotecsIntershipMVC.Services.Filtering.Filters.SortingFilters.AllTime
{
    public class AllTimeDescFilter : AcFilter
    {
        public override IEnumerable<ResultEntity> Apply()
        {
            _results = _results.OrderByDescending(record => record.AllTime).ToList();
            return ToNextFilter();
        }
    }
}
