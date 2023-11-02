using DAL.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public string? Comment { get; set; }
        [ForeignKey(nameof(CustomerId))]
        public string? CustomerId { get; set; }
        public virtual ApplicationUser? Customer { get; set; }
        [ForeignKey(nameof(HouseId))]
        public int? HouseId { get; set; }
        public virtual House? House { get; set; }
        public OrderState? OrderState { get; set; }
    }
}
