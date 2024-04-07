using System.ComponentModel.DataAnnotations;

namespace EcommerceApi.DTOs
{
    public class WishListDTO
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        public int UserId { get; set; }
    }
}
