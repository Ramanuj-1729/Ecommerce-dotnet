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
        private readonly TokenService _tokenService;

        public AuthController(AuthService service, TokenService tokenService)
        {
            _authService = service;
            _tokenService = tokenService;
        }

        [HttpPost("Register")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<ActionResult<string>> Register([FromBody] UserRegisterDTO user)
        {
            try
            {
                string message = await _authService.Register(user);
                return Ok(new { message });
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
                _tokenService.SetTokenCookie(token);
                return Ok(new { message = "Login successful" });
            }
            catch (Exception e)
            {
                return Unauthorized(new { Message = "Login failed.", Error = e.Message });
            }
        }

        [HttpPost("Logout")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(string), 401)]
        public async Task<ActionResult<string>> Logout()
        {
            try
            {
                _tokenService.RemoveTokenCookie();
                return Ok(new { message = "Logout successful" });
            }
            catch (Exception e)
            {
                return Unauthorized(new { Message = "Logout failed.", Error = e.Message });
            }
        }

        [HttpGet("Token")]
        public async Task<ActionResult<string>> GetToken()
        {
            try
            {
                var token = _tokenService.GetTokenCookie();
                return Ok(token);
            }
            catch (Exception e)
            {
                return Unauthorized(new { Message = "Token not found.", Error = e.Message });
            }
        }
    }
}
