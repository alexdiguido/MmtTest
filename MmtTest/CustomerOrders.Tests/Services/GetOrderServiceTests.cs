using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CustomerOrders.DataAccess;
using CustomerOrders.DataAccess.Entities;
using CustomerOrders.Services;
using FluentAssertions;
using Moq;
using Xunit;

namespace CustomerOrders.Tests.Services
{
    public class GetOrderServiceTests
    {
        [Fact]
        public async Task GetAsync_GetAsyncCustomerHasOrder_ShouldReturnMostRecentOrder()
        {
            var request = new GetOrderServiceRequest() {CustomerId = "111"};

            var orderQueriesMock = new Mock<IOrderQueries>();

            var products = new List<Product>()
            {
                new Product()
                {
                    ProductId = 1,
                    Colour = "colour1",
                    PackWeight = 2.2M,
                    PackWidth = 1.1M,
                    Price = 2.2M,
                    ProductName = "Product1",
                    Quantity = 1,
                    Size = "Size1"
                },
                new Product()
                {
                    ProductId = 2,
                    Colour = "colour2",
                    PackWeight = 3.3M,
                    PackWidth = 4.4M,
                    Price = 5.5M,
                    ProductName = "Product2",
                    Quantity = 2,
                    Size = "Size2"
                },
                new Product()
                {
                    ProductId = 3,
                    Colour = "colour3",
                    PackWeight = 10.3M,
                    PackWidth = 14.4M,
                    Price = 15.5M,
                    ProductName = "Product3",
                    Quantity = 3,
                    Size = "Size3"
                }
            };

            var orders = new List<Order>()
            {
                new Order()
                {
                    CustomerId = "111",
                    ContainsGift = false,
                    OrderDate = new DateTime(2021, 10, 16),
                    DeliveryExpected = new DateTime(2021, 10, 20),
                    OrderId = 1,
                    ShippingMode = "Shipping",
                },
                new Order()
                {
                    CustomerId = "111",
                    ContainsGift = false,
                    OrderDate = new DateTime(2021, 10, 14),
                    DeliveryExpected = new DateTime(2021, 10, 20),
                    OrderId = 2,
                    ShippingMode = "Shipping",
                }
            };

            orderQueriesMock.Setup(r => r.GetOrdersByCustomerId(request.CustomerId)).Returns(Task.FromResult(orders));

            orderQueriesMock.Setup(r => r.GetProductsByOrderId(orders[0].OrderId)).Returns(Task.FromResult(products));

            var expected = new GetOrderServiceResponse()
            {
                OrderId = orders[0].OrderId,
                OrderDate = orders[0].OrderDate,
                ContainsGift = orders[0].ContainsGift,
                CustomerId = orders[0].CustomerId,
                DeliveryExpected = orders[0].DeliveryExpected,
                ShippingMode = orders[0].ShippingMode,
                Products = products,
                Success = true
            };


            var actual = await new GetOrderService(orderQueriesMock.Object).GetAsync(request);

            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task GetAsync_GetAsyncCustomerHasNoOrder_ShouldReturnEmptyList()
        {
            var request = new GetOrderServiceRequest() { CustomerId = "111" };

            var orderQueriesMock = new Mock<IOrderQueries>();


            var orders = new List<Order>();

            orderQueriesMock.Setup(r => r.GetOrdersByCustomerId(request.CustomerId)).Returns(Task.FromResult(orders));

            var expected = new GetOrderServiceResponse()
            {
                Success = true
            };


            var actual = await new GetOrderService(orderQueriesMock.Object).GetAsync(request);

            actual.Should().BeEquivalentTo(expected);
        }
    }
}
