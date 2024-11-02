using System;
using System.ComponentModel.DataAnnotations;

namespace SheElevate.Models
{
    public class Mentee
    {
        [Key]
        public int MenteeID { get; set; }

       
        public required string FirstName { get; set; }

        
        public required string LastName { get; set; }

        
        public required string Email { get; set; }

        public required string PhoneNumber { get; set; }


        public DateTime DateOfBirth { get; set; }
       
    }
}
