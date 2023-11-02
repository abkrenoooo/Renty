using BLL.Services.IServices;
using DAL.Enum;
using DAL.Models.SuperAdmin;
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
    public class SuperAdminSevices : ISuperAdminSevices
    {
        #region Depend Injection

        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JWT _jwt;
        private readonly ISuperAdminRepo _SuperAdminRepo;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SuperAdminSevices(UserManager<ApplicationUser> UserManager, ApplicationDbContext db, IOptions<JWT> jwt, ISuperAdminRepo SuperAdminRepo, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _jwt = jwt.Value;
            _userManager = UserManager;
            _SuperAdminRepo = SuperAdminRepo;
            _httpContextAccessor = httpContextAccessor;
        }
        #endregion

        #region Create 

        public async Task<Response<SuperAdminUserVM>> CreateSuperAdminUserAsync(SuperAdminUserCreateVM model)
        {
            try
            {
                var result = await _SuperAdminRepo.CreateSuperAdminUserAsync(model.ToApplicationUserVMToSuperAdmin().Result);
                if (!result.Success|| result.ObjectData==null)
                {
                    return new Response<SuperAdminUserVM>
                    {
                        Success = result.Success,
                        Message = result.Message,
                        status_code = result.status_code,
                    };
                }
                return new Response<SuperAdminUserVM>
                {
                    Success = true,
                    ObjectData =result.ObjectData.FromApplicationUserToSuperAdmin().Result ,
                    status_code = "200",
                };
            }
            catch (Exception ex)
            {
                return new Response<SuperAdminUserVM>
                {
                    Success = false,
                    Message = ex.Message,
                    status_code = "400",
                };
            }

        }
        #endregion

        #region Delete 

        public async Task<Response<SuperAdminUserVM>> RemoveSuperAdminUserAsync(string SuperAdminUserId)
        {
            try
            {
                if (!await _SuperAdminRepo.RemoveSuperAdminUserAsync(SuperAdminUserId))
                {
                    return new Response<SuperAdminUserVM>
                    {
                        Success = false,
                        Message = "error!",
                        status_code = "500",
                    };
                }
                return new Response<SuperAdminUserVM>
                {
                    Success = true,
                    Message = "SuperAdmin has removed",
                    status_code = "200",
                };
            }
            catch (Exception ex)
            {
                return new Response<SuperAdminUserVM>
                {
                    Success = false,
                    Message = ex.Message,
                    status_code = "500",
                };
            }
        }
        #endregion

        #region Get 

        public async Task<Response<SuperAdminUserVM>> GetSuperAdminUserByIdAsync(string SuperAdminUserId)
        {
            try
            {
                var data = await _SuperAdminRepo.GetSuperAdminUserByIdAsync(SuperAdminUserId);
                if (data.ObjectData == null)
                {
                    return new Response<SuperAdminUserVM>
                    {
                        Success = false,
                        Message = "SuperAdmin Is Not Found",
                        status_code = "404",
                    };
                }
                return new Response<SuperAdminUserVM>
                {
                    Success = true,
                    ObjectData = await data.ObjectData.FromApplicationUserToSuperAdmin(),
                    Message = "Data Found",
                    status_code = "200",
                };
            }
            catch (Exception ex)
            {
                return new Response<SuperAdminUserVM>
                {
                    Success = true,
                    Message = ex.Message,
                    status_code = "400",
                };
            }
        }
        #endregion

        #region Update 

        public async Task<Response<SuperAdminUserVM>> EditSuperAdminUserAsync(SuperAdminUserVMEdit model)
        {
            try
            {
                var SuperAdminVM = await model.ToApplicationUserVMEditToSuperAdmin();
                if (model.ProfileImage is not null)
                {
                    var File = UploadFileHelper.SaveFile(model.ProfileImage, "ProfileImage");
                    SuperAdminVM.ProfileImage = _httpContextAccessor.HttpContext.Request.Host.Value + "/ProfileImage/" + File[0];
                }
                if (model.CoverImage is not null)
                {
                    var File = UploadFileHelper.SaveFile(model.CoverImage, "HouseImage");
                    SuperAdminVM.CoverImage = _httpContextAccessor.HttpContext.Request.Host.Value + "/CoverImage/" + File[0];
                }
                var data = await _SuperAdminRepo.EditSuperAdminUserAsync(SuperAdminVM);
                if (data.ObjectData == null)
                {
                    return new Response<SuperAdminUserVM>
                    {
                        Success = false,
                        Message = "Error",
                        status_code = "404",
                    };
                }
                return new Response<SuperAdminUserVM>
                {
                    Success = true,
                    ObjectData = await data.ObjectData.FromApplicationUserToSuperAdmin(),
                    Message = "Data Found",
                    status_code = "200",
                };
            }
            catch (Exception ex)
            {
                return new Response<SuperAdminUserVM>
                {
                    Success = true,
                    Message = ex.Message,
                    status_code = "400",
                };
            }
        }
        #endregion

        #region Get All 

        public async Task<Response<SuperAdminUserVM>> GetSuperAdminUsersAsync(int paggingNumber)
        {
            try
            {
                var result = await _SuperAdminRepo.GetAllSuperAdminAsync(paggingNumber);

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
                return new Response<SuperAdminUserVM>
                {
                    Success = result.Success,
                    Data = result.Data == null ? null : result.Data.ToList().ConvertAll(x => x.FromApplicationUserToSuperAdmin().Result),
                    error = result.error,
                    Message = result.Message,
                    CountOfData = result.CountOfData,
                    paggingNumber = result.paggingNumber,
                    status_code = result.status_code,
                };

            }
            catch (Exception ex)
            {
                return new Response<SuperAdminUserVM>
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
