using BLL.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using DAL.Entities;
using Renty.Models.AuthModel;


namespace Renty.Controllers
{
    [Route("api/")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        #region Depend Injecktion
        private readonly IAuthService userService;
        private readonly UserManager<ApplicationUser> _userManager;
        public AccountController(IAuthService userService,UserManager<ApplicationUser> userManager)
        {
                this.userService=userService;
                this._userManager=userManager;
        }
        #endregion

        #region Register
        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            var result= await userService.RegisterUserAsync(model); 
            
            return Ok(result);
        }
        #endregion

        #region Login
        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginUser loginUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            var result = await userService.LoginAsync(loginUser);
            return Ok(result);
        }
        #endregion

        //#region Update Profile
        //[HttpPost("Update/Profile")]
        //[Authorize]
        //public async Task<IActionResult> UpdateProfile(UpdateVM updeteUser)
        //{
        //    var userId = User.FindFirstValue("uid");
        //    var user = await _userManager.FindByIdAsync(userId);

        //    if (user == null)
        //    {
        //        return NotFound();
        //    }
        //    else
        //    {
        //        var result = await userService.UpdateAsync(updeteUser, user);
        //        return Ok(result);
        //    }
        //}
        //#endregion

        //#region UpdatePassword
        //[HttpPatch("Update/Password")]
        //[Authorize]
        //public async Task<IActionResult> UpdatePassword([FromBody] UpdatePassword dto)
        //{
        //    var userId = User.FindFirstValue("uid");
        //    var user = await _userManager.FindByIdAsync(userId);

        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    var result = await _userManager.ChangePasswordAsync(user, dto.OldPassword, dto.NewPassword);
        //    var studentRoles = await _userManager.GetRolesAsync(user);

        //    if (result.Succeeded)
        //    {
        //        return Ok(new AuthModel
        //        {
        //            Message="updated password",
        //            Email = user.Email,
        //            //ExpiresOn = jwtSecurityToken.ValidTo,
        //            IsAuthenticated = true,
        //            Roles = studentRoles.ToList(),
        //            //Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
        //            Username = user.UserName
        //        }); 
        //    }
        //    else
        //    {
        //        return BadRequest(result.Errors);
        //    }
        //}
        //#endregion

        //#region Get All Account Data
        //[HttpGet("profile")]
        //[Authorize]
        //public async Task<IActionResult> profile()
        //{
        //    var id = User.FindFirstValue("uid");
        //    var user= await _userManager.FindByIdAsync(id);
        //    if (user == null)
        //        return NotFound();
            

        //    return Ok(user);
        //}
        //#endregion

    }
}
