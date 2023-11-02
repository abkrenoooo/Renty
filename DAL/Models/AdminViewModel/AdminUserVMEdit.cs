using DAL.Enum;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models.Admin
{
    public class AdminUserVMEdit
    {
        public string UserId { get; set; }
        [Required]
        public string? FullName { get; set; }
        public IFormFile? ProfileImage { get; set; }
        public IFormFile? CoverImage { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime? BirithDate { get; set; }
        [Required]
        public Gender? Gender { get; set; }
        public bool Active { get; set; } = true;
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        [Required]
        [Compare(nameof(Password), ErrorMessage = "Password don't match.")]
        [DataType(DataType.Password)]
        public string? ConfirmPassword { get; set; }
        [Required]
        public string? Phone { get; set; }
        [Required]
        public string? Username { get; set; }
    }
}
