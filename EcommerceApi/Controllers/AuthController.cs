using EcommerceApi.DTOs;
using EcommerceApi.Models;
using EcommerceApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService service)
        {
            _authService = service;
        }

        [HttpPost("Register")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<ActionResult<string>> Register([FromBody] UserRegisterDTO user)
        {
            try
            {
                string message = await _authService.Register(user);
                return Ok(new { Token = message });
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = "Registration failed.", Error = e.Message });
            }
        }

        [HttpPost("Login")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(string), 401)]
        public async Task<ActionResult<string>> Login(UserLoginDTO user)
        {
            try
            {
                var token = await _authService.Login(user);
                return Ok(new { Token = token });
            }
            catch (Exception e)
            {
                return Unauthorized(new { Message = "Login failed.", Error = e.Message });
            }
        }
    }
}
