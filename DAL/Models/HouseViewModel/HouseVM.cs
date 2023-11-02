using DAL.Entities;
using DAL.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models.HouseViewModel
{
    public class HouseVM
    {
        public int HouseId { get; set; }
        public string? Name { get; set; }
        public string? Photo1 { get; set; }
        public string? Photo2 { get; set; }
        public string? Photo3 { get; set; }
        public string? Photo4 { get; set; }
        public decimal Price { get; set; }
        public decimal Area { get; set; }
        public byte NumberOfRoom { get; set; }
        public byte NumberOfBath { get; set; }
        public byte Floor { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime? Date { get; set; }
        public HouseType? HouseType { get; set; }
        public HouseTarget? HouseTarget { get; set; }
        public int? AddressId { get; set; }
        public virtual Address Address { get; set; }
        public string? OwnerId { get; set; }
    }
}
