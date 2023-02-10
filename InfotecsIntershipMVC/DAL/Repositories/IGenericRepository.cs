using InfotecsIntershipMVC.DAL.DbContexts;
using InfotecsIntershipMVC.DAL.Models;

namespace InfotecsIntershipMVC.DAL.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        public Guid Create(T entity);
        public T? FindById(Guid id);
        public IEnumerable<T> GetAll();
        public IEnumerable<T> GetAllImmutable();
        public int Update(T entity);
        public void DeleteById(Guid id);
        public void DeleteAll();
        
    }
}
