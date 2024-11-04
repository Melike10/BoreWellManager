using BoreWellManager.Business.Operations.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace BoreWellManager.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("/tc/{tc}")]
        public async Task<IActionResult> GetUserLands(string tc)
        {
            var userLands = await _userService.GetLands(tc);
            if(userLands is null)
                return NotFound();
            return Ok(userLands);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userService.GetUserById(id);
            if(user is null)
                return NotFound();
            return Ok(user);
        }

        [HttpGet]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> GetUsers()
        {
            var user = await _userService.GetUsers();
            if (user is null)
                return NotFound();
            return Ok(user);
        } 
        [HttpPatch("{tc}/adress")]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> ChangeAdress(string tc,string adress) {
            
            var res = await _userService.ChangeAdress(tc, adress);
            if(!res.IsSucceed)
                return BadRequest(res.Message);
            return Ok();
        }
        [HttpPatch("ToggleIsresponsible/{tc}")]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> ToggleIsResponsible(string tc)
        {
            var res = await _userService.ToggleIsResponsible(tc);
            if (!res.IsSucceed)
                return BadRequest(res.Message);
            return Ok();
        }

    }
}
