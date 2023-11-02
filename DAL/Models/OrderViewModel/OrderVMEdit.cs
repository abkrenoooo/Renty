using DAL.Entities;
using DAL.Enum;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models.OrderViewModel
{
    public class OrderVMEdit
    {
        [Key]
        public int OrderId { get; set; }
        public string? Comment { get; set; }
        [Display(Name = "House Id")]
        public int? HouseId { get; set; }
    }
}
