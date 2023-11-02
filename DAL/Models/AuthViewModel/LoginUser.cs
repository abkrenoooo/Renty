using System.ComponentModel.DataAnnotations;

namespace Renty.Models.AuthModel
{
    public class LoginUser
    {
        [Required]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)] 
        public string Password { get; set; }  
    }
}
