﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace EcommerceApi.Models
{
    public class OutPutCart
    {
        public int ProductId { get; set; }

        [DisplayName("Product Name")]
        public string ProductName { get; set; }

        [DisplayName("Price")]
        public decimal Price { get; set; }

        [DisplayName("Image URL")]
        public string Image { get; set; }

        [DisplayName("Total Price")]
        public string TotalPrice { get; set; }

        [DisplayName("Quantity")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be a positive number.")]
        public int Quantity { get; set; }
    }
}
