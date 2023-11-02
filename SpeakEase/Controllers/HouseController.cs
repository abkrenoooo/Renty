using BLL.Services.IServices;
using DAL.Entities;
using DAL.Enum;
using DAL.Models.HouseViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Renty.Controllers
{
    [Authorize(Roles = "Server,Admin,SuperAdmin,User")]
    [Route("api/[controller]")]
    [ApiController]
    public class HouseController : ControllerBase
    {

        #region Depend Injection
        private readonly IHouseServices _HouseServices;
        private readonly UserManager<ApplicationUser> _userManager;
        public HouseController(IHouseServices HouseServices, UserManager<ApplicationUser> userManager)
        {
            _HouseServices = HouseServices;
            _userManager = userManager;
        }
        #endregion

        #region Create  
        [HttpPost("Create House")]
        [Authorize(Roles = "Server,Admin,SuperAdmin,User")]
        public async Task<IActionResult> Create_HouseAsync([FromForm] HouseVMCreate House)
        {
            var id = User.FindFirstValue("uid");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _HouseServices.Create_HouseAsync(House, id);
            return Ok(result);
        }
        #endregion

        #region Get 
        [HttpGet ("Get House By Id")]
        [Authorize(Roles = "Server,Admin,SuperAdmin,User")]

        public async Task<IActionResult> GetHouseByIdAsync(int HouseId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _HouseServices.GetHouseAsync(HouseId);

            return Ok(result);
        }
        #endregion

        #region Get All For User
        [HttpGet("Get All Houses For User")]
        [Authorize(Roles = "Server,Admin,SuperAdmin,User")]

        public async Task<IActionResult> GetAllHouseForUserAsync(int paggingNumber)
        {
            var id = User.FindFirstValue("uid");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _HouseServices.GetAll_HouseForUserAsync(paggingNumber,id);

            return Ok(result);
        }
        #endregion

        #region Get All
        [HttpGet("Get All Houses")]
        [Authorize(Roles = "Server,Admin,SuperAdmin,User")]

        public async Task<IActionResult> GetAllHouseAsync(int paggingNumber)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _HouseServices.GetAllHouseAsync(paggingNumber);

            return Ok(result);
        }
        #endregion

        #region Update  
        [HttpPut("Edit House")]
        [Authorize(Roles = "Server,Admin,SuperAdmin,User")]
        public async Task<IActionResult> EditHouseAsync([FromForm] HouseVMEdit House)
        {
            var id = User.FindFirstValue("uid");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _HouseServices.EditHouseAsync(House,id);
            return Ok(result);
        }
        #endregion

        #region Delete 
        [HttpDelete("Remove House")]
        [Authorize(Roles = "Server,Admin,SuperAdmin,User")]
        public async Task<IActionResult> RemoveHouseAsync(int HouseId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _HouseServices.DeleteHouseAsync(HouseId);

            return Ok(result);
        }
        #endregion

        
    }
}
