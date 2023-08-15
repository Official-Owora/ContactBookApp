using ContactBookApp.Domain.Entities;
using ContactBookApp.Infrastructure.Persistence;
using ContactBookApp.Infrastructure.RepositoryBase.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace ContactBookApp.Infrastructure.RepositoryBase.Implementation
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly DbSet<User> users;

        public UserRepository(DataContext dataContext) : base(dataContext)
        {
            users = dataContext.Set<User>();
        }
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await users.ToListAsync();
        }
        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await users.Where(u => u.Email.Contains(email, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefaultAsync();
        }
        public async Task<User> GetUserByIdAsync(int id)
        {
            return await users.FindAsync(id);
        }
    }
}
