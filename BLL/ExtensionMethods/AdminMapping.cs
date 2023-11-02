
using DAL.Entities;
using DAL.Models.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll.ExtensionMethods
{
    public static class AdminMapping
    {
        public static async Task<ApplicationUser> ToApplicationUserToAdmin(this AdminUserVM admin)
        {
            return new()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = admin.Username,
                Email = admin.Email,
                PhoneNumber = admin.Phone,
                IsActive = true,
                BirithDate = admin.BirithDate,
                Gender = admin.Gender,
            };
        } 
        public static async Task<ApplicationUser> ToApplicationUserEditToAdmin(this AdminUserVM admin)
        {
            return new()
            {
                Id = admin.UserId,
                UserName = admin.Username,
                Email = admin.Email,
                PhoneNumber = admin.Phone,
                IsActive = true,
                BirithDate = admin.BirithDate,
                Gender = admin.Gender,
            };
        }
        public static async Task<AdminUserVM> FromApplicationUserToAdmin(this ApplicationUser user)
        {
            return new()
            {
                UserId = user.Id,
                Username = user.UserName,
                Email = user.Email,
                FullName = user.FullName,
                Phone = user.PhoneNumber,
                Active = true,
                BirithDate = user.BirithDate,
                Gender = user.Gender,
            };
        }
        public static async Task<AdminUserVM> ToApplicationUserVMToAdmin(this AdminUserCreateVM user)
        {
            return new()
            {
                Username = user.Username,
                Email = user.Email,
                FullName = user.FullName,
                Phone = user.Phone,
                Active = true,
                BirithDate = user.BirithDate,
                Gender = user.Gender,
            };
        }
        public static async Task<AdminUserVM> ToApplicationUserVMFromVMEditToAdmin(this AdminUserVMEdit user)
        {
            return new()
            {
                Username = user.Username,
                Email = user.Email,
                FullName = user.FullName,
                Phone = user.Phone,
                Active = true,
                BirithDate = user.BirithDate,
                Gender = user.Gender,
            };
        }
    }
}
