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
                // get all addresses for the user and find the default address and set it to false before setting the new default address to true 
                if (address.IsDefault)
                {
                    var userAddresses = await _context.Addresses.Where(a => a.UserId == address.UserId).ToListAsync();
                    foreach (var userAddress in userAddresses)
                    {
                        if (userAddress.IsDefault)
                        {
                            userAddress.IsDefault = false;
                        }
                    }
                }
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
                    UpdateAddress.FullName = address.FullName;
                    UpdateAddress.PhoneNumber = address.PhoneNumber;
                    UpdateAddress.HouseNumber = address.HouseNumber;
                    UpdateAddress.Street = address.Street;
                    UpdateAddress.City = address.City;
                    UpdateAddress.State = address.State;
                    UpdateAddress.Zip = address.Zip;
                    UpdateAddress.Country = address.Country;

                    // get all addresses for the user and find the default address and set it to false before setting the new default address to true 
                    if (address.IsDefault)
                    {
                        var userAddresses = await _context.Addresses.Where(a => a.UserId == address.UserId).ToListAsync();
                        foreach (var userAddress in userAddresses)
                        {
                            if (userAddress.IsDefault)
                            {
                                userAddress.IsDefault = false;
                            }
                        }
                    }
                    UpdateAddress.IsDefault = address.IsDefault;
                    UpdateAddress.AddressType = address.AddressType;
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

        public async Task<List<Address>> GetAddressesById(int id)
        {
            try
            {
                var allAddresses = await _context.Addresses.Where(a => a.UserId == id).ToListAsync();
                return allAddresses;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retreiving addresses: {ex.Message}");
                return null;
            }
        }
    }
}
