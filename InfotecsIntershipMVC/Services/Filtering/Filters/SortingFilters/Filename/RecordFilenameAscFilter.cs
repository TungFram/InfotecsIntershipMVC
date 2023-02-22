using InfotecsIntershipMVC.DAL.Models;

namespace InfotecsIntershipMVC.Services.Filtering.Filters.SortingFilters.Filename
{
    public class RecordFilenameAscFilter : AcFilter<RecordEntity>
    {
        public override IEnumerable<RecordEntity> Apply()
        {
            _results = _results
                .OrderBy(record => record.File.Name)
                .ToList();
            return ToNextFilter();
        }
    }
}
