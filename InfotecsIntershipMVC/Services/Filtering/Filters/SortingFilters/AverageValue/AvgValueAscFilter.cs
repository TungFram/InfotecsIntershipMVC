using InfotecsIntershipMVC.DAL.Models;

namespace InfotecsIntershipMVC.Services.Filtering.Filters.SortingFilters.AverageValue
{
    public class AvgValueAscFilter : AcFilter
    {
        public override IEnumerable<ResultEntity> Apply()
        {
            _results = _results.OrderBy(record => record.AverageValue).ToList();
            return ToNextFilter();
        }
    }
}
