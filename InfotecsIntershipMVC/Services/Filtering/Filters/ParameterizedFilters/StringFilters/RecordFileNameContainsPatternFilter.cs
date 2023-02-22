using InfotecsIntershipMVC.DAL.Models;

namespace InfotecsIntershipMVC.Services.Filtering.Filters.ParameterizedFilters.StringFilters
{
    public class RecordFileNameContainsPatternFilter : AcPatternFilter<RecordEntity>
    {
        public override IEnumerable<RecordEntity> Apply()
        {
            _results = _results
                .Where(record => record.File.Name.Contains(_pattern) == true)
                .ToList();

            return ToNextFilter();
        }
    }
}
