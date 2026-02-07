using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Online_Store.Services.Abstractions;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Store.Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentsController(IServicesManager _servicesManager) : ControllerBase
    {

        [HttpPost("{basketId}")] //Post: baseUrl/api/Payment
        [Authorize]
        public async Task<IActionResult> CreateOrUpdatePaymentIntent(string basketId)
        {
            var result = await _servicesManager.PaymentServices.CreatePaymentIntentAsync(basketId);
            return Ok(result);
        }


        // stripe listen --forward-to https://localhost:7103/api/payments/webhook
        [Route("webhook")]
        [HttpPost]
        public async Task<IActionResult> Index()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            const string endpointSecret = "whsec_b8fff422fab59187b672f0700488053a26843c2a3da90ebd5008f3507257d0cc";

            var stripeEvent = EventUtility.ParseEvent(json);
            var signatureHeader = Request.Headers["Stripe-Signature"];

            stripeEvent = EventUtility.ConstructEvent(json,
                    signatureHeader, endpointSecret);

            // If on SDK version < 46, use class Events instead of EventTypes
            if (stripeEvent.Type == EventTypes.PaymentIntentSucceeded)
            {
                // Update order status to "Success"
            }
            else if (stripeEvent.Type == EventTypes.PaymentIntentPaymentFailed)
            {
                // Update order status to "Failed"
            }
            else
            {
                Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
            }
            return Ok();


        }
    }
}
