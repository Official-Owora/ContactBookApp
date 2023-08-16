using ContactBook.Shared.RequestParameter.Common;
using ContactBook.Shared.RequestParameter.ModelParameters;
using ContactBookApp.Domain.Entities;

namespace ContactBookApp.Infrastructure.RepositoryBase.Abstraction
{
    public interface IUserRepository : IRepository<User>
    {
        Task<PagedList<User>> GetAllUsersAsync(UserRequestInputParameter parameter);
        Task<User> GetUserByIdAsync(int id);
        Task<User> GetUserByEmailAsync(string email);
        Task<User> Delete(int id);
    }
}
