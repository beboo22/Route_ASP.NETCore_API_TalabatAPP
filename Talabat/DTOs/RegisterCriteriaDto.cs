using System.ComponentModel.DataAnnotations;
using Talabat.Core.Entity.Identity;

namespace Talabat.api.DTOs
{
    public class RegisterCriteriaDto
    {
        [Required]
        public string DisplayName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage ="Invalid Password")]
        public string Password { get; set; }
        [Required]
        public string Phonenumber  { get; set; }
        public AddressCriteriaDto Address { get; set; }

    }
}
