using System;
using System.Collections.Generic;
using System.Linq;
using CustomerOrders.Core;
using CustomerOrders.Services;

namespace CustomerOrders.Mapping
{
    public class GetCustomerOrderMapper : IGetCustomerOrderMapper
    {
        public ICustomerOrder MapFrom(GetCustomerOrderResponse getCustomerOrderResponse)
        {

            var customerOrder = new CustomerOrder()
            {
                Customer = MapFrom(getCustomerOrderResponse.CustomerDetails),
                Order = MapFrom(getCustomerOrderResponse.CustomerOrder, getCustomerOrderResponse.CustomerDetails),
                IsSuccess =  getCustomerOrderResponse.CustomerOrder.Success && 
                             getCustomerOrderResponse.CustomerDetails.Success
            };


            return customerOrder;
        }

        private Customer MapFrom(GetCustomerDetailsResponse getCustomerDetailsResponse)
        {
            return new Customer()
            {
                FirstName = getCustomerDetailsResponse?.CustomerDetails?.FirstName,
                LastName = getCustomerDetailsResponse?.CustomerDetails?.LastName
            };
        }

        private Order MapFrom(GetOrderServiceResponse getCustomerOrderResponse, GetCustomerDetailsResponse getCustomerDetailsResponse)
        {
            if(!(getCustomerOrderResponse?.Success ?? false) || !getCustomerDetailsResponse.Success) return new Order();
            return new Order()
            {
                OrderNumber = getCustomerOrderResponse.OrderId,
                OrderDate = getCustomerOrderResponse?.OrderDate ?? new DateTime(),
                DeliveryAddress = BuildDeliveryAddress(getCustomerDetailsResponse),
                OrderItems = MapFrom(getCustomerOrderResponse.Products, getCustomerOrderResponse.ContainsGift).ToList(),
                DeliveryExpected = getCustomerOrderResponse.DeliveryExpected
            };
        }

        private string BuildDeliveryAddress(GetCustomerDetailsResponse getCustomerOrderResponse)
        {
            return $"{getCustomerOrderResponse?.CustomerDetails?.HouseNumber ?? string.Empty} " +
                   $"{getCustomerOrderResponse?.CustomerDetails?.Street ?? string.Empty} " +
                   $"{getCustomerOrderResponse?.CustomerDetails?.Town ?? string.Empty} " +
                   $"{getCustomerOrderResponse?.CustomerDetails?.PostCode ?? string.Empty} ";
        }

        private IEnumerable<Core.Product> MapFrom(IEnumerable<DataAccess.Entities.Product> products, bool isGift)
        {
            return products?.Select(p => MapFrom(p, isGift)) ?? Enumerable.Empty<Product>();
        }

        private Core.Product MapFrom(DataAccess.Entities.Product entityProduct, bool isGift)
        {
            return new Core.Product()
            {
                Quantity = entityProduct.Quantity,
                PriceEach = entityProduct.Price, //is price each or price each * quantity ?
                ProductName = isGift ? "Gift" : entityProduct.ProductName
            };
        }
    }
}