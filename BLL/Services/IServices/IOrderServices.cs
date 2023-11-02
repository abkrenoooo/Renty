using DAL.Entities;
using DAL.Models.OrderViewModel;
using Renty.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.IServices
{
    public interface IOrderServices
    {
        Task<Response<OrderVM>> DeleteOrderAsync(int OrderId);
        Task<Response<OrderVM>> GetOrderAsync(int OrderId);
        Task<Response<OrderVM>> Get_OrderForHouseAsync(int houseId, string userId);
        Task<Response<OrderVM>> Get_ActionOrderRequestAsync(ActionOrderRequest request);
        Task<Response<OrderVM>> Edit_ActionOrderRequestAsync(ActionOrderRequest request);
        Task<Response<OrderVM>> GetAllOrderAsync(int paggingNumber);
        Task<Response<OrderVM>> Create_OrderAsync(OrderVMCreate OrderVM, string userId);
        Task<Response<OrderVM>> EditOrderAsync(OrderVMEdit OrderVM, string userId);
        Task<Response<OrderVM>> GetAll_OrderForUserAsync(int paggingNumber, string userId);
    }
}
