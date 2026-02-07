using Online_Store.Domain.Entites.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Store.Services.Specifications.Orders
{
    public class OrderWithPaymentIntentSpecification : BaseSpecifications<Guid, Order>
    {
        public OrderWithPaymentIntentSpecification(string paymentIntentId) : base(o => o.PaymentIntentId == paymentIntentId)
        {
            
        }
    }
}
