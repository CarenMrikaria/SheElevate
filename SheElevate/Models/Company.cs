using System;
using System.ComponentModel.DataAnnotations;

namespace SheElevate.Models
{
    public class Company
    {
        [Key]
        public int CompanyID { get; set; }

        public required string CompanyName { get; set; }

        public required string IndustryType { get; set; }

        public DateTime EstablishedDate { get; set; }

        public required string Review { get; set; }
    }
}
