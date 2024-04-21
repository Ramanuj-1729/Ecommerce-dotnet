using System.ComponentModel.DataAnnotations;

namespace EcommerceApi.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "User name is required.")]
        [StringLength(50, ErrorMessage = "User name should not exceed 50 characters.")]
        public required string UserName { get; set; }

        [Required(ErrorMessage = "FullName is required.")]
        [StringLength(50, ErrorMessage = "Full name should not exceed 50 characters.")]
        public required string FullName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
        public required string Password { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        public required string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Age is required.")]
        public int Age { get; set; }

        //M=Male, F=Female
        [Required(ErrorMessage = "Gender is required.")]
        public char Gender { get; set; }

        //User Role
        public required string Role { get; set; } = "user";

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public virtual Address Address { get; set; }

        public virtual Cart Cart { get; set; }

        // Navigation properties
        //public virtual List<Order> Orders { get; set; }
        //public virtual List<WishList> WishLists { get; set; }
    }
}
