using ContactBook.Shared.RequestParameter.Common;
using ContactBook.Shared.RequestParameter.ModelParameters;
using ContactBookApp.Domain.Entities;
using ContactBookApp.Infrastructure.Persistence;
using ContactBookApp.Infrastructure.RepositoryBase.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace ContactBookApp.Infrastructure.RepositoryBase.Implementation
{
    internal sealed class ContactRepository : Repository<Contact>, IContactRepository
    {
        private readonly DbSet<Contact> _contacts;

        public ContactRepository(DataContext dataContext) : base(dataContext)
        {
            _contacts = dataContext.Set<Contact>();
        }
        public async Task<PagedList<Contact>> GetAllContactAsync(ContactRequestInputParameter parameter)
        {
            var result = await _contacts.Where(u => u.FirstName.ToLower().Contains(parameter.SearchTerm.ToLower())
            || u.LastName.ToLower().Contains(parameter.SearchTerm.ToLower())
            || u.Email.ToLower().Contains(parameter.SearchTerm.ToLower()))
                .Skip((parameter.PageNumber-1)*parameter.PageSize)
                .Take(parameter.PageSize).ToListAsync();
            var count = await _contacts.CountAsync();
            return new PagedList<Contact>(result, count,parameter.PageNumber, parameter.PageSize);
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
