using AutoMapper;
using EcommerceApi.DTOs;
using EcommerceApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApi.Services
{
    public class WishListService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;


        public WishListService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> AddToWishList(int userId, int productId)
        {
            try
            {
                var itemExists = await _context.WishLists.AnyAsync(w => w.UserId == userId && w.ProductId == productId);

                if (!itemExists)
                {
                    var wishListDTO = new WishListDTO
                    {
                        UserId = userId,
                        ProductId = productId
                    };

                    var wishList = _mapper.Map<WishList>(wishListDTO);

                    _context.WishLists.Add(wishList);

                    await _context.SaveChangesAsync();

                    return true;
                }
                else
                {
                    return false;

                }


            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding product to wishlist: {ex.Message}");
                return false;
            }
        }

        public async Task<List<WishList>> GetWishList(int userId)
        {
            try
            {
                var wishList = await _context.WishLists
                    .Include(w => w.Product)
                    //.ThenInclude(p => p.Category)
                    .Where(u => u.UserId == userId)
                    .ToListAsync();

                if (wishList.Count > 0)
                {
                    return wishList.ToList();
                }
                else
                {
                    return new List<WishList>();

                }

            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                return new List<WishList>();
            }
        }

        public async Task<bool> RemoveFromWishList(int productId)
        {
            try
            {
                var wishList = await _context.WishLists.FirstOrDefaultAsync(p => p.ProductId == productId);

                if (wishList != null)
                {
                    _context.WishLists.Remove(wishList);

                    await _context.SaveChangesAsync();

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error removing product from wishlist: {ex.Message}");
                return false;


            }
        }
    }
}
