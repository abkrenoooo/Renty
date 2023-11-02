using DAL.Entities;
using DAL.Enum;
using DAL.ExtensionMethods;
using DAL.Models.SuperAdmin;
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
    public class SuperAdminRepo : ISuperAdminRepo
    {
        #region Depend Injection
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public SuperAdminRepo(UserManager<ApplicationUser> UserManager, ApplicationDbContext db)
        {
            _db = db;
            _userManager = UserManager;
        }
        #endregion

        #region Create
        public async Task<Response<ApplicationUser>> CreateSuperAdminUserAsync(SuperAdminUserVM model)
        {
            try
            {
                if (await _userManager.FindByEmailAsync(model.Email) is not null)
                    return new Response<ApplicationUser>
                    {
                        Message = "Email is Already exists!",
                        Success = false,
                        status_code = "500"
                    };

                if (await _userManager.FindByNameAsync(model.Username) is not null)
                    return new Response<ApplicationUser>
                    {
                        Message = "Username is Already exists!",
                        Success = false,
                        status_code = "500"
                    };
                var user = model.ToApplicationUser().Result;

                var result = await _userManager.CreateAsync(user, model.Password);

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
                await _userManager.AddToRoleAsync(user, Roles.SuperAdmin.ToString());
                await _userManager.UpdateAsync(user);
                var studentRoles = await _userManager.GetRolesAsync(user);


                return new Response<ApplicationUser>
                {
                    Success = true,
                    ObjectData = model.ToApplicationUser().Result,
                    status_code = "200"
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message, ex.StackTrace);
                return new()
                {
                    Message = ex.Message,
                    Success = false,
                    status_code = "500"
                };
            }
        }
        #endregion

        #region Delete 

        public async Task<bool> RemoveSuperAdminUserAsync(string SuperAdminUserId)
        {
            var SuperAdmin = await _userManager.FindByIdAsync(SuperAdminUserId);
            if (SuperAdmin != null)
            {
                var result = await _userManager.DeleteAsync(SuperAdmin);
                return result.Succeeded;
            }
            return false;
        }
        #endregion

        #region Update 

        public async Task<Response<ApplicationUser>> EditSuperAdminUserAsync(SuperAdminUserVM model)
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
                        Message = "Can't Found SuperAdmin",
                        status_code = "404",
                    };
                }
                var editSuperAdmin = user.ToApplicationUserToEdit(model.ToApplicationUser().Result).Result;
                var result =await _userManager.UpdateAsync(editSuperAdmin);
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

        public async Task<Response<ApplicationUser>> GetSuperAdminUserByIdAsync(string SuperAdminUserId)
        {
            try
            {
                var SuperAdmin = await _userManager.FindByIdAsync(SuperAdminUserId);
                if (SuperAdmin == null)
                {
                    return new Response<ApplicationUser>
                    {
                        Success = true,
                        Message = "SuperAdmin Is Not Found",
                        status_code = "404"
                    };
                }
                return new Response<ApplicationUser>
                {
                    Success = true,
                    Message = "All SuperAdmins",
                    ObjectData = SuperAdmin,
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

        public async Task<Response<ApplicationUser>> GetAllSuperAdminAsync(int paggingNumber)
        {
            try
            {
                var SuperAdmins = await _userManager.GetUsersInRoleAsync(Roles.SuperAdmin.ToString());
                int AllSuperAdminCount = SuperAdmins.Count();
                var AllSuperAdmin = SuperAdmins.Skip((paggingNumber - 1) * 10).Take(10).ToList();
                return new Response<ApplicationUser>
                {
                    Success = true,
                    Message = "All SuperAdmins",
                    Data = AllSuperAdmin,
                    CountOfData = AllSuperAdminCount,
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
