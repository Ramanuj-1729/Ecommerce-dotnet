using EcommerceApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly CartService _cartService;

        public CartController(CartService cartService, IConfiguration configuration)
        {
            _configuration = configuration;
            _cartService = cartService;
        }

        [HttpGet("GetCartItems")]
        [Authorize] // Requires authentication
        [ProducesResponseType(typeof(object), 200)] // Response type when successful
        [ProducesResponseType(401)] // Unauthorized response
        [ProducesResponseType(500)] // Server error response
        public async Task<ActionResult> GetCartItems()
        {
            try
            {
                // Extract JWT token from request header
                var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
                var splitToken = token.Split(' ');
                var jwtToken = splitToken[1];

                // Get cart items using the JWT token
                return Ok(await _cartService.GetCartItems(jwtToken));
            }
            catch (Exception ex)
            {
                // Return server error if an exception occurs
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("add-to-cart")]
        [Authorize] // Requires authentication
        [ProducesResponseType(200)] // Successful response
        [ProducesResponseType(500)] // Server error response
        public async Task<ActionResult> AddToCart(int productId)
        {
            try
            {
                // Extract JWT token from request header
                var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
                var splitToken = token.Split(' ');
                var jwtToken = splitToken[1];

                // Add product to the cart using the JWT token and product ID
                var isok = await _cartService.AddToCart(jwtToken, productId);
                return Ok(new {Message = "Prodcut Successfully added to cart"});
            }
            catch (Exception ex)
            {
                // Return server error if an exception occurs
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("add-to-cart-quantity")]
        [Authorize] // Requires authentication
        [ProducesResponseType(200)] // Successful response
        [ProducesResponseType(500)] // Server error response
        public async Task<ActionResult> AddToCartQuantity(int productId, int quantity)
        {
            try
            {
                // Extract JWT token from request header
                var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
                var splitToken = token.Split(' ');
                var jwtToken = splitToken[1];

                // Add product to the cart using the JWT token and product ID
                var isok = await _cartService.AddToCartByQuantity(jwtToken, productId, quantity);
                return Ok(new { Message = "Prodcut Successfully added to cart" });
            }
            catch (Exception ex)
            {
                // Return server error if an exception occurs
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("increment-quantity")]
        [Authorize] // Requires authentication
        [ProducesResponseType(200)] // Successful response
        [ProducesResponseType(500)] // Server error response
        public async Task<IActionResult> IncrementQuantity(int productId)
        {
            try
            {
                // Extract JWT token from request header
                var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
                var splitToken = token.Split(' ');
                var jwtToken = splitToken[1];

                // Increment quantity of the product in the cart
                bool res = await _cartService.IncreaseQuantity(jwtToken, productId);
                return res ? Ok(new { Quantity = "Cart Successfully Updated" }) : StatusCode(500, "An error occurred while incrementing quantity!");
            }
            catch (Exception e)
            {
                // Return server error if an exception occurs
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut("decrement-quantity")]
        [Authorize] // Requires authentication
        [ProducesResponseType(200)] // Successful response
        [ProducesResponseType(500)] // Server error response
        public async Task<IActionResult> DecrementQuantity(int productId)
        {
            try
            {

                // Extract JWT token from request header
                var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
                var splitToken = token.Split(' ');
                var jwtToken = splitToken[1];

                // Decrement quantity of the product in the cart
                await _cartService.DecreaseQuantity(jwtToken, productId);
                return Ok(new { Message = "Cart Successfully Updated" });
            }
            catch (Exception e)
            {
                // Return server error if an exception occurs
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete("remove-item-from-cart")]
        [Authorize] // Requires authentication
        [ProducesResponseType(typeof(bool), 200)] // Successful response
        [ProducesResponseType(500)] // Server error response
        public async Task<ActionResult> RemoveCartItem(int productId)
        {
            try
            {
                // Extract JWT token from request header
                var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
                var splitToken = token.Split(' ');
                var jwtToken = splitToken[1];

                // Remove the product from the cart
                bool res = await _cartService.DeleteFromCart(jwtToken, productId);
                return Ok(new { Message = "Product Successfully Removed from cart" });
            }
            catch (Exception e)
            {
                // Return server error if an exception occurs
                return StatusCode(500, e.Message);
            }
        }
    }
}
