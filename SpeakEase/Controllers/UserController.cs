using BLL.Services.IServices;
using BLL.Services.Services;
using DAL.Enum;
using DAL.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using DAL.Entities;

namespace Renty.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Server,User")]
    public class UserController : ControllerBase
    {
        #region Depend Injection
        private readonly IUserSevices _UserService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAuthorizationService _authorizationService;

        public UserController(IUserSevices UserService, UserManager<ApplicationUser> userManager, IAuthorizationService authorizationService)
        {
            _UserService = UserService;
            _userManager = userManager;
            _authorizationService = authorizationService;
        }
        #endregion

        #region Get User By Id
        [HttpGet("Get User By Id")]
        [Authorize(Roles = "Server,User")]

        public async Task<IActionResult> GetUserUserByIdAsync(string UserUserId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _UserService.GetUserUserByIdAsync(UserUserId);

            return Ok(result);
        }
        #endregion

        #region Get Users
        [HttpGet("Get Users")]
        [Authorize(Roles = "Server,User")]
        public async Task<IActionResult> GetUserUsersAsync(int paggingNumber)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _UserService.GetUserUsersAsync(paggingNumber);

            return Ok(result);
        }
        #endregion

        #region Edit User
        [HttpPut("Edit User")]
        [Authorize(Roles = "Server,User")]

        public async Task<IActionResult> EditUserUserAsync(UserVMEdit model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _UserService.EditUserUserAsync(model);

            return Ok(result);
        }
        #endregion

        #region Remove User
        [HttpDelete("Remove User")]
        [Authorize(Roles = "Server,User")]

        public async Task<IActionResult> RemoveUserUserAsync(string UserId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _UserService.RemoveUserUserAsync(UserId);

            return Ok(result);
        }
        #endregion

    }
}
