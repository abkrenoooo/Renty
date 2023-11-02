
using DAL.Entities;
using DAL.Models.User;
using DAL.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll.ExtensionMethods
{
    public static class UserMapping
    {
        public static async Task<ApplicationUser> ToApplicationUserToUser(this UserVM User)
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
        public static async Task<UserVM> FromApplicationUserToUser(this ApplicationUser user)
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
        public static async Task<UserVM> ToApplicationUserVMToUser(this UserCreateVM user)
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
        public static async Task<UserVM> ToApplicationUserVMEditToUser(this UserVMEdit user)
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
