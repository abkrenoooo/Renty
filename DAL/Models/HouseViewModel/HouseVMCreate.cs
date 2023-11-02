using DAL.Entities;
using DAL.Enum;
using DAL.Models.AddressViewModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models.HouseViewModel
{
    public class HouseVMCreate
    {
        public string? Name { get; set; }
        public IFormFile Photo1 { get; set; }
        public IFormFile? Photo2 { get; set; }
        public IFormFile? Photo3 { get; set; }
        public IFormFile? Photo4 { get; set; }
        public decimal Price { get; set; }
        public decimal Area { get; set; }
        public byte NumberOfRoom { get; set; }
        public byte NumberOfBath { get; set; }
        public byte Floor { get; set; }
        public HouseType? HouseType { get; set; }
        public HouseTarget? HouseTarget { get; set; }
        public virtual CreateAddressVM? CreateAddressVM { get; set; }
    }
}
