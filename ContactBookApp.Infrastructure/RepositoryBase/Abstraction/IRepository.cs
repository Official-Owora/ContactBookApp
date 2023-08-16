namespace ContactBookApp.Infrastructure.RepositoryBase.Abstraction
{
    public interface IRepository<T>
    {
        Task CreateAsync(T entity);
        Task CreateRangeAsync(IEnumerable<T> entities);
        void Delete(T entity);
        void Update(T entity);
    }
}
