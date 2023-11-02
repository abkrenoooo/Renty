using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Enum
{
    public enum OrderState
    {
        [Display(Name = "Nan")]
        Nan = 1, 
        [Display(Name = "Accepted")]
        Accepted = 2,
        [Display(Name = "Rented")]
        Rented = 3,
        [Display(Name = "Refused")]
        Refused = 4,
    }
}
