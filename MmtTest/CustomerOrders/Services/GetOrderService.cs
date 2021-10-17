using System;
using System.Linq;
using System.Threading.Tasks;
using CustomerOrders.DataAccess;
using CustomerOrders.DataAccess.Entities;

namespace CustomerOrders.Services
{
    public class GetOrderService : IGetOrderService
    {
        private readonly IOrderQueries _orderQueries;

        public GetOrderService(IOrderQueries orderQueries)
        {
            _orderQueries = orderQueries;
        }
        public async Task<GetOrderServiceResponse> GetAsync(GetOrderServiceRequest orderServiceRequest)
        {
            var response = new GetOrderServiceResponse();
            try
            {
                //response.CustomerOrderResponse = await _orderQueries.GetOrdersByCustomerIdV2(orderServiceRequest.CustomerId);
                var order = await _orderQueries.GetOrdersByCustomerId(orderServiceRequest.CustomerId);
                var mostRecentOrder = order.OrderByDescending(or => or.OrderDate).FirstOrDefault();
                if (mostRecentOrder != null)
                {
                    mostRecentOrder.Products = await _orderQueries.GetProductsByOrderId(mostRecentOrder.OrderId);
                    response = MapFrom(mostRecentOrder);
                }

                response.Success = true;
            }
            catch(Exception exception)
            {
                //todo add logging 
                response.Errors.Add(exception);
            }

            return response;
        }

        private GetOrderServiceResponse MapFrom(Order order)
        {
            return new GetOrderServiceResponse()
            {
                OrderId = order.OrderId,
                OrderDate = order.OrderDate,
                ContainsGift = order.ContainsGift,
                CustomerId = order.CustomerId,
                DeliveryExpected = order.DeliveryExpected,
                ShippingMode = order.ShippingMode,
                Products = order.Products
            };
        }


    }
}