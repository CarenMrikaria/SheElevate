using System.ComponentModel.DataAnnotations;

namespace SheElevate.Models
{
    public class Mentors
    {
        [Key] 
        public int MentorsID { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? ProfilePicture { get; set; }
        public int YearsOfExperience { get; set; }
        public required string CompanyName { get; set; }
        public double Rating { get; set; } 
        public DateTime DateJoined { get; set; }

    }
}
