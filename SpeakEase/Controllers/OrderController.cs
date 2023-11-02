using BLL.Services.IServices;
using DAL.Entities;
using DAL.Enum;
using DAL.Models.OrderViewModel;
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
    public class OrderController : ControllerBase
    {
        #region Depend Injection
        private readonly IOrderServices _OrderServices;
        private readonly UserManager<ApplicationUser> _userManager;
        public OrderController(IOrderServices OrderServices, UserManager<ApplicationUser> userManager)
        {
            _OrderServices = OrderServices;
            _userManager = userManager;
        }
        #endregion

        #region Create  
        [HttpPost("Create Order")]
        [Authorize(Roles = "Server,Admin,SuperAdmin,User")]
        public async Task<IActionResult> Create_OrderAsync(OrderVMCreate Order)
        {
            var id = User.FindFirstValue("uid");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _OrderServices.Create_OrderAsync(Order, id);
            return Ok(result);
        }
        #endregion

        #region Action Order Request
        [HttpPost("Action of Order Request")]
        [Authorize(Roles = "Server,Admin,SuperAdmin,User")]

        public async Task<IActionResult> Get_ActionOrderRequestAsync(ActionOrderRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _OrderServices.Get_ActionOrderRequestAsync(request);

            return Ok(result);
        }
        #endregion

        #region Get Order For House
        [HttpGet("Get Orders For House")]
        [Authorize(Roles = "Server,Admin,SuperAdmin,User")]

        public async Task<IActionResult> Get_OrderForHouseAsync(int houseId)
        {
            var userId = User.FindFirstValue("uid");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _OrderServices.Get_OrderForHouseAsync(houseId, userId);

            return Ok(result);
        }
        #endregion

        #region Get 
        [HttpGet("Get Order By Id")]
        [Authorize(Roles = "Server,Admin,SuperAdmin,User")]

        public async Task<IActionResult> GetOrderByIdAsync(int OrderId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _OrderServices.GetOrderAsync(OrderId);

            return Ok(result);
        }
        #endregion

        #region Get All For User
        [HttpGet("Get All Orders For User")]
        [Authorize(Roles = "Server,Admin,SuperAdmin,User")]

        public async Task<IActionResult> GetAllOrderForUserAsync(int paggingNumber)
        {
            var id = User.FindFirstValue("uid");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _OrderServices.GetAll_OrderForUserAsync(paggingNumber, id);

            return Ok(result);
        }
        #endregion

        #region Get All
        [HttpGet("Get All Orders")]
        [Authorize(Roles = "Server,Admin,SuperAdmin")]

        public async Task<IActionResult> GetAllOrderAsync(int paggingNumber)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _OrderServices.GetAllOrderAsync(paggingNumber);

            return Ok(result);
        }
        #endregion

        #region Update  
        [HttpPut("Edit Order")]
        [Authorize(Roles = "Server,Admin,SuperAdmin,User")]
        public async Task<IActionResult> EditOrderAsync(OrderVMEdit Order)
        {
            var id = User.FindFirstValue("uid");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _OrderServices.EditOrderAsync(Order, id);
            return Ok(result);
        }
        #endregion

        #region Action Order Request
        [HttpPost("Edit Action of Order Request")]
        [Authorize(Roles = "Server,Admin,SuperAdmin,User")]

        public async Task<IActionResult> Edit_ActionOrderRequestAsync(ActionOrderRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _OrderServices.Edit_ActionOrderRequestAsync(request);

            return Ok(result);
        }
        #endregion

        #region Delete 
        [HttpDelete("Remove Order")]
        [Authorize(Roles = "Server,Admin,SuperAdmin,User")]
        public async Task<IActionResult> RemoveOrderAsync(int OrderId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _OrderServices.DeleteOrderAsync(OrderId);

            return Ok(result);
        }
        #endregion
    }
}
