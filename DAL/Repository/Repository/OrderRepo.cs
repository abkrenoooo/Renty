using DAL.Entities;
using DAL.Enum;
using DAL.Models.OrderViewModel;
using DAL.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Renty.DAL.Data;
using Renty.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository.Repository
{
    public class OrderRepo : IOrderRepo
    {
        #region Depend Injection

        private readonly ApplicationDbContext _db;

        public OrderRepo(ApplicationDbContext db)
        {
            _db = db;
        }
        #endregion

        #region Create
        public async Task<Response<Order>> Create_OrderAsync(Order Order)
        {
            try
            {
                var anotherOrder = await _db.Orders.Where(x => x.CustomerId == Order.CustomerId && x.HouseId == Order.HouseId).FirstOrDefaultAsync();
                if (anotherOrder != null)
                {
                    return new Response<Order>
                    {
                        Success = true,
                        Message = "Can't Create Order Because you are make the order to this house before",
                        status_code = "500",
                    };
                }
                var House = await _db.Houses.Where(x => x.HouseId == Order.HouseId).FirstOrDefaultAsync();
                if (House == null)
                {
                    return new Response<Order>
                    {
                        Success = true,
                        Message = "This houseIs is Not Found",
                        status_code = "500",
                    };
                }
                if (House.OwnerId == Order.CustomerId)
                {
                    return new Response<Order>
                    {
                        Success = true,
                        Message = "Can't Create Order Because you are the Owner of This house",
                        status_code = "500",
                    };
                }
                await _db.Orders.AddAsync(Order);
                var result = await _db.SaveChangesAsync();
                if (result > 0)
                {
                    return new Response<Order>
                    {
                        Success = true,
                        Message = "Order Is Created",
                        status_code = "200",
                        ObjectData=Order
                    };
                }
                return new Response<Order>
                {
                    Success = true,
                    Message = "Can't Create the Order",
                    status_code = "500",
                };
            }
            catch (Exception e)
            {
                return new Response<Order>
                {
                    Success = false,
                    Message = e.Message,
                    status_code = "500"
                };
            }
        }
        #endregion

        #region Delete
        public async Task<Response<Order>> Delete_OrderAsync(int OrderId)
        {
            try
            {
                var Order = await _db.Orders.Where(n => n.OrderId == OrderId).SingleOrDefaultAsync();

                if (Order == null)
                {
                    return new Response<Order>
                    {
                        Success = true,
                        Message = "Order Not Found",
                        status_code = "404"
                    };
                }
                _db.Orders.Remove(Order);
                var result = await _db.SaveChangesAsync();
                if (result > 0)
                {
                    return new Response<Order>
                    {
                        Success = true,
                        Message = "Order Is Deleted",
                        status_code = "200",
                    };
                }
                return new Response<Order>
                {
                    Success = true,
                    Message = "Can't delete the Order",
                    status_code = "500",
                };
            }
            catch (Exception e)
            {
                return new Response<Order>
                {
                    Success = true,
                    error = e.Message,
                    status_code = "500"
                };
            }
        }
        #endregion

        #region Update
        public async Task<Response<Order>> EditOrderAsync(Order Order)
        {
            try
            {
                var oldOrder = await _db.Orders.FirstOrDefaultAsync(x => x.OrderId == Order.OrderId);
                //oldOrder.AdditionalInformation = Order.AdditionalInformation;
                //oldOrder.Flat = Order.Flat;
                //oldOrder.Floor = Order.Floor;
                _db.Orders.Update(oldOrder);
                var result = await _db.SaveChangesAsync();
                if (result > 0)
                {
                    return new Response<Order>
                    {
                        Success = true,
                        Message = "Order Is Edit",
                        status_code = "200",
                    };
                }
                return new Response<Order>
                {
                    Success = true,
                    Message = "Can't Edit the Order",
                    status_code = "500",
                };
            }
            catch (Exception e)
            {
                return new Response<Order>
                {
                    Success = false,
                    error = e.Message,
                    status_code = "500"
                };
            }
        }
        #endregion

        #region Get All Order For User
        public async Task<Response<Order>> GetAll_OrderForUserAsync(int paggingNumber, string userId)
        {
            try
            {
                int AllPatientcount = await _db.Orders.Where(x => x.CustomerId == userId).CountAsync();
                var AllPatient = await _db.Orders.Where(x => x.CustomerId == userId).Skip((paggingNumber - 1) * 10).Take(10).ToListAsync(); ;
                return new Response<Order>
                {
                    Success = true,
                    Message = "All Order",
                    Data = AllPatient,
                    CountOfData = AllPatientcount,
                    paggingNumber = paggingNumber,
                    status_code = "200"
                };

            }
            catch (Exception e)
            {
                return new Response<Order>
                {
                    Success = false,
                    error = e.Message,
                    status_code = "500"
                };
            }
        }
        #endregion

        #region Get All
        public async Task<Response<Order>> GetAll_OrderAsync(int paggingNumber)
        {
            try
            {
                int AllPatientcount = await _db.Orders.CountAsync();
                var AllPatient = await _db.Orders.Skip((paggingNumber - 1) * 10).Take(10).ToListAsync(); ;
                return new Response<Order>
                {
                    Success = true,
                    Message = "All Order",
                    Data = AllPatient,
                    CountOfData = AllPatientcount,
                    paggingNumber = paggingNumber,
                    status_code = "200"
                };

            }
            catch (Exception e)
            {
                return new Response<Order>
                {
                    Success = false,
                    error = e.Message,
                    status_code = "500"
                };
            }
        }
        #endregion

        #region Get Order For House
        public async Task<Response<Order>> Get_OrderForHouseAsync(int houseId, string userId)
        {
            try
            {
                var Order = await _db.Orders.Include(x => x.House).Where(n => n.House.OwnerId == userId && n.HouseId == houseId).ToListAsync();
                if (Order == null)
                {
                    return new Response<Order>
                    {
                        Success = false,
                        status_code = "404",
                        Message = "Order Not found",

                    };
                }
                return new Response<Order>
                {
                    Success = true,
                    Message = "the patient",
                    status_code = "200",
                    Data = Order
                };

            }
            catch (Exception e)
            {
                return new Response<Order>
                {
                    Success = false,
                    error = e.Message,
                    status_code = "500"
                };

            }
        }
        #endregion

        #region Edit Action order Request
        public async Task<Response<Order>> Edit_ActionOrderRequestAsync(ActionOrderRequest request)
        {
            try
            {
                var Order = await _db.Orders.Include(x=>x.House).Where(n => n.OrderId == request.OrderId).FirstOrDefaultAsync();
                if (Order == null)
                {
                    return new Response<Order>
                    {
                        Success = false,
                        status_code = "404",
                        Message = "Order Not found",
                    };
                }
                if (request.OrderState == OrderState.Accepted)
                {
                    Order.OrderState = OrderState.Accepted;
                    var House = await _db.Houses.Where(n => n.HouseId == Order.HouseId).FirstOrDefaultAsync(); 
                    if (House == null)
                    {
                        return new Response<Order>
                        {
                            Success = false,
                            status_code = "404",
                            Message = "House In Order is Not found",

                        };
                    }
                    //Order.House = null;
                    if (House.IsCompleted == true)
                    {
                        return new Response<Order>
                        {
                            Success = false,
                            status_code = "500",
                            Message = "This House is already Rented",

                        };
                    }
                    House.IsCompleted=true;
                    await _db.Orders.Where(n => n.HouseId == Order.HouseId&&n.OrderId!= Order.OrderId).ForEachAsync(s => s.OrderState = OrderState.Rented);
                }
                else if (request.OrderState == OrderState.Refused)
                {
                    Order.OrderState = OrderState.Refused;
                    var House = await _db.Houses.Where(n => n.HouseId == Order.HouseId).FirstOrDefaultAsync();
                    if (House == null)
                    {
                        return new Response<Order>
                        {
                            Success = false,
                            status_code = "404",
                            Message = "House In Order is Not found",

                        };
                    }
                    House.IsCompleted = false;
                    await _db.Orders.Where(n => n.HouseId == Order.HouseId && n.OrderId != Order.OrderId).ForEachAsync(s => s.OrderState = OrderState.Nan);
                }
                else if (request.OrderState == OrderState.Nan)
                {
                    return new Response<Order>
                    {
                        Success = true,
                        Message = "Order State must not be Nan",
                        status_code = "500",
                    };
                }
                else
                {
                    return new Response<Order>
                    {
                        Success = true,
                        Message = "Order State must not be Rented",
                        status_code = "500",
                    };
                }
                var result = await _db.SaveChangesAsync();
                if (result>=0)
                {
                    Order.House = null;
                    return new Response<Order>
                    {
                        Success = true,
                        Message = "the order Action Is Success",
                        status_code = "200",
                        ObjectData = Order
                    };
                }
                return new Response<Order>
                {
                    Success = true,
                    Message = "Error to Accepted Request",
                    status_code = "500",
                };

            }
            catch (Exception e)
            {
                return new Response<Order>
                {
                    Success = false,
                    error = e.Message,
                    status_code = "500"
                };

            }
        }
        #endregion

        #region Action order Request
        public async Task<Response<Order>> Get_ActionOrderRequestAsync(ActionOrderRequest request)
        {
            try
            {
                var Order = await _db.Orders.Include(x=>x.House).Where(n => n.OrderId == request.OrderId).FirstOrDefaultAsync();
                if (Order == null)
                {
                    return new Response<Order>
                    {
                        Success = false,
                        status_code = "404",
                        Message = "Order Not found",
                    };
                }
                if (request.OrderState == OrderState.Accepted)
                {
                    Order.OrderState = OrderState.Accepted;
                    var House = await _db.Houses.Where(n => n.HouseId == Order.HouseId).FirstOrDefaultAsync(); 
                    if (House == null)
                    {
                        return new Response<Order>
                        {
                            Success = false,
                            status_code = "404",
                            Message = "House In Order is Not found",

                        };
                    }
                    //Order.House = null;
                    if (House.IsCompleted == true)
                    {
                        return new Response<Order>
                        {
                            Success = false,
                            status_code = "500",
                            Message = "This House is already Rented",

                        };
                    }
                    House.IsCompleted=true;
                    await _db.Orders.Where(n => n.HouseId == Order.HouseId&&n.OrderId!= Order.OrderId).ForEachAsync(s => s.OrderState = OrderState.Rented);
                }
                else if (request.OrderState == OrderState.Refused)
                {
                    Order.OrderState = OrderState.Refused;
                }
                else if (request.OrderState == OrderState.Nan)
                {
                    return new Response<Order>
                    {
                        Success = true,
                        Message = "Order State must not be Nan",
                        status_code = "500",
                    };
                }
                else
                {
                    return new Response<Order>
                    {
                        Success = true,
                        Message = "Order State must not be Rented",
                        status_code = "500",
                    };
                }
                var result = await _db.SaveChangesAsync();
                if (result>=0)
                {
                    Order.House = null;
                    return new Response<Order>
                    {
                        Success = true,
                        Message = "the order Action Is Success",
                        status_code = "200",
                        ObjectData = Order
                    };
                }
                return new Response<Order>
                {
                    Success = true,
                    Message = "Error to Accepted Request",
                    status_code = "500",
                };

            }
            catch (Exception e)
            {
                return new Response<Order>
                {
                    Success = false,
                    error = e.Message,
                    status_code = "500"
                };

            }
        }
        #endregion

        #region Get 
        public async Task<Response<Order>> Get_OrderAsync(int OrderId)
        {
            try
            {
                var Order = await _db.Orders.Where(n => n.OrderId == OrderId).FirstOrDefaultAsync();
                if (Order == null)
                {
                    return new Response<Order>
                    {
                        Success = false,
                        status_code = "200",
                        Message = "Order Not found",

                    };
                }
                return new Response<Order>
                {
                    Success = true,
                    Message = "the patient",
                    status_code = "200",
                    ObjectData = Order
                };

            }
            catch (Exception e)
            {
                return new Response<Order>
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
