using InfotecsIntershipMVC.DAL.Models;
using System.ComponentModel;

namespace InfotecsIntershipMVC.Services.Filtering.Filters.ParameterizedFilters.ScopedFilters.AverageDuration
{
    public class AvgDurationScopedFilter : AcScopedFilter<int, ResultEntity>
    {
        public override IEnumerable<ResultEntity> Apply()
        {
            _results = _results
                .Where(result => result.AverageDuration >= _startBoundary
                            && result.AverageDuration <= _endBoundary)
                .ToList();

            return ToNextFilter();
        }

        public override AcScopedFilter<int, ResultEntity> WithBoundaries(
            int startBoundary = 0,
            int endBoundary = int.MaxValue)
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
