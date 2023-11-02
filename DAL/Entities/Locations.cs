using DAL.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Locations
    {
        [Key]
        [Display(Name = "ID")]
        public int LocationId { get; set; }
        [Display(Name = "City Name")]
        public string? CityName { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public virtual ApplicationUser? User { get; set; }
        public virtual House? House { get; set; }
    }
}
