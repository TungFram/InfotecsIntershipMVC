using InfotecsIntershipMVC.DAL.Models;

namespace InfotecsIntershipMVC.Services.Filtering.Filters.ParameterizedFilters.StringFilters
{
    public abstract class AcPatternFilter<T> : AcFilter<T> where T : IEntity
    {
        protected string _pattern;

        public AcPatternFilter<T> WithPattern(string pattern)
        {
            if (string.IsNullOrEmpty(pattern))
                throw new ArgumentNullException(nameof(pattern));
            _pattern = pattern;

            return this;
        }
    }
}
