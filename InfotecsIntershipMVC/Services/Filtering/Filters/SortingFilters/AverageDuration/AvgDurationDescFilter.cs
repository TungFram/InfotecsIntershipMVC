using InfotecsIntershipMVC.DAL.Models;
using Microsoft.AspNetCore.Http;

namespace InfotecsIntershipMVC.Services.Filtering.Filters.SortingFilters.AverageDuration
{
    public class AvgDurationDescFilter : AcFilter
    {
        public override IEnumerable<ResultEntity> Apply()
        {
            _results = _results.OrderByDescending(record => record.AverageDuration).ToList();
            return ToNextFilter();
        }
    }
}
