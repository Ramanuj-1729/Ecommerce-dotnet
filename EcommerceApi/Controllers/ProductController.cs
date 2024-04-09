using EcommerceApi.Models;
using EcommerceApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;

        // Constructor to initialize dependencies
        public ProductController(ProductService productService, IWebHostEnvironment webHostEnvironment)
        {
            _productService = productService;
        }
        [HttpPost]
        //[Authorize(Roles = "admin")] // Requires admin role
        //[ProducesResponseType(200)] // Successful response
        //[ProducesResponseType(500)] // Server error response
        public async Task<IActionResult> AddProduct([FromBody] Product productDto, IFormFile image)
        {
            try
            {
                // Add product
                var res = await _productService.CreateProduct(productDto, image);
                return res ? Ok("Product created successfully!") : StatusCode(500, "Error while creating a new product!");
            }
            catch (Exception e)
            {
                // Return server error if an exception occurs
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        //[Authorize] // Requires authentication
        //[ProducesResponseType(typeof(object), 200)] // Successful response
        //[ProducesResponseType(401)] // Unauthorized response
        //[ProducesResponseType(500)] // Server error response
        public async Task<IActionResult> GetProducts()
        {
            try
            {
                // Retrieve all products
                var products = await _productService.GetProducts();
                return Ok(products);
            }
            catch (Exception ex)
            {
                // Return server error if an exception occurs
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetProdectById(int id)
        {
            try
            {
                var product = await _productService.GetProductById(id);
                return Ok(product);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete("{id}")]
        //[Authorize(Roles = "admin")] // Requires admin role
        [ProducesResponseType(200)] // Successful response
        [ProducesResponseType(500)] // Server error response
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                bool res = await _productService.DeleteProduct(id);
                return res ? Ok("Product Successfully Deleted!!") : StatusCode(500, "An error occurred while deleting product!");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] Product product, IFormFile image)
        {
            try
            {
                bool res = await _productService.UpdateProduct(id, product, image);
                return res ? Ok("Product Successfully Updated!!") : StatusCode(500, "An error occurred while updating product!");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
