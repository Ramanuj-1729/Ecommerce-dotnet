//using AutoMapper;
using EcommerceApi.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Runtime.InteropServices;

namespace EcommerceApi.Services
{
    public class ProductService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        //private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string? _hostUrl;

        public ProductService(IConfiguration configuration, AppDbContext context, IWebHostEnvironment environment)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _context = context ?? throw new ArgumentNullException(nameof(context));
            //_mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _webHostEnvironment = environment ?? throw new ArgumentNullException(nameof(environment));
            _hostUrl = _configuration["HostUrl:url"];
        }


        public async Task<bool> CreateProduct(Product product, IFormFile image)
        {
            try
            {
                string? productImage = null;


                if (image != null && image.Length > 0)
                {

                    string filleName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);

                    //Getting file Path
                    string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "Images", "Products", filleName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await image.CopyToAsync(stream);
                    }

                    productImage = "/Images/Products/" + filleName;
                }
                else
                {
                    productImage = "/Images/Products/placeholder.jpg";
                }


                //var product = _mapper.Map<Models.ProductModels.Product>(productDTO);

                product.Image = productImage;

                await _context.Products.AddAsync(product);
                await _context.SaveChangesAsync();

                return true;

            }
            catch (Exception ex)
            {
                return false;
                throw new Exception("Error adding product: " + ex.Message);

            }
        }

        public async Task<List<Product>> GetProducts()
        {
            try
            {
                var products = await _context.Products.ToListAsync();
                
                if(products.Count > 0)
                {
                    return products;
                }
                return [];
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);throw new Exception(ex.Message);
            }
        }

        public async Task<Product> GetProductById(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            return product;
        }

        public async Task<bool> DeleteProduct(int id)
        {
            try
            {
                var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync($"An exception occured while removing product with id {id}" + ex.Message);
                return false;
            }
        }

        public async Task<bool> UpdateProduct(int id, Product productparam, IFormFile image)
        {
            try
            {
                var product = _context.Products.FirstOrDefault(p => p.Id == id);
                if (product != null)
                {
                    product.Name = productparam.Name;
                    product.Description = productparam.Description;
                    product.Price = productparam.Price;
                    product.Quantity = productparam.Quantity;
                    product.Rating = productparam.Rating;
                    product.CategoryId = productparam.CategoryId;


                    if (image != null && image.Length > 0)
                    {

                        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                        string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "Images", "Products", fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await image.CopyToAsync(stream);
                        }


                        product.Image = "/Images/Products/" + fileName;
                    }
                    else
                    {
                        product.Image = "Images/Products/PlaceholderImg/placeholder.jpg";
                    }


                    await _context.SaveChangesAsync();

                    return true;
                }
                else
                {
                    return false;
                    throw new InvalidOperationException($"Product with ID {id} not found.");
                }
            }
            catch (Exception ex)
            {
                return false;
                throw new Exception($"Error updating product with ID {id}: {ex.Message}", ex);
            }
        }

    }
}
