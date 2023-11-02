using BLL.Services.IServices;
using DAL.Enum;
using DAL.Models.Admin;
using DAL.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Renty.DAL.Data;
using Renty.BLL.Helper;
using Renty.Models;
using Renty.Models.AuthModel;
using Bll.ExtensionMethods;
using DAL.ExtensionMethods;
using DAL.Models.HouseViewModel;
using BlL.Helper;
using Microsoft.AspNetCore.Http;

namespace BLL.Services.Services
{
    public class AdminSevices : IAdminSevices
    {
        #region Depend Injection

        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JWT _jwt;
        private readonly IAdminRepo _adminRepo;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AdminSevices(UserManager<ApplicationUser> UserManager, ApplicationDbContext db, IOptions<JWT> jwt, IAdminRepo adminRepo, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _jwt = jwt.Value;
            _userManager = UserManager;
            _adminRepo = adminRepo;
            _httpContextAccessor = httpContextAccessor;

        }
        #endregion

        #region Create 

        public async Task<Response<AdminUserVM>> CreateAdminUserAsync(AdminUserCreateVM model)
        {
            try
            {
                var result = await _adminRepo.CreateAdminUserAsync(model.ToApplicationUserVMToAdmin().Result);
                if (!result.Success|| result.ObjectData==null)
                {
                    return new Response<AdminUserVM>
                    {
                        Success = result.Success,
                        Message = result.Message,
                        status_code = result.status_code,
                    };
                }
                return new Response<AdminUserVM>
                {
                    Success = true,
                    ObjectData =result.ObjectData.FromApplicationUserToAdmin().Result ,
                    status_code = "200",
                };
            }
            catch (Exception ex)
            {
                return new Response<AdminUserVM>
                {
                    Success = false,
                    Message = ex.Message,
                    status_code = "400",
                };
            }

        }
        #endregion

        #region Delete 

        public async Task<Response<AdminUserVM>> RemoveAdminUserAsync(string AdminUserId)
        {
            try
            {
                if (!await _adminRepo.RemoveAdminUserAsync(AdminUserId))
                {
                    return new Response<AdminUserVM>
                    {
                        Success = false,
                        Message = "error!",
                        status_code = "500",
                    };
                }
                return new Response<AdminUserVM>
                {
                    Success = true,
                    Message = "admin has removed",
                    status_code = "200",
                };
            }
            catch (Exception ex)
            {
                return new Response<AdminUserVM>
                {
                    Success = false,
                    Message = ex.Message,
                    status_code = "500",
                };
            }
        }
        #endregion

        #region Get 

        public async Task<Response<AdminUserVM>> GetAdminUserByIdAsync(string AdminUserId)
        {
            try
            {
                var data = await _adminRepo.GetAdminUserByIdAsync(AdminUserId);
                if (data.ObjectData == null)
                {
                    return new Response<AdminUserVM>
                    {
                        Success = false,
                        Message = "Admin Is Not Found",
                        status_code = "404",
                    };
                }
                return new Response<AdminUserVM>
                {
                    Success = true,
                    ObjectData = await data.ObjectData.FromApplicationUserToAdmin(),
                    Message = "Data Found",
                    status_code = "200",
                };
            }
            catch (Exception ex)
            {
                return new Response<AdminUserVM>
                {
                    Success = true,
                    Message = ex.Message,
                    status_code = "400",
                };
            }
        }
        #endregion

        #region Update 

        public async Task<Response<AdminUserVM>> EditAdminUserAsync(AdminUserVMEdit model)
        {
            try
            {
                var AdminVM = await model.ToApplicationUserVMFromVMEditToAdmin();
                if (model.ProfileImage is not null)
                {
                    var File = UploadFileHelper.SaveFile(model.ProfileImage, "ProfileImage");
                    AdminVM.ProfileImage = _httpContextAccessor.HttpContext.Request.Host.Value + "/ProfileImage/" + File[0];
                }
                if (model.CoverImage is not null)
                {
                    var File = UploadFileHelper.SaveFile(model.CoverImage, "HouseImage");
                    AdminVM.CoverImage = _httpContextAccessor.HttpContext.Request.Host.Value + "/CoverImage/" + File[0];
                }
                var data = await _adminRepo.EditAdminUserAsync(AdminVM);
                if (data.ObjectData == null)
                {
                    return new Response<AdminUserVM>
                    {
                        Success = false,
                        Message = "Error",
                        status_code = "404",
                    };
                }
                return new Response<AdminUserVM>
                {
                    Success = true,
                    ObjectData = await data.ObjectData.FromApplicationUserToAdmin(),
                    Message = "Data Found",
                    status_code = "200",
                };
            }
            catch (Exception ex)
            {
                return new Response<AdminUserVM>
                {
                    Success = true,
                    Message = ex.Message,
                    status_code = "400",
                };
            }
        }
        #endregion

        #region Get All 

        public async Task<Response<AdminUserVM>> GetAdminUsersAsync(int paggingNumber)
        {
            try
            {
                var result = await _adminRepo.GetAllAdminAsync(paggingNumber);

                if (result.Success)
                {
                    double pagging = Convert.ToInt32(result.CountOfData) / 10;
                    if (pagging % 10 == 0)
                    {
                        result.paggingNumber = (int)pagging;
                    }
                    else
                    {
                        result.paggingNumber = (int)pagging + 1;
                    }
                }
                return new Response<AdminUserVM>
                {
                    Success = result.Success,
                    Data = result.Data == null ? null : result.Data.ToList().ConvertAll(x => x.FromApplicationUserToAdmin().Result),
                    error = result.error,
                    Message = result.Message,
                    CountOfData = result.CountOfData,
                    paggingNumber = result.paggingNumber,
                    status_code = result.status_code,
                };

            }
            catch (Exception ex)
            {
                return new Response<AdminUserVM>
                {
                    Success = true,
                    Message = ex.Message,
                    status_code = "400",
                };
            }
        }
        #endregion

    }
}
