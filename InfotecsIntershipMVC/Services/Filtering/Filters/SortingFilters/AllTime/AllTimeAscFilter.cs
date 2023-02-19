using InfotecsIntershipMVC.DAL.Models;

namespace InfotecsIntershipMVC.Services.Filtering.Filters.SortingFilters.AllTime
{
    public class AllTimeAscFilter : AcFilter
    {
        public override IEnumerable<ResultEntity> Apply()
        {
            _results = _results.OrderBy(record => record.AllTime).ToList();
            return ToNextFilter();
        }
    }
}
