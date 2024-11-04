using BoreWellManager.Business.Operations.User;
using BoreWellManager.Business.Operations.User.Dtos;
using BoreWellManager.WebApi.Jwt;
using BoreWellManager.WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace BoreWellManager.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        public AuthController(IUserService userService)
        {
            _userService = userService; 
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var addUser = new AddUserDto {
             TC=request.TC,
             Name=request.Name,
             Phone=request.Phone,
             Adress=request.Adress,
             UserType=request.UserType,
             IsResponsible=request.IsResponsible
            };
            var result = await _userService.AddUser(addUser);

            if (result.IsSucceed)
            {
                return Ok();
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult>Login(LoginRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result =  await _userService.LoginUser(new LoginUserDto {
               Name=request.Name,
               TC=request.TC
            });
            

            if(!result.IsSucceed)
                return BadRequest(result.Message);

           var user = result.Data;
            var config = HttpContext.RequestServices.GetRequiredService<IConfiguration>();
            var token = JwtHelper.GenerateJwtToken(new JwtDto
            {
                Id=user.Id,
                Name=user.Name,
                Phone=user.Phone,
                UserType = user.UserType,
                IsResponsible=user.IsResponsible,
                SecretKey= config["Jwt:SecretKey"]!,
                Issuer = config["Jwt:Issuer"]!,
                Audience= config["Jwt:Audience"]!,
                ExpireMinutes = int.Parse(config["Jwt:ExpireMinutes"]!)

            });

            return Ok(new LoginResponse
            {
                Message="Giriş başarıyla gerçekleştirildi",
                Token = token
            });
        }
    }
}
