using System.ComponentModel.DataAnnotations;

namespace api.Dto
{
    public class SignInDto
    {

        [Required(ErrorMessage = "First Name is required")]
        [StringLength(100, MinimumLength = 3)]
        public string FirstName {get;set;}

        [Required(ErrorMessage = "Last Name is required")]
        [StringLength(100, MinimumLength = 3)]
        public string LastName {get;set;}


        [Required(ErrorMessage = "Eamil Address is required")]
        [EmailAddress]
        public string Email {get;set;}

        [Required(ErrorMessage = "Password should be min 6 length")]
        [StringLength(15, MinimumLength =6)]
        public string Password {get;set;}
    }
}