using DAL.Entities;
using DAL.Enum;
using DAL.Models.AddressViewModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models.OrderViewModel
{
    public class OrderVMCreate
    {
        [Display(Name ="Comment")]
        public string? Comment { get; set; }
        [Required]
        [Display(Name = "House Id")]
        public int? HouseId { get; set; }
    }
}
