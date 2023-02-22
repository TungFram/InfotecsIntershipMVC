using InfotecsIntershipMVC.DAL.Models;

namespace InfotecsIntershipMVC.Services.Filtering.Filters.SortingFilters.AverageValue
{
    public class AvgValueDescFilter : AcFilter<ResultEntity>
    {
        public override IEnumerable<ResultEntity> Apply()
        {
            _results = _results.OrderByDescending(record => record.AverageValue).ToList();
            return ToNextFilter();
        }
    }
}
