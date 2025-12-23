using AutoMapper;
using Online_Store.Domain.Contracts;
using Online_Store.Domain.Entites.Orders;
using Online_Store.Domain.Entites.Products;
using Online_Store.Domain.Exeptions.BadRequest;
using Online_Store.Domain.Exeptions.NotFound;
using Online_Store.Services.Abstractions.Orders;
using Online_Store.Services.Specifications.Orders;
using Online_Store.Shared.Dtos.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Online_Store.Services.Orders
{
    public class OrderService(IUnitOfWork _unitOfWork , IMapper _mapper ,IBasketReposatory _basketReposatory) : IOrderService
    {
        public async Task<OrderResponse?> CreateOrderAsync(OrderRequest request, string userEmail)
        {
            // 1. Address
            var address =  _mapper.Map<OrderAddress>(request.ShippingAddress);

            //2. Delivery Method
             var delivery = await _unitOfWork.GetRepository<int, DeliveryMethod>().GetAsync(request.DeliveryMethodId);
            if (delivery == null) throw new DeliveryMethodNotFoundException(request.DeliveryMethodId);
            // 3. Basket Items to Order Items

            var basketItem = await _basketReposatory.GetBasketAsync(request.BasketId);
            if (basketItem == null) throw new BasetNotFoungException(request.BasketId);

            var items = new List<OrderItem>();
            foreach (var item in basketItem.Items)
            {
                //check Price
                //get product from Db
               var product = await _unitOfWork.GetRepository<int, Online_Store.Domain.Entites.Products.Product>().GetAsync(item.Id);
               if(product ==null) throw new ProductNotFoundException(item.Id);

               if(product.Price != item.Price) item.Price = product.Price;


                var orderInItemProduct = new ProductInItemOrderd(item.Id,item.PrpductName,item.PictrulUrl);
                var orderItem = new OrderItem(orderInItemProduct, item.Quantity,item.Price);
                items.Add(orderItem);
            }

            // 4. Calculate Subtotal
            decimal subtotal = items.Sum(I => I.Price * I.Quentity);


            // 5. Create Order
            var order = new Order(userEmail,address,delivery, items, subtotal);

              await _unitOfWork.GetRepository<Guid,Order>().AddAsync(order);
            var count = await _unitOfWork.SaveChangeAsync();
            if (count <= 0) throw new CreateOrderBadRequestException();

            return _mapper.Map<OrderResponse>(order);
        }

        public async Task<IEnumerable<DeliveryMethodDto>> GetDeliveryMethodsAsync()
        {
          var delivery = await _unitOfWork.GetRepository<int, DeliveryMethod>().GetAllAsync();
            if(delivery == null) throw new DeliveryMethodBadRequestException();
            return _mapper.Map<IEnumerable<DeliveryMethodDto>>(delivery);
        }

        public async Task<OrderResponse?> GetOrderForSpecificUserAsync(Guid id, string userEmail)
        {
            var spec = new OrderSpecification(id, userEmail);
          var order = await _unitOfWork.GetRepository<Guid,Order>().GetAsync(spec);
            if(order == null ) throw new OrderNotFoundException(id);
            return _mapper.Map<OrderResponse>(order);
        }

        public async Task<IEnumerable<OrderResponse>> GetOrdersForSpecificUserAsync(string userEmail)
        {
            var spec = new OrderSpecification(userEmail);
            var orders = await _unitOfWork.GetRepository<Guid, Order>().GetAllAsync(spec);
            return _mapper.Map<IEnumerable<OrderResponse>>(orders); 
            
        }
    }
}
