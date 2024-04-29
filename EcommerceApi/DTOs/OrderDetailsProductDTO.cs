namespace EcommerceApi.DTOs
{
    public class OrderDetailsProductDTO
    {
        public int ProductId { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
