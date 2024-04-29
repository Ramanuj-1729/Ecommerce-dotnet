using Stripe;
using Stripe.Checkout;
using Stripe.Climate;

namespace EcommerceApi.Services
{
    public class PaymentService
    {
        private readonly CartService _cartService;
        private readonly AuthService _authService;
        private readonly OrderService _orderService;
        private readonly IConfiguration _configuration;

        const string secret = "whsec_1fb6bb64cd9c37d4385a76b62ffcbcad08aa8e68a8a9d4a7ec1d69a79ff4c907";
        

        public PaymentService(CartService cartService, AuthService authService, OrderService orderService, IConfiguration configuration)
        {
            _cartService = cartService;
            _authService = authService;
            _orderService = orderService;
            _configuration = configuration;

            StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];
        }

        public async Task<Session> CreateCheckoutSession(string token)
        {

            var cart = await _cartService.GetCartItems(token);
            var lineItems = new List<SessionLineItemOptions>();

            cart.ForEach(product => lineItems.Add(new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    UnitAmountDecimal = product.Price * 100,
                    Currency = "inr",
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = product.ProductName,
                        Images = new List<string> { "https://lazeapostolski.com/sophia-ecommerce/assets/images/product/small/headphone1/headphone1.png" }
                    },
                },
                Quantity = 1,
            }));

            var options = new SessionCreateOptions
            {
                CustomerEmail = _authService.GetUserEmail(),
                PaymentMethodTypes = new List<string>
                {
                    "card"
                },
                LineItems = lineItems,
                Mode = "payment",
                SuccessUrl = "http://localhost:4200/order-success",
                CancelUrl = "http://localhost:4200/cart"
            };

            var service = new SessionService();
            Session session = service.Create(options);
            return session;
        }

        public async Task<bool> FulfillOrder(int userId)
        {
            //var json = await new StreamReader(request.Body).ReadToEndAsync();
            try
            {
                //var stripeEvent = EventUtility.ConstructEvent(
                //        json,
                //        request.Headers["Stripe-Signature"],
                //        secret
                //    );

                //if (stripeEvent.Type == Events.CheckoutSessionCompleted)
                //{
                //    dynamic session = (Stripe.Checkout.Session) stripeEvent.Data.Object;
                //    var user = await _authService.GetUserByEmail(session.customerEmail);
                //    await _orderService.PlaceOrder(user.Id);
                //}

                //dynamic session = (Stripe.Checkout.Session) stripeEvent.Data.Object;
                //var user = await _authService.GetUserByEmail(session.customerEmail);
                //int userId = _authService.GetUserId();
                await _orderService.PlaceOrder(userId);

                return true;
            }
            catch (StripeException e)
            {
                return false;
                throw new Exception("An exception ocuured while placing the oredr :" + e.Message);
            }
        }
    }
}
