using EcommerceApi.DTOs;
using EcommerceApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApi.Services
{
    public class OrderService
    {
        private readonly AppDbContext _context;
        private readonly AuthService _authService;
        private readonly CartService _cartService;
        private readonly TokenService _tokenService;

        public OrderService(AppDbContext context, AuthService authService, CartService cartService, TokenService tokenService)
        {
            _context = context;
            _authService = authService;
            _cartService = cartService;
            _tokenService = tokenService;
        }

        public async Task<bool> PlaceOrder(int userId)
        {
            try
            {
                string token = _tokenService.GetTokenCookie();
                if (userId == 0)
                {
                    throw new Exception($"User id not valid with token {token}");
                }

                var cart = await _context.Cart.Include(c => c.CartItems).ThenInclude(ci => ci.Product).FirstOrDefaultAsync(u => u.UserId == userId);

                var order = new Order
                {
                    UserId = userId,
                    OrderDate = DateTime.Now,
                    OrderStatus = "Pending",
                    OrderItems = cart.CartItems.Select(ci => new OrderItem
                    {
                        ProductId = ci.ProductId,
                        OrderId = cart.Id,
                        Quantity = ci.Quantity,
                        TotalPrice = ci.Product.Price * ci.Quantity
                    }).ToList()

                };

                _context.Orders.Add(order);
                _context.Cart.Remove(cart);
                await _context.SaveChangesAsync();
                return true;
            }

            catch (Exception ex)
            {
                return false;
                throw new Exception("An exception ocuured while placing the oredr :" + ex.Message);

            }
        }
    }
}
