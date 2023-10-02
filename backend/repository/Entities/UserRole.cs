using System.ComponentModel.DataAnnotations;

namespace repository.Entities
{
    public class UserRole : Entity
    {
        public Guid RoleId {get;set;}

        [Required]
        public Role Role {get;set;} = null!;

        public Guid UserId {get;set;}

        [Required]
        public User User {get;set;} = null!;
    }
}