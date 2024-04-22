using EcommerceApi.Models;
using EcommerceApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryService _categoryServices;
        public CategoryController(CategoryService categoryServices)
        {
            _categoryServices = categoryServices;
        }

        [HttpGet]
        [ProducesResponseType(typeof(object), 200)] // Successful response
        [ProducesResponseType(401)] // Unauthorized response
        [ProducesResponseType(500)] // Server error response
        public async Task<IActionResult> GetCategories()
        {
            try
            {
                List<Category> res = await _categoryServices.GetAllCategories();
                return res == null ? NotFound("Sorry no categories found") : Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(object), 200)] // Successful response
        [ProducesResponseType(401)] // Unauthorized response
        [ProducesResponseType(500)] // Server error response
        public async Task<IActionResult> GetCategoryById(int id)
        {
            try
            {
                Category res = await _categoryServices.GetCategoryById(id);

                return res != null ? Ok(res) : NotFound($"Sorry no category found with id:{id}");
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
        public async Task<IActionResult> AddCategory([FromBody] Category categoryDto)
        {
            try
            {
                var isok = await _categoryServices.CreateCategory(categoryDto);
                return Ok("Category Successfully Added!!");
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
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] Category categoryDto)
        {
            try
            {
                await _categoryServices.UpdateCategory(id, categoryDto);
                return Ok("Category Successfully Updated!!");
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
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                await _categoryServices.DeleteCategory(id);
                return Ok("Category Successfully Deleted!!");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
