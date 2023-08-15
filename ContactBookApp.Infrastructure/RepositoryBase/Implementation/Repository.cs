using ContactBookApp.Infrastructure.Persistence;
using ContactBookApp.Infrastructure.RepositoryBase.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace ContactBookApp.Infrastructure.RepositoryBase.Implementation
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbSet<T> _dataContext;
        public Repository(DataContext dataContext)
        {
            _dataContext = dataContext.Set<T>();
        }
        public async Task CreateAsync(T entity) => await _dataContext.AddAsync(entity);
        public async Task CreateRangeAsync(IEnumerable<T> entities) => await _dataContext.AddRangeAsync(entities);
        public void Delete(T entity) => _dataContext.Remove(entity);
        public void DeleteRange(IEnumerable<T> entities) => _dataContext.RemoveRange(entities);
        public void Update(T entity) => _dataContext.Update(entity);
    }
}
