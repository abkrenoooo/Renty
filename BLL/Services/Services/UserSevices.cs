using BLL.Services.IServices;
using DAL.Enum;
using DAL.Models.User;
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
    public class UserSevices : IUserSevices
    {
        #region Depend Injection

        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JWT _jwt;
        private readonly IUserRepo _UserRepo;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserSevices(UserManager<ApplicationUser> UserManager, ApplicationDbContext db, IOptions<JWT> jwt, IUserRepo UserRepo, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _jwt = jwt.Value;
            _userManager = UserManager;
            _UserRepo = UserRepo;
            _httpContextAccessor = httpContextAccessor;

        }
        #endregion

        #region Delete 

        public async Task<Response<UserVM>> RemoveUserUserAsync(string UserUserId)
        {
            try
            {
                if (!await _UserRepo.RemoveUserUserAsync(UserUserId))
                {
                    return new Response<UserVM>
                    {
                        Success = false,
                        Message = "error!",
                        status_code = "500",
                    };
                }
                return new Response<UserVM>
                {
                    Success = true,
                    Message = "User has removed",
                    status_code = "200",
                };
            }
            catch (Exception ex)
            {
                return new Response<UserVM>
                {
                    Success = false,
                    Message = ex.Message,
                    status_code = "500",
                };
            }
        }
        #endregion

        #region Get 

        public async Task<Response<UserVM>> GetUserUserByIdAsync(string UserUserId)
        {
            try
            {
                var data = await _UserRepo.GetUserUserByIdAsync(UserUserId);
                if (data.ObjectData == null)
                {
                    return new Response<UserVM>
                    {
                        Success = false,
                        Message = "User Is Not Found",
                        status_code = "404",
                    };
                }
                return new Response<UserVM>
                {
                    Success = true,
                    ObjectData = await data.ObjectData.FromApplicationUserToUser(),
                    Message = "Data Found",
                    status_code = "200",
                };
            }
            catch (Exception ex)
            {
                return new Response<UserVM>
                {
                    Success = true,
                    Message = ex.Message,
                    status_code = "400",
                };
            }
        }
        #endregion

        #region Update 

        public async Task<Response<UserVM>> EditUserUserAsync(UserVMEdit model)
        {
            try
            {
                var UserVM = await model.ToApplicationUserVMEditToUser();
                if (model.ProfileImage is not null)
                {
                    var File = UploadFileHelper.SaveFile(model.ProfileImage, "ProfileImage");
                    UserVM.ProfileImage = _httpContextAccessor.HttpContext.Request.Host.Value + "/ProfileImage/" + File[0];
                }
                if (model.CoverImage is not null)
                {
                    var File = UploadFileHelper.SaveFile(model.CoverImage, "HouseImage");
                    UserVM.CoverImage = _httpContextAccessor.HttpContext.Request.Host.Value + "/CoverImage/" + File[0];
                }
                var data = await _UserRepo.EditUserUserAsync(UserVM);
                if (data.ObjectData == null)
                {
                    return new Response<UserVM>
                    {
                        Success = false,
                        Message = "Error",
                        status_code = "404",
                    };
                }
                return new Response<UserVM>
                {
                    Success = true,
                    ObjectData = await data.ObjectData.FromApplicationUserToUser(),
                    Message = "Data Found",
                    status_code = "200",
                };
            }
            catch (Exception ex)
            {
                return new Response<UserVM>
                {
                    Success = true,
                    Message = ex.Message,
                    status_code = "400",
                };
            }
        }
        #endregion

        #region Get All 

        public async Task<Response<UserVM>> GetUserUsersAsync(int paggingNumber)
        {
            try
            {
                var result = await _UserRepo.GetAllUserAsync(paggingNumber);

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
                return new Response<UserVM>
                {
                    Success = result.Success,
                    Data = result.Data == null ? null : result.Data.ToList().ConvertAll(x => x.FromApplicationUserToUser().Result),
                    error = result.error,
                    Message = result.Message,
                    CountOfData = result.CountOfData,
                    paggingNumber = result.paggingNumber,
                    status_code = result.status_code,
                };

            }
            catch (Exception ex)
            {
                return new Response<UserVM>
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
