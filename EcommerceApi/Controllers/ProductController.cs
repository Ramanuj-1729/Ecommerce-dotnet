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
        public async Task<IActionResult> AddProduct([FromForm] Product productDto, IFormFile image, IFormFileCollection images)
        {
            try
            {
                // Add product
                var res = await _productService.CreateProduct(productDto, image, images);
                return res ? Ok("Product created successfully!") : StatusCode(500, "Error while creating a new product!");
            }
            catch (Exception e)
            {
                // Return server error if an exception occurs
                return StatusCode(500, e.Message);
            }
        }

        //[HttpGet("images")]
        //public async Task<IActionResult> GetProductImages(int id)
        //{
        //    try
        //    {
        //        // Retrieve all products
        //        var images = await _productService.GetProductImages(id);
        //        return Ok(images);
        //    }
        //    catch (Exception ex)
        //    {
        //        // Return server error if an exception occurs
        //        return StatusCode(500, ex.Message);
        //    }
        //}

        [HttpGet]
        // [Authorize] // Requires authentication
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

        [HttpGet("Flag")]
        public async Task<IActionResult> GetProductsByFlag(string flag)
        {
            try
            {
                var products = await _productService.GetProductsByFlag(flag);
                return Ok(products);
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
        public async Task<IActionResult> UpdateProduct(int id, [FromForm] Product product, IFormFile image, IFormFileCollection images)
        {
            try
            {
                bool res = await _productService.UpdateProduct(id, product, image, images);
                return res ? Ok("Product Successfully Updated!!") : StatusCode(500, "An error occurred while updating product!");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}