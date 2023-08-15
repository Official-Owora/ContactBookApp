using ContactBookApp.Infrastructure.RepositoryBase.Abstraction;

namespace ContactBookApp.Infrastructure.RepositoryManager
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        IContactRepository ContactRepository { get; }
        Task SaveAsync();
    }
}
