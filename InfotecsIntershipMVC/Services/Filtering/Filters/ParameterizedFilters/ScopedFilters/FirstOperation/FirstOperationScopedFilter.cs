using InfotecsIntershipMVC.DAL.Models;

namespace InfotecsIntershipMVC.Services.Filtering.Filters.ParameterizedFilters.ScopedFilters.FirstOperation
{
    public class FirstOperationScopedFilter : AcScopedFilter<DateTime>
    {
        public override IEnumerable<ResultEntity> Apply()
        {
            _results = _results
                .Where(result => result.FirstOperation >= _startBoundary
                                && result.FirstOperation <= _endBoundary)
                .ToList();

            return ToNextFilter();
        }

        public override AcScopedFilter<DateTime> WithBoundaries(
            DateTime startBoundary, 
            DateTime endBoundary)
        {
            if (startBoundary > endBoundary)
            {
                throw new ArgumentException($"Invalid boundaries of filter {this.GetType().Name}");
            }

            _startBoundary = startBoundary;
            _endBoundary = endBoundary;
            return this;
        }
    }
}
