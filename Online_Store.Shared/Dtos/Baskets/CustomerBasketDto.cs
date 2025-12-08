using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Store.Shared.Dtos.Baskets
{
    public class CustomerBasketDto
    {
        public string Id { get; set; }
        public IEnumerable<BasketItemsDto> Items { get; set; }
    }
}
