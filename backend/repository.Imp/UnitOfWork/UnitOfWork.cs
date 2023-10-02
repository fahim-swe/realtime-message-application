using repository.Entities;
using repository.Imp.Repository;
using repository.Imp.StoreContext;
using repository.Interfaces;

namespace repository.Imp.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;
        public UnitOfWork(DataContext context)
        {
            _context = context;
        }
        public IRepository<User> UserRepository => new Repository<User>(_context);
        public IRepository<Role> RoleRepository => new Repository<Role>(_context);
        public IRepository<UserRole> UserRoleRepository => new Repository<UserRole>(_context);

       

        public async Task<bool> Commit()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async void Dispose()
        {
            await _context.DisposeAsync();
        }

    }
}