
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
   public class RealtimeModel
    {
        public int Id { get; set; }
        [ForeignKey(nameof(SenderId))]
        public string SenderId { get; set; }
        public virtual ApplicationUser Sender { get; set; }
        [ForeignKey(nameof(ReciverId))]
        public string ReciverId { get; set; }
        public virtual ApplicationUser Reciver { get; set; }
        public string Content { get; set; }
        public bool Read { get; set; }
        public DateTimeOffset Createdate { get; set; }
    }
}
