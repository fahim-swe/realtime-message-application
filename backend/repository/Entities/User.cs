
using System.ComponentModel.DataAnnotations;

namespace repository.Entities
{
    public class User : Entity
    {

        [Required]
        public string FirstName {get;set;} = null!;

        [Required]
        public string LastName {get;set;} = null!;
        [Required]
        public string Email {get;set;} = null!;

        [Required]
        public ICollection<UserRole> UserRoles {get;set;} = null!;

        [Required]
        public Byte[] PasswordHash {get;set;} = null!;

        [Required]
        public Byte[] PasswordSalt {get;set;} = null!;

    }
}