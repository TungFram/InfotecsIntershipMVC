namespace InfotecsIntershipMVC.DAL.Repositories
{
    public interface IGenericAsyncRepository<T> where T : class
    {
        public Task<Guid> CreateAsync(T entity);
        public Task<T?> FindByIdAsync(Guid id);
        public Task<IEnumerable<T>> GetAllAsync();
        public Task<int> UpdateAsync(T entity);
        public void DeleteByIdAsync(Guid id);
        public void DeleteAllAsync();
    }
}
