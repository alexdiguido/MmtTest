using System.Collections.Generic;
using System.Linq;
using CustomerOrders.Core;

namespace CustomerOrders.Services
{
    public interface IGetCustomerOrderMapper
    {
        ICustomerOrder MapFrom(GetCustomerOrderResponse getCustomerOrderResponse);
    }

    public class GetCustomerOrderMapper : IGetCustomerOrderMapper
    {
        public ICustomerOrder MapFrom(GetCustomerOrderResponse getCustomerOrderResponse)
        {
            var customerOrder = new CustomerOrder()
            {
                Customer = new Customer()
                {
                    FirstName = getCustomerOrderResponse.CustomerDetails.CustomerDetails.FirstName,
                    LastName = getCustomerOrderResponse.CustomerDetails.CustomerDetails.LastName
                },
                Order = new Order()
                {
                    OrderNumber = getCustomerOrderResponse.CustomerOrder.OrderId,
                    OrderDate = getCustomerOrderResponse.CustomerOrder.OrderDate,
                    DeliveryAddress = BuildDeliveryAddress(getCustomerOrderResponse),
                    OrderItems = MapFrom(getCustomerOrderResponse.CustomerOrder.Products).ToList(),
                    DeliveryExpected = getCustomerOrderResponse.CustomerOrder.DeliveryExpected
                }
            };

            return customerOrder;
        }

        private string BuildDeliveryAddress(GetCustomerOrderResponse getCustomerOrderResponse)
        {
            return $"{getCustomerOrderResponse.CustomerDetails.CustomerDetails.HouseNumber} " +
                   $"{getCustomerOrderResponse.CustomerDetails.CustomerDetails.Street} " +
                   $"{getCustomerOrderResponse.CustomerDetails.CustomerDetails.Town} " +
                   $"{getCustomerOrderResponse.CustomerDetails.CustomerDetails.PostCode} ";
        }

        private IEnumerable<Core.Product> MapFrom(IEnumerable<DataAccess.Entities.Product> products)
        {
            return products?.Select(p => MapFrom(p)) ?? Enumerable.Empty<Product>();
        }

        private Core.Product MapFrom(DataAccess.Entities.Product entityProduct)
        {
            return new Core.Product()
            {
                Quantity = entityProduct.Quantity,
                PriceEach = entityProduct.Price, //is price each or price each * quantity ?
                ProductName = entityProduct.ProductName
            };
        }
    }
}