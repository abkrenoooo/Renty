using System.ComponentModel.DataAnnotations;

namespace DAL.Enum
{
    public enum Modules
    {
        [Display(Name = "House")]
        House = 1,
        [Display(Name = "Super Admin")]
        SuperAdmin = 2,
        [Display(Name = "Admin")]
        Admin = 3,
        [Display(Name = "Order")]
        Order = 4,
        [Display(Name = "User")]
        User = 5,

    }
}