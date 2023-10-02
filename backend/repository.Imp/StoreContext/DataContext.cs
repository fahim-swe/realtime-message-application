using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using repository.Entities;

namespace repository.Imp.StoreContext
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options)
        {
            
        }


        public DbSet<User> User {get;set;}
        public DbSet<Role> Role {get;set;}
        public DbSet<UserRole> UserRole {get;set;}


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserRole>()
                .HasKey(key => new {key.RoleId, key.UserId, key.Id});

            modelBuilder.Entity<UserRole>()
                .HasOne<User>( user => user.User)
                .WithMany(userRole => userRole.UserRoles)
                .HasForeignKey(user => user.UserId);
            
            modelBuilder.Entity<UserRole>()
                .HasOne<Role>(role => role.Role)
                .WithMany(userRole => userRole.UserRoles)
                .HasForeignKey(role => role.RoleId);
        }
    }
}



