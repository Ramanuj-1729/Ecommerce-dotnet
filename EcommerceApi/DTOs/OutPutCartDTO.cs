using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace EcommerceApi.DTOs
{
    public class OutPutCartDTO
    {
        public int Id { get; set; }

        [DisplayName("Product Name")]
        public string ProductName { get; set; }

        [DisplayName("Price")]
        public decimal Price { get; set; }

        [DisplayName("Image URL")]
        public string Image { get; set; }

        [DisplayName("Total Price")]
        public decimal TotalPrice { get; set; }

        [DisplayName("Quantity")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be a positive number.")]
        public int Quantity { get; set; }
    }
}
