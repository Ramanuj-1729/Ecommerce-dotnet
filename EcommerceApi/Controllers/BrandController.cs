using EcommerceApi.Models;
using EcommerceApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly BrandService _brandServices;
        public BrandController(BrandService brandServices)
        {
            _brandServices = brandServices;
        }

        [HttpGet]
        //[Authorize] // Requires authentication
        [ProducesResponseType(typeof(object), 200)] // Successful response
        [ProducesResponseType(401)] // Unauthorized response
        [ProducesResponseType(500)] // Server error response
        public async Task<IActionResult> GetBrands()
        {
            try
            {
                List<Brand> res = await _brandServices.GetAllBrands();
                return res == null ? NotFound("Sorry no brands found") : Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        //[Authorize(Roles = "admin")] // Requires admin role
        [ProducesResponseType(typeof(object), 200)] // Successful response
        [ProducesResponseType(401)] // Unauthorized response
        [ProducesResponseType(500)] // Server error response
        public async Task<IActionResult> GetBrandById(int id)
        {
            try
            {
                Brand res = await _brandServices.GetBrandById(id);

                return res != null ? Ok(res) : NotFound($"Sorry no brand found with id:{id}");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost]
        //[Authorize(Roles = "admin")] // Requires admin role
        [ProducesResponseType(typeof(bool), 200)] // Successful response
        [ProducesResponseType(401)] // Unauthorized response
        [ProducesResponseType(500)] // Server error response
        public async Task<IActionResult> AddBrand([FromBody] Brand brandDto)
        {
            try
            {
                var isok = await _brandServices.CreateBrand(brandDto);
                return Ok("Brand Successfully Added!!");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut("{id}")]
        //[Authorize(Roles = "admin")] // Requires admin role
        [ProducesResponseType(200)] // Successful response
        [ProducesResponseType(401)] // Unauthorized response
        [ProducesResponseType(500)] // Server error response
        public async Task<IActionResult> UpdateBrand(int id, [FromBody] Brand brandDto)
        {
            try
            {
                await _brandServices.UpdateBrand(id, brandDto);
                return Ok("Brand Successfully Updated!!");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete("{id}")]
        //[Authorize(Roles = "admin")] // Requires admin role
        [ProducesResponseType(200)] // Successful response
        [ProducesResponseType(401)] // Unauthorized response
        [ProducesResponseType(500)] // Server error response
        public async Task<IActionResult> DeleteBrand(int id)
        {
            try
            {
                await _brandServices.DeleteBrand(id);
                return Ok("Brand Successfully Deleted!!");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
