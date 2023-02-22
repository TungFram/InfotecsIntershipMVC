using InfotecsIntershipMVC.DAL.Models;

namespace InfotecsIntershipMVC.Services.Filtering.Filters.SortingFilters.Filename
{
    public class RecordFilenameDescFilter : AcFilter<RecordEntity>
    {
        public override IEnumerable<RecordEntity> Apply()
        {
            _results = _results
                .OrderByDescending(record => record.File.Name)
                .ToList();
            return ToNextFilter();
        }
    }
}
