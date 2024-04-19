using System.ComponentModel.DataAnnotations;

namespace EcommerceApi.Models
{
    public class Brand
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Brand name is required.")]
        [StringLength(100, ErrorMessage = "Brand name must be between {2} and {1} characters.", MinimumLength = 2)]
        public required string Name { get; set; }
    }
}
