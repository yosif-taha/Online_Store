using AutoMapper;
using Microsoft.Extensions.Configuration;
using Online_Store.Domain.Contracts;
using Online_Store.Domain.Entites.Orders;
using Online_Store.Domain.Entites.Products;
using Online_Store.Domain.Exeptions.NotFound;
using Online_Store.Services.Abstractions.Payment;
using Online_Store.Shared.Dtos.Baskets;
using Stripe;
using Stripe.V2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Product = Online_Store.Domain.Entites.Products.Product;

namespace Online_Store.Services.Payments
{
    public class PaymentService(IBasketReposatory _basketReposatory, IUnitOfWork _unitOfWorkc, IConfiguration _configuration, IMapper _mapper) : IPaymentService
    {
        public async Task<CustomerBasketDto> CreatePaymentIntentAsync(string basketId)
        {
            // Calculate Amount = Subtotal + DeliveryMethodPrice
            var basket = await _basketReposatory.GetBasketAsync(basketId);
            if (basket == null) throw new BasketNotFoungException(basketId);

            foreach (var item in basket.Items)
            {
                // Get the latest price from the product service
                var product = await _unitOfWorkc.GetRepository<int, Product>().GetAsync(item.Id);

                // Update the price in the basket
                item.Price = product.Price;
            }

            var subTotal = basket.Items.Sum(b => b.Price * b.Quantity);

            var deliveryMethod = await  _unitOfWorkc.GetRepository<int, DeliveryMethod>().GetAsync(basket.DeliveryMethodId.Value);
            var  amount = subTotal + deliveryMethod.Cost;
            basket.ShippingCost = deliveryMethod.Cost;
         
            // Send Amount to Stripe and get ClientSecret
            StripeConfiguration.ApiKey = _configuration["StripeOptions:Secretkey"];

            var paymentIntentService = new PaymentIntentService();
            PaymentIntent paymentIntent;
            if(basket.PaymentIntentId is null)
            {
                // Create
                var Options = new PaymentIntentCreateOptions()
                   {
                      Amount = (long)amount * 100,
                      Currency = "usd",
                      PaymentMethodTypes = new List<string>() {"card"}
                   };
              paymentIntent =  await paymentIntentService.CreateAsync(Options);
            }
            else
            {
                // Update
                var  Options = new PaymentIntentUpdateOptions()
                   {
                      Amount = (long)amount * 100
                  
                   };
                paymentIntent = await paymentIntentService.UpdateAsync(basket.PaymentIntentId,Options);
            }
           basket.PaymentIntentId = paymentIntent.Id;
           basket.ClientSecret = paymentIntent.ClientSecret;
    
            basket =  await _basketReposatory.CreateBasketAsync(basket,TimeSpan.FromDays(1));
            var basketdto = _mapper.Map<CustomerBasketDto>(basket);
            return basketdto;
        }
    }
}
