
using DAL.Entities;
using DAL.Models.SuperAdmin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll.ExtensionMethods
{
    public static class SuperAdminMapping
    {
        public static async Task<ApplicationUser> ToApplicationUserToSuperAdmin(this SuperAdminUserVM SuperAdmin)
        {
            return new()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = SuperAdmin.Username,
                Email = SuperAdmin.Email,
                PhoneNumber = SuperAdmin.Phone,
                IsActive = true,
                BirithDate = SuperAdmin.BirithDate,
                Gender = SuperAdmin.Gender,
            };
        } 
        public static async Task<ApplicationUser> ToApplicationUserEditToSuperAdmin(this SuperAdminUserVM SuperAdmin)
        {
            return new()
            {
                Id = SuperAdmin.UserId,
                UserName = SuperAdmin.Username,
                Email = SuperAdmin.Email,
                PhoneNumber = SuperAdmin.Phone,
                IsActive = true,
                BirithDate = SuperAdmin.BirithDate,
                Gender = SuperAdmin.Gender,
            };
        }
        public static async Task<SuperAdminUserVM> FromApplicationUserToSuperAdmin(this ApplicationUser user)
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
        public static async Task<SuperAdminUserVM> ToApplicationUserVMToSuperAdmin(this SuperAdminUserCreateVM user)
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
        public static async Task<SuperAdminUserVM> ToApplicationUserVMEditToSuperAdmin(this SuperAdminUserVMEdit user)
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
