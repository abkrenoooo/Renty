
using DAL.Entities;
using DAL.Models.HouseViewModel;
using DAL.Models.OrderViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ExtensionMethods
{
    public static class OrderMapping
    {
        public static async Task<Order> ToOrder(this OrderVM orderVM,string CustomerId)
        {
            return new()
            {
                Comment = orderVM.Comment,
                CustomerId = CustomerId,
                House = orderVM.House,
                HouseId = orderVM.HouseId,
                OrderState = orderVM.OrderState,
                OrderId = orderVM.OrderId,
            };
        }
        public static async Task<OrderVM> ToOrderVM(this OrderVMEdit orderVM,string CustomerId)
        {
            return new()
            {
                Comment = orderVM.Comment,
                HouseId = orderVM.HouseId,
                OrderId = orderVM.OrderId,
                CustomerId = CustomerId,
            };
        }
        public static async Task<OrderVM> ToOrderVM(this OrderVMCreate orderVM,string CustomerId)
        {
            return new()
            {
                Comment = orderVM.Comment,
                HouseId = orderVM.HouseId,
                CustomerId= CustomerId,
            };
        }
        public static async Task<OrderVM> FromOrder(this Order order)
        {
            return new()
            {
                Comment = order.Comment,
                Customer = order.Customer,
                CustomerId = order.CustomerId,
                House = order.House,
                HouseId = order.HouseId,
                OrderState = order.OrderState,
                OrderId = order.OrderId,
            };
        }
    }
}
