using DAL.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class Notifications 
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey(nameof(SenderId))]
        public string? SenderId { get; set; }
        public virtual ApplicationUser? Sender { get; set; }
        [ForeignKey(nameof(ReciverId))]
        public string? ReciverId { get; set; }
        public virtual ApplicationUser? Reciver { get; set; }
        public string? Content { get; set; }
        public bool Read { get; set; }
        public DateTime? Createdate { get; set; }
        [ForeignKey(nameof(OrderId))]
        public int? OrderId { get; set; }
        public virtual Order? Order { get; set; }
        public string? Type { get; set; }
    }
}
