using System.ComponentModel.DataAnnotations;

namespace Talabat.APIs.DTOs
{
    public class LoginDto
    {
        [Required]
        [EmailAddress(ErrorMessage = "Email Format: example@mail.com")]
        public string Email { get; set; }

        [Required(ErrorMessage = "You Must Passwword")]
        public string Password { get; set; }
    }
}
