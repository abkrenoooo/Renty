using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using DAL.Enum;

namespace DAL.Entities
{
    public class House
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

        [ForeignKey(nameof(AddressId))]
        public int? AddressId { get; set; } 
        public virtual Address? Address { get; set; }
        public virtual List<Order> Orders { get; set; }
        [ForeignKey(nameof(OwnerId))]
        public string? OwnerId { get; set; }
        public virtual ApplicationUser? Owner { get; set; }
    }
}
