using EcommerceApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApi.Services
{
    public class AddressService
    {
        private readonly AppDbContext _context;

        public AddressService(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<bool> CreateAddress(Address address)
        {
            try
            {
                await _context.Addresses.AddAsync(address);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating address: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> UpdateAddress(int id, Address address)
        {
            try
            {
                Address? UpdateAddress = await _context.Addresses.FirstOrDefaultAsync(a => a.Id == id);

                if (UpdateAddress != null)
                {
                    UpdateAddress.Street = address.Street;
                    UpdateAddress.City = address.City;
                    UpdateAddress.State = address.State;
                    UpdateAddress.Zip = address.Zip;
                    UpdateAddress.Country = address.Country;
                    await _context.SaveChangesAsync();
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating address: {ex.Message}");
                return false;
            }
        }

        public async Task<Address> DeleteAddress(int id)
        {
            try
            {
                Address? addressToDelete = await _context.Addresses.FirstOrDefaultAsync(a => a.Id == id);


                if (addressToDelete != null)
                {
                    _context.Addresses.Remove(addressToDelete);
                    await _context.SaveChangesAsync();

                    return addressToDelete;
                }

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting address: {ex.Message}");
                return null;

            }
        }

        public async Task<List<Address>> GetAllAddresses()
        {
            try
            {
                var allAddresses = await _context.Addresses.ToListAsync();
                return allAddresses;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retreiving address: {ex.Message}");
                return null;
            }
        }

        public async Task<Address> GetAddressById(int id)
        {
            try
            {
                Address address = await _context.Addresses.FirstOrDefaultAsync(c => c.Id == id);
                return address;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retreiving address: {ex.Message}");
                return null;
            }
        }
    }
}
