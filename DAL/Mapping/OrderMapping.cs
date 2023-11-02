using DAL.Entities;
using DAL.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Mapping
{
    public static class OrderMapping
    {
        public static async Task<Order> ToOderToEdit(this Order order, Order editorder)
        {
            order.Comment = editorder.Comment;
            order.Customer = editorder.Customer;
            order.CustomerId = editorder.CustomerId;
            order.OrderState = editorder.OrderState;
            order.OrderId = editorder.OrderId;
            return order;
        }
    }
}
