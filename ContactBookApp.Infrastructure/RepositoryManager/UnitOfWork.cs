using ContactBookApp.Infrastructure.Persistence;
using ContactBookApp.Infrastructure.RepositoryBase.Abstraction;
using ContactBookApp.Infrastructure.RepositoryBase.Implementation;

namespace ContactBookApp.Infrastructure.RepositoryManager
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _dataContext;
        private IUserRepository _userRepository;
        private IContactRepository _contactRepository;

        public UnitOfWork(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public IUserRepository UserRepository
        {
            get
            {
                if (_userRepository == null)
                {
                    _userRepository = new UserRepository(_dataContext);
                }
                return _userRepository;
            }
        }
        public IContactRepository ContactRepository
        {
            get
            {
                if( _contactRepository == null)
                {
                    _contactRepository = new ContactRepository(_dataContext);
                }
                return _contactRepository;
            }
        }

        public async Task SaveAsync()
        {
            await _dataContext.SaveChangesAsync();
        }
    }
}
