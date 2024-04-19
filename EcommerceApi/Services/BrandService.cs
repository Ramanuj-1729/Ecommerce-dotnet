using EcommerceApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApi.Services
{
    public class BrandService
    {
        private readonly AppDbContext _context;

        public BrandService(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<bool> CreateBrand(Brand brand)
        {
            try
            {
                await _context.Brands.AddAsync(brand);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating brand: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> UpdateBrand(int id, Brand brand)
        {
            try
            {
                Brand? UpdateBrand = await _context.Brands.FirstOrDefaultAsync(c => c.Id == id);

                if (UpdateBrand != null)
                {
                    UpdateBrand.Name = brand.Name;
                    await _context.SaveChangesAsync();
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating brand: {ex.Message}");
                return false;
            }
        }

        public async Task<Brand> DeleteBrand(int id)
        {
            try
            {
                Brand? brandyToDelete = await _context.Brands.FirstOrDefaultAsync(b => b.Id == id);


                if (brandyToDelete != null)
                {
                    _context.Brands.Remove(brandyToDelete);
                    await _context.SaveChangesAsync();

                    return brandyToDelete;
                }

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting brans: {ex.Message}");
                return null;

            }
        }

        public async Task<List<Brand>> GetAllBrands()
        {
            try
            {
                var allBrands = await _context.Brands.ToListAsync();
                return allBrands;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retreiving brand: {ex.Message}");
                return null;
            }
        }

        public async Task<Brand> GetBrandById(int id)
        {
            try
            {
                Brand brand = await _context.Brands.FirstOrDefaultAsync(b => b.Id == id);
                return brand;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retreiving brand: {ex.Message}");
                return null;
            }
        }
    }
}
