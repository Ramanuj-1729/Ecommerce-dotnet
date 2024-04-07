﻿using System.ComponentModel.DataAnnotations;

namespace EcommerceApi.DTOs
{
    public class UserLoginDTO
    {
        [Required]
        public required string Email { get; set; }

        [Required]
        public required string Password { get; set; }

    }
}
