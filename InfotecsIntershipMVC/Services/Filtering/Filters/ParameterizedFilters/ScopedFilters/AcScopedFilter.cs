using System.Numerics;

namespace InfotecsIntershipMVC.Services.Filtering.Filters.ParameterizedFilters.ScopedFilters
{
    public abstract class AcScopedFilter<T> : AcFilter
    {
        protected T _startBoundary;
        protected T _endBoundary;

        public abstract AcScopedFilter<T> WithBoundaries(T startBoundary, T endBoundary);
    }
}
