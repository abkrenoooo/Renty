
using DAL.Entities;
using DAL.Models.SuperAdmin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ExtensionMethods
{
    public static class SuperAdminMapping
    {
        public static async Task<ApplicationUser> ToApplicationUser(this SuperAdminUserVM SuperAdmin)
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
        public static async Task<ApplicationUser> ToApplicationUserEdit(this SuperAdminUserVM SuperAdmin)
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
    }
}
