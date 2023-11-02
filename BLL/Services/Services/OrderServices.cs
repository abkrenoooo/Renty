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
using DAL.Models.OrderViewModel;
using DAL.ExtensionMethods;
using BLL.ExtensionMethods;
using BlL.Helper;
using System.Runtime.ConstrainedExecution;
using DAL.Mapping;
using DAL.Enum;

namespace BLL.Services.Services
{

    public class OrderServices : IOrderServices
    {
        #region Depend Injection

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IOrderRepo _OrderRepo;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public OrderServices(UserManager<ApplicationUser> UserManager, IOrderRepo OrderRepo, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = UserManager;
            _OrderRepo = OrderRepo;
            _httpContextAccessor = httpContextAccessor;
        }

        #endregion

        #region Create

        public async Task<Response<OrderVM>> Create_OrderAsync(OrderVMCreate Order, string CustomerId)
        {
            try
            {
                var OrderVM = Order.ToOrderVM(CustomerId).Result.ToOrder(CustomerId).Result;
                OrderVM.OrderState = OrderState.Nan;
                var data = await _OrderRepo.Create_OrderAsync(OrderVM);
                if (data.ObjectData == null)
                {
                    return new Response<OrderVM>
                    {
                        Success = data.Success,
                        Message = data.Message,
                        status_code = data.status_code,
                    };
                }
                return new Response<OrderVM>
                {
                    ObjectData = await data.ObjectData.FromOrder(),
                    Success = true,
                    Message = "Order is Created",
                    status_code = "200",
                };
            }
            catch (Exception ex)
            {
                return new Response<OrderVM>
                {
                    Success = false,
                    Message = ex.Message,
                    status_code = "400",
                };
            }
        }
        #endregion

        #region Delete 

        public async Task<Response<OrderVM>> DeleteOrderAsync(int Id)
        {
            try
            {
                var spetialistvm = GetOrderAsync(Id).Result;
                var result = await _OrderRepo.Delete_OrderAsync(Id);
                if (result.status_code != "200")
                {
                    return new Response<OrderVM>
                    {
                        Success = false,
                        Message = "error!",
                        status_code = "400",
                    };
                }
                return new Response<OrderVM>
                {
                    Success = true,
                    Message = "Order has removed",
                    status_code = "200",
                };
            }
            catch (Exception ex)
            {
                return new Response<OrderVM>
                {
                    Success = false,
                    Message = ex.Message,
                    status_code = "400",
                };
            }
        }
        #endregion

        #region Update

        public async Task<Response<OrderVM>> EditOrderAsync(OrderVMEdit Order, string CustomerId)
        {
            try
            {
                var OrderVM = await Order.ToOrderVM(CustomerId);

                var data = await _OrderRepo.EditOrderAsync(OrderVM.ToOrder(CustomerId).Result);

                if (data.ObjectData == null)
                {
                    return new Response<OrderVM>
                    {
                        Success = false,
                        Message = "Error",
                        status_code = "400",
                    };
                }
                return new Response<OrderVM>
                {
                    ObjectData = await data.ObjectData.FromOrder(),
                    Success = true,
                    Message = "Spetialist  is Updated",
                    status_code = "200",
                };
            }
            catch (Exception ex)
            {
                return new Response<OrderVM>
                {
                    Success = false,
                    Message = ex.Message,
                    status_code = "400",
                };
            }
        }

        #endregion

        #region Get All

        public async Task<Response<OrderVM>> GetAllOrderAsync(int paggingNumber)
        {
            try
            {
                var result = await _OrderRepo.GetAll_OrderAsync(paggingNumber);

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
                return new Response<OrderVM>
                {
                    Success = result.Success,
                    Data = result.Data == null ? null : result.Data.ToList().ConvertAll(x => x.FromOrder().Result),
                    error = result.error,
                    Message = result.Message,
                    CountOfData = result.CountOfData,
                    paggingNumber = result.paggingNumber,
                    status_code = result.status_code,
                };
            }
            catch (Exception ex)
            {
                return new Response<OrderVM>
                {
                    Success = true,
                    Message = ex.Message,
                    status_code = "400",
                };
            }
        }

        #endregion

        #region Get All For User

        public async Task<Response<OrderVM>> GetAll_OrderForUserAsync(int paggingNumber, string userId)
        {
            try
            {
                var result = await _OrderRepo.GetAll_OrderForUserAsync(paggingNumber, userId);
                if (result == null)
                {
                    return new Response<OrderVM>
                    {
                        Success = true,
                        Message = "Can't found Orders",
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
                return new Response<OrderVM>
                {
                    Success = result.Success,
                    Data = result.Data == null ? null : result.Data.ToList().ConvertAll(x => x.FromOrder().Result),
                    error = result.error,
                    Message = result.Message,
                    CountOfData = result.CountOfData,
                    paggingNumber = result.paggingNumber,
                    status_code = result.status_code,
                };
            }
            catch (Exception ex)
            {
                return new Response<OrderVM>
                {
                    Success = true,
                    Message = ex.Message,
                    status_code = "400",
                };
            }
        }
        #endregion

        #region Get Orders For House

        public async Task<Response<OrderVM>> Get_OrderForHouseAsync(int houseId, string userId)
        {
            try
            {
                var data = await _OrderRepo.Get_OrderForHouseAsync(houseId, userId);
                if (data.Data == null)
                {
                    return new Response<OrderVM>
                    {
                        Success = false,
                        Message = "Error",
                        status_code = "404",
                    };
                }
                return new Response<OrderVM>
                {
                    Success = true,
                    Data = data.Data.ToList().ConvertAll(x => x.FromOrder().Result),
                    Message = "Data Found",
                    status_code = "200",
                };
            }
            catch (Exception ex)
            {
                return new Response<OrderVM>
                {
                    Success = true,
                    Message = ex.Message,
                    status_code = "400",
                };
            }
        }
        #endregion

        #region Edit Action Order Request

        public async Task<Response<OrderVM>> Edit_ActionOrderRequestAsync(ActionOrderRequest request)
        {
            try
            {
                var data = await _OrderRepo.Edit_ActionOrderRequestAsync(request);
                if (data.ObjectData == null)
                {
                    return new Response<OrderVM>
                    {
                        Success = false,
                        Message = data.Message,
                        status_code = "404",
                    };
                }
                return new Response<OrderVM>
                {
                    Success = true,
                    ObjectData = await data.ObjectData.FromOrder(),
                    Message = "Data Found",
                    status_code = "200",
                };
            }
            catch (Exception ex)
            {
                return new Response<OrderVM>
                {
                    Success = true,
                    Message = ex.Message,
                    status_code = "400",
                };
            }
        }
        #endregion

        #region Action Order Request

        public async Task<Response<OrderVM>> Get_ActionOrderRequestAsync(ActionOrderRequest request)
        {
            try
            {
                var data = await _OrderRepo.Get_ActionOrderRequestAsync(request);
                if (data.ObjectData == null)
                {
                    return new Response<OrderVM>
                    {
                        Success = false,
                        Message = data.Message,
                        status_code = "404",
                    };
                }
                return new Response<OrderVM>
                {
                    Success = true,
                    ObjectData = await data.ObjectData.FromOrder(),
                    Message = "Data Found",
                    status_code = "200",
                };
            }
            catch (Exception ex)
            {
                return new Response<OrderVM>
                {
                    Success = true,
                    Message = ex.Message,
                    status_code = "400",
                };
            }
        }
        #endregion

        #region Get Order

        public async Task<Response<OrderVM>> GetOrderAsync(int OrderId)
        {
            try
            {
                var data = await _OrderRepo.Get_OrderAsync(OrderId);
                if (data.ObjectData == null)
                {
                    return new Response<OrderVM>
                    {
                        Success = false,
                        Message = "Error",
                        status_code = "404",
                    };
                }
                return new Response<OrderVM>
                {
                    Success = true,
                    ObjectData = await data.ObjectData.FromOrder(),
                    Message = "Data Found",
                    status_code = "200",
                };
            }
            catch (Exception ex)
            {
                return new Response<OrderVM>
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
