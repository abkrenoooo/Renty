using DAL.Entities;
using DAL.Models.OrderViewModel;
using Renty.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository.IRepository
{
    public interface IOrderRepo
    {
        Task<Response<Order>> Create_OrderAsync(Order order);
        Task<Response<Order>> Delete_OrderAsync(int orderId);
        Task<Response<Order>> Get_OrderAsync(int orderId);
        Task<Response<Order>> Get_ActionOrderRequestAsync(ActionOrderRequest request);
        Task<Response<Order>> Edit_ActionOrderRequestAsync(ActionOrderRequest request);
        Task<Response<Order>> Get_OrderForHouseAsync(int houseId, string userId);
        Task<Response<Order>> GetAll_OrderAsync(int paggingNumber);
        Task<Response<Order>> GetAll_OrderForUserAsync(int paggingNumber, string userId);
        Task<Response<Order>> EditOrderAsync(Order order);
    }
}
