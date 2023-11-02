using DAL.Entities;
using DAL.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models.OrderViewModel
{
    public class OrderVM
    {
        public int OrderId { get; set; }
        public string? Comment { get; set; }
        public string? CustomerId { get; set; }
        public virtual ApplicationUser? Customer { get; set; }
        public int? HouseId { get; set; }
        public virtual House? House { get; set; }
        public OrderState? OrderState { get; set; }
    }
}
