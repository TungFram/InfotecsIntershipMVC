using InfotecsIntershipMVC.DAL.Models;

namespace InfotecsIntershipMVC.Services.Filtering.Filters.SortingFilters.Filename
{
    public class FilenameDescFilter : AcFilter
    {
        public override IEnumerable<ResultEntity> Apply()
        {
            _results = _results.OrderByDescending(result => result.FileName).ToList();

            return ToNextFilter();
        }
    }
}
