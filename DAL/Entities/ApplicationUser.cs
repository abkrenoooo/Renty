using DAL.Enum;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [Required, MaxLength(50)]
        public string? FullName { get; set; }
        public string? ProfileImage { get; set; }
        public string? CoverImage { get; set; }
        public string? Bio { get; set; }
        public string? NationalId { get; set; }
        public string? Nationality { get; set; }
        public DateTime? BirithDate { get; set; }
        public bool IsActive { get; set; }
        public Gender? Gender { get; set; }
        [ForeignKey(nameof(AddressId))]
        public int? AddressId { get; set; }
        public virtual Address? address { get; set; }
        //public List<Notifications> ReciverNotifications { get; set; }
    }
}
