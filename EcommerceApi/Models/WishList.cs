using System.ComponentModel.DataAnnotations;

namespace EcommerceApi.Models
{
    public class WishList
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "User ID is required.")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Product ID is required.")]
        public int ProductId { get; set; }

        public virtual required User User { get; set; }

        public virtual required Product Product { get; set; }
    }
}
