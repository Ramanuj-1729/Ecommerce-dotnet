using System.ComponentModel.DataAnnotations;

namespace EcommerceApi.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Product name is required.")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "Product description is required.")]
        public string Description { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Price must be greater than or equal to 0.")]
        public int Price { get; set; }

        //[Required(ErrorMessage = "Image URL is required.")]
        //[Url(ErrorMessage = "Invalid URL format.")]
        public required string Image { get; set; }

        public List<ProductImage> Images { get; set; }

        public int Quantity { get; set; }

        [Range(0, 5, ErrorMessage = "Rating must be between 0 and 5.")]
        public int Rating { get; set; }

        [Required(ErrorMessage = "Category ID is required.")]
        public int CategoryId { get; set; }

        public bool IsFeatured { get; set; }

        public bool IsNew { get; set; }

        public bool IsOnSale { get; set; }

        public int Discount { get; set; }

        //public virtual ProductImage ProductImages { get; set; }




        // Navigation property to represent the category associated with this product
        //public virtual Category Category { get; set; }

        // Navigation property to represent the cart items associated with this product
        //public virtual List<CartItem> CartItems { get; set; }
    }
}