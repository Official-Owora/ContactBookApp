using ContactBookApp.Domain.Entities;
using ContactBookApp.Infrastructure.Persistence;
using ContactBookApp.Infrastructure.RepositoryBase.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace ContactBookApp.Infrastructure.RepositoryBase.Implementation
{
    public class ContactRepository : Repository<Contact>, IContactRepository
    {
        private readonly DbSet<Contact> _contacts;

        public ContactRepository(DataContext dataContext) : base(dataContext)
        {
            _contacts = dataContext.Set<Contact>();
        }
        public async Task<IEnumerable<Contact>> GetAllContactAsync()
        {
            return await _contacts.ToListAsync();
        }
        public async Task<Contact> GetContactByEmailAsync(string email)
        {
            return await _contacts.Where(c => c.Email.Contains(email, StringComparison.InvariantCultureIgnoreCase))
                .FirstOrDefaultAsync();
        }
        public async Task<Contact> GetContactByIdAsync(int id)
        {
            return await _contacts.FindAsync(id);
        }
    }
}
