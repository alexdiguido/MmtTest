using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CustomerOrders.DataAccess.Entities;
using CustomerOrders.Services;
using FluentAssertions;
using Moq;
using Xunit;

namespace CustomerOrders.Tests.Services
{
    public class GetCustomerOrderServiceTests
    {
        [Fact]
        public async Task GetAsync_CustomerServicesDetailRequest_ReturnNoSuccessWithEmptyObject_ShouldReturnNoCustomerServiceDetailsAndNoOrders()
        {
            var request = new GetCustomerOrderRequest()
            {
                CustomerId = "CustomerId",
                User = "User"
            };

            var customerDetailsRequest = new GetCustomerDetailsRequest()
            {
                CustomerId = request.CustomerId,
                Email = request.User
            };

            var customerDetailsResponse = new GetCustomerDetailsResponse()
            {
                CustomerDetails = new CustomerDetails()
            };

            var getCustomerDetailsMock = new Mock<IGetCustomerDetailsService>();

            var getCustomerOrderMock = new Mock<IGetOrderService>();

            getCustomerDetailsMock.Setup(g => g.GetAsync(It.Is<GetCustomerDetailsRequest>(p => p.CustomerId == customerDetailsRequest.CustomerId && p.Email == customerDetailsRequest.Email))).Returns(Task.FromResult(customerDetailsResponse));

            var expected = new GetCustomerOrderResponse()
            {
                CustomerDetails = customerDetailsResponse, 
                CustomerOrder = new GetOrderServiceResponse()
            };

            var actual = await new GetCustomerOrderService(getCustomerDetailsMock.Object, getCustomerOrderMock.Object)
                .GetAsync(request);

            actual.Should().BeEquivalentTo(expected);
        }


        [Fact]
        public async Task GetAsync_CustomerServicesDetailRequest_Succeed_ReturnNoOrder_shouldReturnEmptyOrderResponse() 
        { 
            var request = new GetCustomerOrderRequest()
            {
                CustomerId = "CustomerId",
                User = "User"
            };

            var customerDetailsRequest = new GetCustomerDetailsRequest()
            {
                CustomerId = request.CustomerId,
                Email = request.User
            };

            var customerDetailsResponse = new GetCustomerDetailsResponse()
            {
                CustomerDetails = new CustomerDetails()
                {
                    CustomerId = request.CustomerId,
                    Email = request.User,
                    FirstName = "FirstName",
                    HouseNumber = "HouseNumber",
                    LastName = "LastName", 
                    PostCode = "PostCode",
                    Street = "Street",
                    Town = "Town"
                }, 
                Success = true
            };

            var getorderServiceResponse = new GetOrderServiceResponse()
            {
                CustomerId = request.CustomerId,
                DeliveryExpected = new DateTime(2021, 10, 20),
                OrderDate = new DateTime(2021, 10, 16),
                OrderId = 1,
                Success = true,
                ContainsGift = true,
                Products = new List<Product>(),
                ShippingMode = "shippingMode"
            };

            var getCustomerDetailsMock = new Mock<IGetCustomerDetailsService>();

            var getCustomerOrderMock = new Mock<IGetOrderService>();

            getCustomerDetailsMock.Setup(g => g.GetAsync(It.Is<GetCustomerDetailsRequest>(p => p.CustomerId == customerDetailsRequest.CustomerId && p.Email == customerDetailsRequest.Email))).Returns(Task.FromResult(customerDetailsResponse));

            getCustomerOrderMock.Setup(o =>
                o.GetAsync(It.Is<GetOrderServiceRequest>(o => o.CustomerId == request.CustomerId))).Returns(Task.FromResult(getorderServiceResponse));


            var actual = await new GetCustomerOrderService(getCustomerDetailsMock.Object, getCustomerOrderMock.Object)
                .GetAsync(request);

            actual.Should().NotBeNull();
            actual.CustomerDetails.Should().BeEquivalentTo(customerDetailsResponse);
            actual.CustomerOrder.Should().BeEquivalentTo(getorderServiceResponse);
        }
    }
}
