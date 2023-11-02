using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Enum
{
    public enum Roles
    {
        [Display(Name = "Server")]
        Server = 1,
        [Display(Name = "Super Admin")]
        SuperAdmin = 2,
        [Display(Name = "Admin")]
        Admin = 3,
        [Display(Name = "User")]
        User = 4,
    }
}
