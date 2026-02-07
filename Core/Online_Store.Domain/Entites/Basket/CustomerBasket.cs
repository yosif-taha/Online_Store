using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Store.Domain.Entites.Basket
{
    public class CustomerBasket
    {
        public string Id { get; set; }
        public IEnumerable<BasketItems> Items { get; set; }
        public int? DeliveryMethodId { get; set; }
        public string? PaymentIntentId { get; set; }
        public string? ClientSecret { get; set; }
        public decimal? ShippingCost { get; set; }
    }
}
