using DAL.Entities;
using DAL.Enum;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Renty.Models.AuthModel
{
    public class RegisterModel
    {
        [StringLength(50)]
        [Required]
        public string? FullName { get; set; }
        [Required]
        [StringLength(50)]
        public string? Username { get; set; }
        [Required]
        [StringLength(128)]
        public string? Email { get; set; }
        [Required]
        [StringLength(20)]
        public string? Password { get; set; }
        [Required]
        [MaxLength(11, ErrorMessage = "Phone number must be less than 11 number")]
        [MinLength(11, ErrorMessage = "Phone number must be more than 11 number")]
        [DataType(DataType.PhoneNumber)]
        public string? PhoneNumber { get; set; }
        [Required]
        [MaxLength(14, ErrorMessage = "National Id must be less than 14 number")]
        [MinLength(14, ErrorMessage = "National Id must be more than 14 number")]
        public string? NationalId { get; set; }
        [Required]

        public DateTime? BirithDate { get; set; }
        [Required]
        public string? Nationality { get; set; }

        [Required]
        public Gender? Gender { get; set; }
 
    }
}
