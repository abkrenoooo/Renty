using BLL.Services.IServices;
using BLL.Services.Services;
using DAL.Enum;
using DAL.Models.SuperAdmin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using DAL.Entities;

namespace Renty.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Server")]
    public class SuperAdminController : ControllerBase
    {
        #region Depend Injection
        private readonly ISuperAdminSevices _SuperAdminService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAuthorizationService _authorizationService;

        public SuperAdminController(ISuperAdminSevices SuperAdminService, UserManager<ApplicationUser> userManager, IAuthorizationService authorizationService)
        {
            _SuperAdminService = SuperAdminService;
            _userManager = userManager;
            _authorizationService = authorizationService;
        }
        #endregion

        #region Add SuperAdmin
        [HttpPost("Add SuperAdmin")]
        [Authorize(Roles = "Server")]
        public async Task<IActionResult> AddSuperAdminUserAsync(SuperAdminUserCreateVM model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _SuperAdminService.CreateSuperAdminUserAsync(model);
            return Ok(result);
        }
        #endregion

        #region Get SuperAdmin By Id
        [HttpGet("Get SuperAdmin By Id")]
        [Authorize(Roles = "Server")]

        public async Task<IActionResult> GetSuperAdminUserByIdAsync(string SuperAdminUserId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _SuperAdminService.GetSuperAdminUserByIdAsync(SuperAdminUserId);

            return Ok(result);
        }
        #endregion

        #region Get SuperAdmins
        [HttpGet("Get SuperAdmins")]
        [Authorize(Roles = "Server")]
        public async Task<IActionResult> GetSuperAdminUsersAsync(int paggingNumber)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _SuperAdminService.GetSuperAdminUsersAsync(paggingNumber);

            return Ok(result);
        }
        #endregion

        #region Edit SuperAdmin
        [HttpPut("Edit SuperAdmin")]
        [Authorize(Roles = "Server")]

        public async Task<IActionResult> EditSuperAdminUserAsync(SuperAdminUserVMEdit model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _SuperAdminService.EditSuperAdminUserAsync(model);

            return Ok(result);
        }
        #endregion

        #region Remove SuperAdmin
        [HttpDelete("Remove SuperAdmin")]
        [Authorize(Roles = "Server")]

        public async Task<IActionResult> RemoveSuperAdminUserAsync(string SuperAdminId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _SuperAdminService.RemoveSuperAdminUserAsync(SuperAdminId);

            return Ok(result);
        }
        #endregion

    }
}
