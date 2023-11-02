using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Enum
{
    public enum RattingValue
    {
        [Display(Name = "Bad")]
        Bad = 1, 
        [Display(Name = "Nice")]
        Nice = 2,
        [Display(Name = "Good")]
        Good = 3,
        [Display(Name = "Very Good")]
        Very_Good = 4,
        [Display(Name = "Excellent")]
        Excellent = 5
    }
}
