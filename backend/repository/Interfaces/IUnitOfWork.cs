using repository.Entities;

namespace repository.Interfaces
{
    public interface IUnitOfWork: IDisposable
    {
         IRepository<User> UserRepository {get;}
         IRepository<Role> RoleRepository {get;}
         IRepository<UserRole> UserRoleRepository {get;}
         Task<Boolean> Commit();
         
    }
}