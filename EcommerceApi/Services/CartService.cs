﻿using EcommerceApi.DTOs;
using EcommerceApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApi.Services
{
    public class CartService
    {
        private readonly AppDbContext _context;
        private readonly string _hostUrl;
        private readonly IConfiguration _configuration;
        private readonly JwtService _jwtService;

        public CartService(AppDbContext context, IConfiguration configuration, JwtService jwtService)
        {
            _context = context;
            _configuration = configuration;
            _jwtService = jwtService;
            _hostUrl = _configuration["HostUrl"];
        }

        public async Task<List<OutPutCartDTO>> GetCartItems(string token)
        {
            try
            {
                int userId = _jwtService.GetUserIdFromToken(token);


                if (userId == 0) throw new Exception("User with id doesn't exist !");

                var user = await _context.Cart
                    .Include(c => c.CartItems)
                    .ThenInclude(ci => ci.Product)
                    .FirstOrDefaultAsync(p => p.UserId == userId);
                if (user == null)
                {
                    return [];
                }

                if (user != null)
                {
                    var cartItems = user.CartItems.Select(ci => new OutPutCartDTO
                    {
                        Id = ci.ProductId,
                        ProductName = ci.Product.Name,
                        Quantity = ci.Quantity,
                        Price = ci.Product.Price,
                        TotalPrice = ci.Product.Price * ci.Quantity,
                        Image = _hostUrl + ci.Product.Image

                    }).ToList();

                    return cartItems;
                }
                return new List<OutPutCartDTO>();

            }

            catch (Exception ex)
            {
                throw new Exception("Something went wring while fetching cart items : 👉🏼 " + ex.Message);
            }
        }

        public async Task<bool> AddToCart(string token, int productId)
        {
            try
            {
                int userId = _jwtService.GetUserIdFromToken(token);

                if (userId == 0) throw new Exception($"User not valid witrh token: {token}");

                var user = await _context.Users
                    .Include(u => u.Cart)
                    .ThenInclude(c => c.CartItems)
                    .FirstOrDefaultAsync(u => u.Id == userId);

                var Product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);

                if (Product == null) throw new Exception($"Product with id {productId} not found");

                if (user != null && Product != null)
                {
                    //If user doesnt have a cart (empty cart)
                    if (user.Cart == null)
                    {
                        user.Cart = new Cart
                        {
                            UserId = userId,
                            CartItems = new List<CartItem>()
                        };

                        _context.Cart.Add(user.Cart);
                        await _context.SaveChangesAsync();

                    }


                }

                CartItem? existingCartProduct = user.Cart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);

                if (existingCartProduct != null)
                {
                    existingCartProduct.Quantity++;
                }
                else
                {
                    CartItem cartItem = new CartItem
                    {
                        CartId = user.Cart.Id,
                        ProductId = productId,
                        Quantity = 1,
                    };

                    _context.CartItems.Add(cartItem);

                }

                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error adding product to cart: {ex}");
                return false;

            }
        }

        public async Task<bool> AddToCartByQuantity(string token, int productId, int quantity)
        {
            try
            {
                int userId = _jwtService.GetUserIdFromToken(token);

                if (userId == 0) throw new Exception($"User not valid witrh token: {token}");

                var user = await _context.Users
                    .Include(u => u.Cart)
                    .ThenInclude(c => c.CartItems)
                    .FirstOrDefaultAsync(u => u.Id == userId);

                var Product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);

                if (Product == null) throw new Exception($"Product with id {productId} not found");

                if (user != null && Product != null)
                {
                    //If user doesnt have a cart (empty cart)
                    if (user.Cart == null)
                    {
                        user.Cart = new Cart
                        {
                            UserId = userId,
                            CartItems = new List<CartItem>()
                        };

                        _context.Cart.Add(user.Cart);
                        await _context.SaveChangesAsync();

                    }


                }

                CartItem? existingCartProduct = user.Cart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);

                if (existingCartProduct != null)
                {
                    existingCartProduct.Quantity = quantity;
                }
                else
                {
                    CartItem cartItem = new CartItem
                    {
                        CartId = user.Cart.Id,
                        ProductId = productId,
                        Quantity = quantity,
                    };

                    _context.CartItems.Add(cartItem);

                }

                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error adding product to cart: {ex}");
                return false;

            }
        }

        public async Task<bool> DeleteFromCart(string token, int ProductId)
        {
            try
            {
                int userId = _jwtService.GetUserIdFromToken(token);

                if (userId == 0) throw new Exception("User id is not valid !");

                var user = await _context.Users
                    .Include(u => u.Cart)
                    .ThenInclude(u => u.CartItems)
                    .FirstOrDefaultAsync(u => u.Id == userId);

                var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == ProductId);

                if (user != null && product != null)
                {
                    var item = user.Cart.CartItems.FirstOrDefault(ci => ci.ProductId == ProductId);

                    if (item != null)
                    {
                        _context.CartItems.Remove(item);
                        await _context.SaveChangesAsync();

                        return true;
                    }
                }

                return false;
                throw new Exception($"No User or Product presnt with given id , ProductId:{ProductId} !");

            }
            catch (Exception ex)
            {
                return false;
                throw new Exception("An exception occured while deleting a product from the users cart " + ex.Message);

            }
        }

        public async Task<bool> IncreaseQuantity(string token, int ProductId)
        {
            try
            {
                int userId = _jwtService.GetUserIdFromToken(token);

                if (userId == 0)
                    throw new Exception("A user with the current token is not found !");

                var user = await _context.Users
                    .Include(u => u.Cart)
                    .ThenInclude(c => c.CartItems)
                    .FirstOrDefaultAsync(u => u.Id == userId);


                var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == ProductId);


                if (user != null && product != null)
                {
                    var item = user.Cart.CartItems.FirstOrDefault(ci => ci.ProductId == ProductId);
                    if (item != null)
                    {
                        item.Quantity++;
                        await _context.SaveChangesAsync();


                        return true;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An exception occured while increasing the quantity of the product" + ex.Message);

            }
        }

        public async Task<bool> DecreaseQuantity(string token, int productId)
        {
            try
            {
                int userId = _jwtService.GetUserIdFromToken(token);

                if (userId == 0) throw new Exception($"User is not valid with token {token}");


                var user = await _context.Users
                    .Include(u => u.Cart)
                    .ThenInclude(c => c.CartItems)
                    .FirstOrDefaultAsync(u => u.Id == userId);
                var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);

                if (user != null && product != null)
                {
                    var item = user.Cart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);

                    if (item != null)
                    {
                        item.Quantity = item.Quantity > 1 ? --item.Quantity : item.Quantity;

                        if (item.Quantity == 0)
                        {
                            _context.CartItems.Remove(item);
                            await _context.SaveChangesAsync();
                        }

                        await _context.SaveChangesAsync();
                    }

                }

                return false;
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                throw;
            }
        }
    }
}
