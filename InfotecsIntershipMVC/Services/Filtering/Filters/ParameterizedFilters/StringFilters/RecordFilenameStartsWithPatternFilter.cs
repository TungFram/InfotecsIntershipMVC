using InfotecsIntershipMVC.DAL.Models;

namespace InfotecsIntershipMVC.Services.Filtering.Filters.ParameterizedFilters.StringFilters
{
    public class RecordFilenameStartsWithPatternFilter : AcPatternFilter<RecordEntity>
    {
        public override IEnumerable<RecordEntity> Apply()
        {
            _results = _results
                .Where(record => record.File.Name.StartsWith(_pattern))
                .ToList();

            return ToNextFilter();
        }
    }
}
