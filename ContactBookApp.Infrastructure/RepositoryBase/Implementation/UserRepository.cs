using ContactBook.Shared.RequestParameter.Common;
using ContactBook.Shared.RequestParameter.ModelParameters;
using ContactBookApp.Domain.Entities;
using ContactBookApp.Infrastructure.Persistence;
using ContactBookApp.Infrastructure.RepositoryBase.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace ContactBookApp.Infrastructure.RepositoryBase.Implementation
{
    internal sealed class UserRepository : Repository<User>, IUserRepository
    {
        private readonly DbSet<User> users;

        public UserRepository(DataContext dataContext) : base(dataContext)
        {
            users = dataContext.Set<User>();
        }
        public async Task<PagedList<User>> GetAllUsersAsync(UserRequestInputParameter parameter)
        {
            var result = await users.Where(u=>u.FirstName.ToLower().Contains(parameter.SearchTerm.ToLower())
            || u.LastName.ToLower().Contains(parameter.SearchTerm.ToLower())
            || u.Email.ToLower().Contains(parameter.SearchTerm.ToLower()))
                .Skip((parameter.PageNumber-1)*parameter.PageSize)
                .Take(parameter.PageSize).ToListAsync();
            var count = await users.CountAsync();
            return new PagedList<User>(result, count, parameter.PageNumber, parameter.PageSize);
        }
        public async Task<User> GetUserByIdAsync(int id)
        {
            return await users.Where(u => u.Id.Equals(id)).FirstOrDefaultAsync();
        }
       
        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await users.Where(u => u.Email.Contains(email, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefaultAsync();
        }
        public async Task<User> Delete(int id)
        {
            return await users.FindAsync(id);
        }
    }
}
