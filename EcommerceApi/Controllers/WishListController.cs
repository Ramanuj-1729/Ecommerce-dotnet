using EcommerceApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishListController : ControllerBase
    {
        private readonly WishListService _wishListService;

        public WishListController(WishListService wishListService)
        {
            _wishListService = wishListService;
        }

        [HttpGet("get-wishlist")]
        //[Authorize] // Requires authentication
        [ProducesResponseType(typeof(object), 200)] // Successful response
        [ProducesResponseType(401)] // Unauthorized response
        [ProducesResponseType(500)] // Server error response
        public async Task<ActionResult> GetWishLists(int userId)
        {
            try
            {
                return Ok(await _wishListService.GetWishList(userId));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("add-wishlist")]
        //[Authorize] // Requires authentication
        [ProducesResponseType(200)] // Successful response
        [ProducesResponseType(400)] // Bad request response
        [ProducesResponseType(500)] // Server error response
        public async Task<ActionResult> AddWishList(int userId, int productId)
        {
            try
            {
                var isExist = await _wishListService.AddToWishList(userId, productId);
                if (!isExist)
                {
                    return BadRequest("Item already in the wish list");
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("remove-wishlist")]
        //[Authorize] // Requires authentication
        [ProducesResponseType(200)] // Successful response
        [ProducesResponseType(500)] // Server error response
        public async Task<ActionResult> RemoveFromWishList(int productId)
        {
            try
            {
                await _wishListService.RemoveFromWishList(productId);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
