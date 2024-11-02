using System.ComponentModel.DataAnnotations;

namespace SheElevate.Models
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public required string Password { get; set; }

        [Required]
        public required string FirstName { get; set; }

        [Required]
        public required string LastName { get; set; }

        public required string PhoneNumber { get; set; }

        public DateTime DateOfBirth { get; set; }
    }
}
