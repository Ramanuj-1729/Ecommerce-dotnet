﻿using System.ComponentModel.DataAnnotations;

namespace EcommerceApi.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Category name is required.")]
        [StringLength(100, ErrorMessage = "Category name must be between {2} and {1} characters.", MinimumLength = 2)]
        public required string Name { get; set; }

        // Navigation property to represent products belonging to this category
        //public virtual List<Product> Products { get; set; }
    }
}
