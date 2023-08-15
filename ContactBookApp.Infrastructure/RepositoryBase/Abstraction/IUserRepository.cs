using ContactBookApp.Domain.Entities;

namespace ContactBookApp.Infrastructure.RepositoryBase.Abstraction
{
    public interface IUserRepository : IRepository<User>
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserByEmailAsync(string email);
        Task<User> GetUserByIdAsync(int id);
    }
}
