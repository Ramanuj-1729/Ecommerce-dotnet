using Azure;
using System.Security.Claims;

namespace EcommerceApi.Services
{
    public class TokenService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TokenService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public void SetTokenCookie(string token)
        {
            //var cookieOptions = new CookieOptions
            //{
            //    HttpOnly = true, // This restricts the cookie to be accessed only through HTTP requests
            //    //Secure = true, // Send cookie only over HTTPS (recommended for secure tokens)
            //    //SameSite = SameSiteMode.Strict, // Protects against CSRF attacks (recommended)
            //    //Expires = DateTimeOffset.UtcNow.AddDays(1) // Set an expiration time if needed
            //};
            _httpContextAccessor.HttpContext.Response.Cookies.Append("X-Access-Token", token, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict });

            //_httpContextAccessor.HttpContext.Response.Cookies.Append("AuthToken", token, cookieOptions);
        }

        public string GetTokenCookie()
        {
            if(_httpContextAccessor.HttpContext.Request.Cookies["AuthToken"] == null)
            {
                return null;
            } else
            {
                return _httpContextAccessor.HttpContext.Request.Cookies["AuthToken"];
            }
        }

        public void RemoveTokenCookie()
        {
            _httpContextAccessor.HttpContext.Response.Cookies.Delete("AuthToken");
        }

        internal void SetTokenCookie(ClaimsPrincipal claimsPrincipal)
        {
            throw new NotImplementedException();
        }
    }
}
