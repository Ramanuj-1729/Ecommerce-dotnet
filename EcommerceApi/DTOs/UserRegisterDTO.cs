using EcommerceApi.Models;
using System.ComponentModel.DataAnnotations;

namespace EcommerceApi.DTOs
{
    public class UserRegisterDTO
    {

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

    }
}
