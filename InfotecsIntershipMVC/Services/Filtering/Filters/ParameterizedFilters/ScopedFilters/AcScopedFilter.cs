using InfotecsIntershipMVC.DAL.Models;
using System.Numerics;

namespace InfotecsIntershipMVC.Services.Filtering.Filters.ParameterizedFilters.ScopedFilters
{
    public abstract class AcScopedFilter<TBoundary, TEntity> : AcFilter<TEntity> where TEntity : IEntity
    {
        protected TBoundary _startBoundary;
        protected TBoundary _endBoundary;

        public abstract AcScopedFilter<TBoundary, TEntity>
            WithBoundaries(TBoundary startBoundary, TBoundary endBoundary);
    }
}
