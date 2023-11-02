using DAL.Entities;
using DAL.Enum;
using DAL.ExtensionMethods;
using DAL.Models.User;
using DAL.Models.HouseViewModel;
using DAL.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Renty.DAL.Data;
using Renty.Models;
using Renty.Models.AuthModel;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository.Repository
{
    public class UserRepo : IUserRepo
    {
        #region Depend Injection
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserRepo(UserManager<ApplicationUser> UserManager, ApplicationDbContext db)
        {
            _db = db;
            _userManager = UserManager;
        }
        #endregion

        #region Delete 

        public async Task<bool> RemoveUserUserAsync(string UserUserId)
        {
            var User = await _userManager.FindByIdAsync(UserUserId);
            if (User != null)
            {
                var result = await _userManager.DeleteAsync(User);
                return result.Succeeded;
            }
            return false;
        }
        #endregion

        #region Update 

        public async Task<Response<ApplicationUser>> EditUserUserAsync(UserVM model)
        {
            try
            {
                if (await _userManager.FindByEmailAsync(model.Email) is not null)
                {
                    return new Response<ApplicationUser>
                    {
                        Message = "Email is Already exists!",
                        Success = false,
                        status_code = "500"
                    };
                }
                if (await _userManager.FindByNameAsync(model.Username) is not null)
                {
                    return new Response<ApplicationUser>
                    {
                        Message = "Username is Already exists!",
                        Success = false,
                        status_code = "500"
                    };
                }
                var user = await _userManager.FindByIdAsync(model.UserId);
                if (user == null)
                {
                    return new Response<ApplicationUser>
                    {
                        Success = false,
                        Message = "Can't Found User",
                        status_code = "404",
                    };
                }
                var editUser = user.ToApplicationUserToEdit(model.ToApplicationUserEditToUser().Result).Result;
                var result =await _userManager.UpdateAsync(editUser);
                if (!result.Succeeded)
                {
                    var errors = string.Empty;

                    foreach (var error in result.Errors)
                        errors += $"{error.Description},";

                    return new Response<ApplicationUser>
                    {
                        Message = errors,
                        Success = false,
                        status_code = "500"
                    };
                }
                return new Response<ApplicationUser>
                {
                    Success = true,
                    ObjectData = model.ToApplicationUser().Result,
                    status_code = "200"
                };
            }

            catch (Exception e)
            {
                return new Response<ApplicationUser>
                {
                    Success = false,
                    error = e.Message,
                    status_code = "500"
                };
            }

        }
        #endregion 

        #region Get 

        public async Task<Response<ApplicationUser>> GetUserUserByIdAsync(string UserUserId)
        {
            try
            {
                var User = await _userManager.FindByIdAsync(UserUserId);
                if (User == null)
                {
                    return new Response<ApplicationUser>
                    {
                        Success = true,
                        Message = "User Is Not Found",
                        status_code = "404"
                    };
                }
                return new Response<ApplicationUser>
                {
                    Success = true,
                    Message = "All Users",
                    ObjectData = User,
                    status_code = "200"
                };

            }
            catch (Exception e)
            {
                return new Response<ApplicationUser>
                {
                    Success = false,
                    error = e.Message,
                    status_code = "500"
                };
            }
        }
        #endregion

        #region Get All 

        public async Task<Response<ApplicationUser>> GetAllUserAsync(int paggingNumber)
        {
            try
            {
                var Users = await _userManager.GetUsersInRoleAsync(Roles.User.ToString());
                int AllUserCount = Users.Count();
                var AllUser = Users.Skip((paggingNumber - 1) * 10).Take(10).ToList();
                return new Response<ApplicationUser>
                {
                    Success = true,
                    Message = "All Users",
                    Data = AllUser,
                    CountOfData = AllUserCount,
                    paggingNumber = paggingNumber,
                    status_code = "200"
                };

            }
            catch (Exception e)
            {
                return new Response<ApplicationUser>
                {
                    Success = false,
                    error = e.Message,
                    status_code = "500"
                };
            }
        }
        #endregion
    }
}
