using DAL.Entities;
using DAL.Enum;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models.HouseViewModel
{
    public class HouseVMEdit
    {
        public int HouseId { get; set; }
        public string? Name { get; set; }
        public IFormFile? Photo1 { get; set; }
        public IFormFile? Photo2 { get; set; }
        public IFormFile? Photo3 { get; set; }
        public IFormFile? Photo4 { get; set; }
        public decimal Price { get; set; }
        public decimal Area { get; set; }
        public byte NumberOfRoom { get; set; }
        public byte NumberOfBath { get; set; }
        public byte Floor { get; set; }
        public DateTime? Date { get; set; }
        public HouseType? HouseType { get; set; }
        public HouseTarget? HouseTarget { get; set; }
        public int? AddressId { get; set; }
        public virtual Address? address { get; set; }

    }
}
