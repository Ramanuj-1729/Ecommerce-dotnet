namespace EcommerceApi.DTOs
{
    public class OrderDetailsDTO
    {
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public List<OrderDetailsProductDTO> Products { get; set; }
    }
}
