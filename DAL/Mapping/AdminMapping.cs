
using DAL.Entities;
using DAL.Models.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ExtensionMethods
{
    public static class AdminMapping
    {
        public static async Task<ApplicationUser> ToApplicationUserToEdit(this ApplicationUser user, ApplicationUser userEdit)
        {
            userEdit.FullName = user.FullName;
            userEdit.Gender = user.Gender;
            userEdit.PhoneNumber = user.PhoneNumber;
            userEdit.BirithDate = user.BirithDate;
            userEdit.UserName = user.UserName;
            return userEdit;
        }
        public static async Task<ApplicationUser> ToApplicationUser(this AdminUserVM admin)
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
        public static async Task<ApplicationUser> ToApplicationUserEdit(this AdminUserVM admin)
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

    }
}
