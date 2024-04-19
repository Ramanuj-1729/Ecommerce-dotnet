//using AutoMapper;
using AutoMapper;
using EcommerceApi.DTOs;
using EcommerceApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EcommerceApi.Services
{
    public class AuthService
    {
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public AuthService(AppDbContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<string> Register(UserRegisterDTO user)
        {
            try
            {
                if (user == null)
                    throw new ArgumentNullException(nameof(user), "User data cannot be null.");


                var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
                if (existingUser != null)
                    throw new InvalidOperationException("User with the same email already exists.");


                string salt = BCrypt.Net.BCrypt.GenerateSalt(12);
                string hashPassword = BCrypt.Net.BCrypt.HashPassword(user.Password, salt);

                var userEntity = _mapper.Map<User>(user);
                userEntity.Password = HashPassword(user.Password, salt);
                _context.Users.Add(userEntity);
                await _context.SaveChangesAsync();

                return "User registered successfully !";
            }
            catch (Exception ex)
            {
                return $"Failed to register user ,Please try again! {ex.Message}";
                throw;
            }
        }

        public async Task<string> Login(UserLoginDTO user)
        {

            var userEntity = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email);

            if (userEntity == null || !validatePassword(user.Password, userEntity.Password))
            {
                throw new InvalidOperationException("Invalid email or password");
            }

            var token = GenerateJwtToken(userEntity);

            return token;
        }

        private string GenerateJwtToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
            var credentails = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier ,user.Id.ToString()),
                new Claim(ClaimTypes.Name,user.UserName ),
                new Claim(ClaimTypes.Role,user.Role),
                new Claim(ClaimTypes.Email,user.Email),
            };

            var token = new JwtSecurityToken(
                    claims: claims,
                    signingCredentials: credentails,
                    expires: DateTime.UtcNow.AddHours(2)
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string HashPassword(string password, string salt)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, salt);
        }

        //Function to verify the password when login in.
        private bool validatePassword(string password, string hashPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashPassword);
        }
    }
}
