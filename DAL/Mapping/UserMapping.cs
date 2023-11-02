
using DAL.Entities;
using DAL.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ExtensionMethods
{
    public static class UserMapping
    {
        public static async Task<ApplicationUser> ToApplicationUser(this UserVM User)
        {
            return new()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = User.Username,
                Email = User.Email,
                PhoneNumber = User.Phone,
                IsActive = true,
                BirithDate = User.BirithDate,
                Gender = User.Gender,
            };
        }
        public static async Task<ApplicationUser> ToApplicationUserEditToUser(this UserVM User)
        {
            return new()
            {
                Id = User.UserId,
                UserName = User.Username,
                Email = User.Email,
                PhoneNumber = User.Phone,
                IsActive = true,
                BirithDate = User.BirithDate,
                Gender = User.Gender,
            };
        }

    }
}
