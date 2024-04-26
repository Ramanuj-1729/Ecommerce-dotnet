using Microsoft.EntityFrameworkCore;

namespace EcommerceApi.Models
{
    public class Address
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string phoneNumber { get; set; } = string.Empty;
        public string houseNumber { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string Zip { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public bool IsDefault { get; set; }
        public string AddressType { get; set; } = string.Empty;

        // user id unique false
        public int UserId { get; set; }
    }
}
