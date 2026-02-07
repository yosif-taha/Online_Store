using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Online_Store.Shared.Dtos.Baskets
{
    public class CustomerBasketDto
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("items")]
        public IEnumerable<BasketItemsDto> Items { get; set; }
        public int? DeliveryMethodId { get; set; }
        public string? PaymentIntentId { get; set; }
        public string? ClientSecret { get; set; }
        public decimal? ShippingCost { get; set; }
    }
}
