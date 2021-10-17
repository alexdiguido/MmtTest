using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CustomerOrders.Services;
using FluentAssertions;
using Moq;
using Xunit;
using IGetCustomerOrderService = CustomerOrders.Services.IGetCustomerOrderService;

namespace CustomerOrders.Tests
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

            getCustomerDetailsMock.Setup(g => g.GetAsync(customerDetailsRequest)).Returns(Task.FromResult(customerDetailsResponse));

            var expected = new GetCustomerOrderResponse()
            {
                CustomerDetails = customerDetailsResponse
            };

            var actual = await new GetCustomerOrderService(getCustomerDetailsMock.Object, getCustomerOrderMock.Object)
                .GetAsync(request);

            actual.Should().BeEquivalentTo(expected);
        }
    }
}
