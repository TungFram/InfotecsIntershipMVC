using InfotecsIntershipMVC.DAL.Models;

namespace InfotecsIntershipMVC.Services.Filtering.Filters.ParameterizedFilters.StringFilters
{
    public class RecordFilenameEqualsPatternFilter : AcPatternFilter<RecordEntity>
    {
        public override IEnumerable<RecordEntity> Apply()
        {
            _results = _results
                .Where(record => record.File.Name.Equals(_pattern))
                .ToList();

            return ToNextFilter();
        }
    }
}
