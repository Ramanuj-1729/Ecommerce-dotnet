using EcommerceApi.Models;
using EcommerceApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly AddressService _addressServices;
        public AddressController(AddressService categoryServices)
        {
            _addressServices = categoryServices;
        }

        [HttpGet]
        [Authorize] // Requires authentication
        [ProducesResponseType(typeof(object), 200)] // Successful response
        [ProducesResponseType(401)] // Unauthorized response
        [ProducesResponseType(500)] // Server error response
        public async Task<IActionResult> GetAddresses()
        {
            try
            {
                List<Address> res = await _addressServices.GetAllAddresses();
                return res == null ? NotFound("Sorry no addresses found") : Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        [Authorize] // (Roles = "admin") Requires admin role
        [ProducesResponseType(typeof(object), 200)] // Successful response
        [ProducesResponseType(401)] // Unauthorized response
        [ProducesResponseType(500)] // Server error response
        public async Task<IActionResult> GetAddressById(int id)
        {
            try
            {
                List<Address> res = await _addressServices.GetAddressesById(id);

                return res != null ? Ok(res) : NotFound($"Sorry no addresses found with id:{id}");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost]
        [Authorize] // (Roles = "admin") Requires admin role
        [ProducesResponseType(typeof(bool), 200)] // Successful response
        [ProducesResponseType(401)] // Unauthorized response
        [ProducesResponseType(500)] // Server error response
        public async Task<IActionResult> AddAddress([FromBody] Address address)
        {
            try
            {
                var isok = await _addressServices.CreateAddress(address);
                return Ok("Address Successfully Added!!");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize] // (Roles = "admin") Requires admin role
        [ProducesResponseType(200)] // Successful response
        [ProducesResponseType(401)] // Unauthorized response
        [ProducesResponseType(500)] // Server error response
        public async Task<IActionResult> UpdateAddress(int id, [FromBody] Address address)
        {
            try
            {
                await _addressServices.UpdateAddress(id, address);
                return Ok("Address Successfully Updated!!");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize] // (Roles = "admin") Requires admin role
        [ProducesResponseType(200)] // Successful response
        [ProducesResponseType(401)] // Unauthorized response
        [ProducesResponseType(500)] // Server error response
        public async Task<IActionResult> DeleteAddress(int id)
        {
            try
            {
                await _addressServices.DeleteAddress(id);
                return Ok("Address Successfully Deleted!!");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
