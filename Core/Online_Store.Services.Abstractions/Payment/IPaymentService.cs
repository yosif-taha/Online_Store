using Online_Store.Shared.Dtos.Baskets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Store.Services.Abstractions.Payment
{
    public interface IPaymentService
    {
       Task<CustomerBasketDto> CreatePaymentIntentAsync(string basketId);
    }
}
