using System.ComponentModel.DataAnnotations;

namespace repository.Entities
{
    public class Role : Entity
    {
        [Required]
        public string Name {get;set;} = null!;

        [Required]
        public ICollection<UserRole> UserRoles {get;set;} = null!;
    }
}