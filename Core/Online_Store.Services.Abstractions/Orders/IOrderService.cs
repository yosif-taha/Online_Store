using Online_Store.Shared.Dtos.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Store.Services.Abstractions.Orders
{
    public interface IOrderService
    {
      Task<OrderResponse?>  CreateOrderAsync(OrderRequest request,string userEmail);
      Task<IEnumerable<DeliveryMethodDto>> GetDeliveryMethodsAsync();
      Task<OrderResponse?> GetOrderForSpecificUserAsync(Guid id,string userEmail);
      Task<IEnumerable<OrderResponse>> GetOrdersForSpecificUserAsync(string userEmail);
    }
}
