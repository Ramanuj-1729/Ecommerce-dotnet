using EcommerceApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApi.Services
{
    public class CategoryService
    {
        private readonly AppDbContext _context;

        public CategoryService(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<bool> CreateCategory(Category category)
        {
            try
            {
                await _context.Categories.AddAsync(category);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating category: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> UpdateCategory(int id, Category category)
        {
            try
            {
                Category? UpdateCategory = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);

                if (UpdateCategory != null)
                {
                    UpdateCategory.Name = category.Name;
                    await _context.SaveChangesAsync();
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating category: {ex.Message}");
                return false;
            }
        }

        public async Task<Category> DeleteCategory(int id)
        {
            try
            {
                Category? categoryToDelete = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);


                if (categoryToDelete != null)
                {
                    _context.Categories.Remove(categoryToDelete);
                    await _context.SaveChangesAsync();

                    return categoryToDelete;
                }

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting category: {ex.Message}");
                return null;

            }
        }

        public async Task<List<Category>> GetAllCategories()
        {
            try
            {
                var allCategories = await _context.Categories.ToListAsync();
                return allCategories;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retreiving category: {ex.Message}");
                return null;
            }
        }

        public async Task<Category> GetCategoryById(int id)
        {
            try
            {
                Category category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
                return category;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retreiving category: {ex.Message}");
                return null;
            }
        }
    }
}
