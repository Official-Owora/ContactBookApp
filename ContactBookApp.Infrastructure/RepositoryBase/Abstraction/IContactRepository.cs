using ContactBook.Shared.RequestParameter.Common;
using ContactBook.Shared.RequestParameter.ModelParameters;
using ContactBookApp.Domain.Entities;
using System.Threading.Tasks;

namespace ContactBookApp.Infrastructure.RepositoryBase.Abstraction
{
    public interface IContactRepository : IRepository<Contact>
    {
        Task<PagedList<Contact>> GetAllContactAsync(ContactRequestInputParameter parameter);
        Task<Contact> GetContactByEmailAsync(string email);
        Task<Contact> GetContactByIdAsync(int id);
    }
}
