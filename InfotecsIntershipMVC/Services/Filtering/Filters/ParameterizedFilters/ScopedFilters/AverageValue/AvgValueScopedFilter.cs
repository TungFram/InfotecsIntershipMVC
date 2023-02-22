using InfotecsIntershipMVC.DAL.Models;

namespace InfotecsIntershipMVC.Services.Filtering.Filters.ParameterizedFilters.ScopedFilters.AverageValue
{
    public class AvgValueScopedFilter : AcScopedFilter<float, ResultEntity>
    {
        public override IEnumerable<ResultEntity> Apply()
        {
            _results = _results
                .Where(result => result.AverageValue >= _startBoundary
                                && result.AverageValue <= _endBoundary)
                .ToList();

            return ToNextFilter();
        }

        public override AcScopedFilter<float, ResultEntity> WithBoundaries(
            float startBoundary = 0, 
            float endBoundary = float.MaxValue)
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
