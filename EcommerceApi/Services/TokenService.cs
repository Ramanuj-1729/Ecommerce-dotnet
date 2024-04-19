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
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true, // This restricts the cookie to be accessed only through HTTP requests
                                 // Other options you might consider:
                                 // Secure = true, // Send cookie only over HTTPS (recommended for secure tokens)
                                 // SameSite = SameSiteMode.Strict, // Protects against CSRF attacks (recommended)
                                 // Expires = DateTimeOffset.UtcNow.AddDays(1) // Set an expiration time if needed
            };

            _httpContextAccessor.HttpContext.Response.Cookies.Append("AuthToken", token, cookieOptions);
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
    }
}
