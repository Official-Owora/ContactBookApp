using ContactBookApp.Domain.Entities;
using System.Threading.Tasks;

namespace ContactBookApp.Infrastructure.RepositoryBase.Abstraction
{
    public interface IContactRepository : IRepository<Contact>
    {
        Task<IEnumerable<Contact>> GetAllContactAsync();
        Task<Contact> GetContactByEmailAsync(string email);
        Task<Contact> GetContactByIdAsync(int id);
    }
}
