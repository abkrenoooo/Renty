using BLL.Services.IServices;
using BLL.Services.Services;
using DAL.Enum;
using DAL.Models.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using DAL.Entities;

namespace Renty.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Server,SuperAdmin")]
    public class AdminController : ControllerBase
    {
        #region Depend Injection
        private readonly IAdminSevices _adminService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAuthorizationService _authorizationService;

        public AdminController(IAdminSevices adminService, UserManager<ApplicationUser> userManager, IAuthorizationService authorizationService)
        {
            _adminService = adminService;
            _userManager = userManager;
            _authorizationService = authorizationService;
        }
        #endregion

        #region Add Admin
        [HttpPost("Add Admin")]
        [Authorize(Roles = "Server,SuperAdmin")]
        public async Task<IActionResult> AddAdminUserAsync(AdminUserCreateVM model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _adminService.CreateAdminUserAsync(model);
            return Ok(result);
        }
        #endregion

        #region Get Admin By Id
        [HttpGet("Get Admin By Id")]
        [Authorize(Roles = "Server,SuperAdmin")]

        public async Task<IActionResult> GetAdminUserByIdAsync(string AdminUserId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _adminService.GetAdminUserByIdAsync(AdminUserId);

            return Ok(result);
        }
        #endregion

        #region Get Admins
        [HttpGet("Get Admins")]
        [Authorize(Roles = "Server,SuperAdmin")]
        public async Task<IActionResult> GetAdminUsersAsync(int paggingNumber)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _adminService.GetAdminUsersAsync(paggingNumber);

            return Ok(result);
        }
        #endregion

        #region Edit Admin
        [HttpPut("Edit Admin")]
        [Authorize(Roles = "Server,SuperAdmin")]

        public async Task<IActionResult> EditAdminUserAsync(AdminUserVMEdit model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _adminService.EditAdminUserAsync(model);

            return Ok(result);
        }
        #endregion

        #region Remove Admin
        [HttpDelete("Remove Admin")]
        [Authorize(Roles = "Server,SuperAdmin")]

        public async Task<IActionResult> RemoveAdminUserAsync(string AdminId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _adminService.RemoveAdminUserAsync(AdminId);

            return Ok(result);
        }
        #endregion

    }
}
