using InfotecsIntershipMVC.DAL.Models;

namespace InfotecsIntershipMVC.Services.Filtering.Filters.SortingFilters.AverageDuration
{
    public class AvgDurationAscFilter : AcFilter<ResultEntity>
    {
        public override IEnumerable<ResultEntity> Apply()
        {
            _results = _results.OrderBy(record => record.AverageDuration).ToList();
            return ToNextFilter();
        }
    }
}
