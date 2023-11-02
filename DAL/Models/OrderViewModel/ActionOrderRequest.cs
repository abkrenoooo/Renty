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
    public class ActionOrderRequest
    {
        [Required]
        public int OrderId { get; set; }
        [Required]
        public OrderState? OrderState { get; set; }
    }
}
