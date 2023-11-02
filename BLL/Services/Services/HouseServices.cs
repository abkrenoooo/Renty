using BLL.Services.IServices;
using DAL.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Renty.Models;
using DAL.Models.HouseViewModel;
using DAL.ExtensionMethods;
using BLL.ExtensionMethods;
using BlL.Helper;
using System.Runtime.ConstrainedExecution;
using DAL.Mapping;
using BLL.Mapping;

namespace BLL.Services.Services
{

    public class HouseServices : IHouseServices
    {
        #region Depend Injection

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHouseRepo _HouseRepo;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HouseServices(UserManager<ApplicationUser> UserManager, IHouseRepo HouseRepo, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = UserManager;
            _HouseRepo = HouseRepo;
            _httpContextAccessor = httpContextAccessor;
        }

        #endregion

        #region Delete 

        public async Task<Response<HouseVM>> DeleteHouseAsync(int Id)
        {
            try
            {
                var spetialistvm = GetHouseAsync(Id).Result;
                var result = await _HouseRepo.Delete_HouseAsync(Id);
                if (result.status_code != "200")
                {

                    return new Response<HouseVM>
                    {
                        Success = false,
                        Message = "error!",
                        status_code = "400",
                    };
                }
                return new Response<HouseVM>
                {
                    Success = true,
                    Message = "House has removed",
                    status_code = "200",
                };
            }
            catch (Exception ex)
            {
                return new Response<HouseVM>
                {
                    Success = false,
                    Message = ex.Message,
                    status_code = "400",
                };
            }
        }
        #endregion

        #region Update

        public async Task<Response<HouseVM>> EditHouseAsync(HouseVMEdit house, string OwnerId)
        {
            try
            {
                var houseVM = await house.ToHouseVM(OwnerId);
                if (house.Photo1 is null && house.Photo2 is null && house.Photo3 is null && house.Photo4 is null)
                {
                    return new Response<HouseVM>
                    {
                        Success = false,
                        Message = "You must upload at least one photo",
                        status_code = "400",
                    };
                }
                if (house.Photo1 is not null)
                {
                    var File = UploadFileHelper.SaveFile(house.Photo1, "HouseImage");
                    houseVM.Photo1 = _httpContextAccessor.HttpContext.Request.Host.Value + "/HouseImage/" + File[0];
                }
                if (house.Photo2 is not null)
                {
                    var File = UploadFileHelper.SaveFile(house.Photo2, "HouseImage");
                    houseVM.Photo2 = _httpContextAccessor.HttpContext.Request.Host.Value + "/HouseImage/" + File[0];
                }
                if (house.Photo3 is not null)
                {
                    var File = UploadFileHelper.SaveFile(house.Photo3, "HouseImage");
                    houseVM.Photo3 = _httpContextAccessor.HttpContext.Request.Host.Value + "/HouseImage/" + File[0];
                }
                if (house.Photo4 is not null)
                {
                    var File = UploadFileHelper.SaveFile(house.Photo4, "HouseImage");
                    houseVM.Photo4 = _httpContextAccessor.HttpContext.Request.Host.Value + "/HouseImage/" + File[0];
                }
                var data = await _HouseRepo.EditHouseAsync(houseVM.ToHouse(OwnerId).Result);

                if (data.ObjectData == null)
                {
                    return new Response<HouseVM>
                    {
                        Success = false,
                        Message = "Error",
                        status_code = "400",
                    };
                }
                return new Response<HouseVM>
                {
                    ObjectData = await data.ObjectData.FromHouse(),
                    Success = true,
                    Message = "Spetialist  is Updated",
                    status_code = "200",
                };
            }
            catch (Exception ex)
            {
                return new Response<HouseVM>
                {
                    Success = false,
                    Message = ex.Message,
                    status_code = "400",
                };
            }
        }

        #endregion

        #region Get All

        public async Task<Response<HouseVM>> GetAllHouseAsync(int paggingNumber)
        {
            try
            {
                var result = await _HouseRepo.GetAll_HouseAsync(paggingNumber);

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
                return new Response<HouseVM>
                {
                    Success = result.Success,
                    Data = result.Data == null ? null : result.Data.ToList().ConvertAll(x => x.FromHouse().Result),
                    error = result.error,
                    Message = result.Message,
                    CountOfData = result.CountOfData,
                    paggingNumber = result.paggingNumber,
                    status_code = result.status_code,
                };
            }
            catch (Exception ex)
            {
                return new Response<HouseVM>
                {
                    Success = true,
                    Message = ex.Message,
                    status_code = "400",
                };
            }
        }

        #endregion

        #region get All For User

        public async Task<Response<HouseVM>> GetAll_HouseForUserAsync(int paggingNumber, string userId)
        {
            try
            {
                var result = await _HouseRepo.GetAll_HouseForUserAsync(paggingNumber, userId);
                if (result==null)
                {
                    return new Response<HouseVM>
                    {
                        Success = true,
                        Message ="Can't found Houses",
                        status_code = "400",
                    };
                }
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
                return new Response<HouseVM>
                {
                    Success = result.Success,
                    Data = result.Data == null ? null : result.Data.ToList().ConvertAll(x => x.FromHouse().Result),
                    error = result.error,
                    Message = result.Message,
                    CountOfData = result.CountOfData,
                    paggingNumber = result.paggingNumber,
                    status_code = result.status_code,
                };
            }
            catch (Exception ex)
            {
                return new Response<HouseVM>
                {
                    Success = true,
                    Message = ex.Message,
                    status_code = "400",
                };
            }
        }
        #endregion

        #region Get House

        public async Task<Response<HouseVM>> GetHouseAsync(int houseId)
        {
            try
            {
                var data = await _HouseRepo.Get_HouseAsync(houseId);
                if (data.ObjectData == null)
                {
                    return new Response<HouseVM>
                    {
                        Success = false,
                        Message = "Error",
                        status_code = "404",
                    };
                }
                return new Response<HouseVM>
                {
                    Success = true,
                    ObjectData = await data.ObjectData.FromHouse(),
                    Message = "Data Found",
                    status_code = "200",
                };
            }
            catch (Exception ex)
            {
                return new Response<HouseVM>
                {
                    Success = true,
                    Message = ex.Message,
                    status_code = "400",
                };
            }
        }
        #endregion

        #region Create

        public async Task<Response<HouseVM>> Create_HouseAsync(HouseVMCreate house, string OwnerId)
        {
            try
            {
                var houseVM =await house.ToHouseVM(OwnerId);
                houseVM.Address = houseVM.Address;
                if (house.Photo1 is  null&&house.Photo2 is  null&&house.Photo3 is  null&&house.Photo4 is  null)
                {
                    return new Response<HouseVM>
                    {
                        Success = false,
                        Message = "You must upload at least one photo",
                        status_code = "400",
                    };
                }
                if (house.CreateAddressVM is null)
                {
                    return new Response<HouseVM>
                    {
                        Success = false,
                        Message = "You must Enter the Address",
                        status_code = "500",
                    };
                }

                if (house.Photo1 is not null)
                {
                    var File = UploadFileHelper.SaveFile(house.Photo1, "HouseImage");
                    houseVM.Photo1 = _httpContextAccessor.HttpContext.Request.Host.Value + "/HouseImage/" + File[0];
                }
                if (house.Photo2 is not null)
                {
                    var File = UploadFileHelper.SaveFile(house.Photo2, "HouseImage");
                    houseVM.Photo2 = _httpContextAccessor.HttpContext.Request.Host.Value + "/HouseImage/" + File[0];
                }
                if (house.Photo3 is not null)
                {
                    var File = UploadFileHelper.SaveFile(house.Photo3, "HouseImage");
                    houseVM.Photo3 = _httpContextAccessor.HttpContext.Request.Host.Value + "/HouseImage/" + File[0];
                }
                if (house.Photo4 is not null)
                {
                    var File = UploadFileHelper.SaveFile(house.Photo4, "HouseImage");
                    houseVM.Photo4 = _httpContextAccessor.HttpContext.Request.Host.Value + "/HouseImage/" + File[0];
                }
                var newhaos = houseVM.ToHouse(OwnerId).Result;
                newhaos.Address=house.CreateAddressVM.ToAddressVM().Result.ToAddress().Result;
                var data = await _HouseRepo.Create_HouseAsync(newhaos);

                if (data.ObjectData == null)
                {
                    return new Response<HouseVM>
                    {
                        Success = data.Success,
                        Message = data.Message,
                        status_code = data.status_code,
                    };
                }
                return new Response<HouseVM>
                {
                    ObjectData = await data.ObjectData.FromHouse(),
                    Success = true,
                    Message = "House is Updated",
                    status_code = "200",
                };
            }
            catch (Exception ex)
            {
                return new Response<HouseVM>
                {
                    Success = false,
                    Message = ex.Message,
                    status_code = "400",
                };
            }
        }
        #endregion
    }
}
