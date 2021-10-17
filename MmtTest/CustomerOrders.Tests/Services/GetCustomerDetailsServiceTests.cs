using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Cache;
using System.Text;
using System.Threading.Tasks;
using CustomerOrders.Helpers;
using CustomerOrders.Services;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;

namespace CustomerOrders.Tests.Services
{
    public class GetCustomerDetailsServiceTests
    {
        [Fact]
        public async Task GetAsync_CustomerExist_ShouldReturnCustomerDataWithSuccess()
        {
            var request = new GetCustomerDetailsRequest()
            {
                CustomerId = "customerId",
                Email = "email"
            };

            var customerDetails = new CustomerDetails()
            {
                CustomerId = "customerId",
                Email = "email",
                FirstName = "FirstName",
                HouseNumber = "HouseNumber",
                LastName = "LastName",
                PostCode = "PostCode",
                Street = "Street",
                Town = "Town"
            };

            var apiGatewayMock = new Mock<IApiGateway>();

            var configurationMock = new Mock<IConfiguration>();

            apiGatewayMock.Setup(api =>
                api.GetAsync<CustomerDetails>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<string>>()))
                .Returns(Task.FromResult(customerDetails));

            var expected = new GetCustomerDetailsResponse()
            {
                CustomerDetails = customerDetails,
                Success = true
            };

            var actual = await new GetCustomerDetailsService(apiGatewayMock.Object, configurationMock.Object).GetAsync(request);

            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task GetAsync_CustomerNotExist_ShouldReturnCustomerDataNotSuccessfull()
        {
            var request = new GetCustomerDetailsRequest()
            {
                CustomerId = "customerId",
                Email = "email"
            };

            var customerDetails = new CustomerDetails()
            {
                CustomerId = "customerId",
                Email = "email",
                FirstName = "FirstName",
                HouseNumber = "HouseNumber",
                LastName = "LastName",
                PostCode = "PostCode",
                Street = "Street",
                Town = "Town"
            };

            var apiGatewayMock = new Mock<IApiGateway>();

            var configurationMock = new Mock<IConfiguration>();

            apiGatewayMock.Setup(api =>
                    api.GetAsync<CustomerDetails>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<string>>()))
                .ThrowsAsync(new Exception("message"));


            var actual = await new GetCustomerDetailsService(apiGatewayMock.Object, configurationMock.Object).GetAsync(request);

            actual.CustomerDetails.Should().BeNull();
            actual.Success.Should().BeFalse();
            actual.Errors.Any().Should().BeTrue();
        }

        [Fact]
        public async Task GetAsync_CustomerExist_EmailNotMatch_ShouldReturnCustomerDataWithSuccess()
        {
            var request = new GetCustomerDetailsRequest()
            {
                CustomerId = "customerId",
                Email = "email"
            };

            var customerDetails = new CustomerDetails()
            {
                CustomerId = "customerIdNotMatch",
                Email = "email",
                FirstName = "FirstName",
                HouseNumber = "HouseNumber",
                LastName = "LastName",
                PostCode = "PostCode",
                Street = "Street",
                Town = "Town"
            };

            var apiGatewayMock = new Mock<IApiGateway>();

            var configurationMock = new Mock<IConfiguration>();

            apiGatewayMock.Setup(api => 
                    api.GetAsync<CustomerDetails>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<string>>()))
                .Returns(Task.FromResult(customerDetails));


            var actual = await new GetCustomerDetailsService(apiGatewayMock.Object, configurationMock.Object).GetAsync(request);

            actual.CustomerDetails.Should().NotBeNull();
            actual.Success.Should().BeFalse();
            actual.Errors.Any().Should().BeTrue();
        }
    }
}
