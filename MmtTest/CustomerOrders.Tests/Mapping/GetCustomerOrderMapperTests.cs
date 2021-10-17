using System;
using System.Collections.Generic;
using System.Text;
using CustomerOrders.Core;
using CustomerOrders.Mapping;
using CustomerOrders.Services;
using FluentAssertions;
using Xunit;

namespace CustomerOrders.Tests.Mapping
{
    public class GetCustomerOrderMapperTests
    {
        [Fact]
        public void MapFrom_CustomerDetailsNotSucceeded()
        {
            var data = new GetCustomerOrderResponse()
            {
                CustomerDetails = new GetCustomerDetailsResponse(),
                CustomerOrder = new GetOrderServiceResponse()
            };

            var expected = new CustomerOrder()
            {
                Customer = new Customer(),
                Order = new Order()
            };

            var actual = new GetCustomerOrderMapper().MapFrom(data);

            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void MapFrom_CustomerDetailsNotSucceeded_ButNotEmpty()
        {
            var data = new GetCustomerOrderResponse()
            {
                CustomerDetails = new GetCustomerDetailsResponse()
                {
                    CustomerDetails = new CustomerDetails()
                    {
                        FirstName = "FirstName",
                        LastName = "LastName"
                    }
                },

                CustomerOrder = new GetOrderServiceResponse()
            };

            var expected = new CustomerOrder()
            {
                Customer = new Customer()
                {
                    FirstName = "FirstName",
                    LastName = "LastName"
                },

                Order = new Order()
            };

            var actual = new GetCustomerOrderMapper().MapFrom(data);

            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void MapFrom_CustomerDetailsSucceeded_ButNoOrders()
        {
            var data = new GetCustomerOrderResponse()
            {
                CustomerDetails = new GetCustomerDetailsResponse()
                {
                    CustomerDetails = new CustomerDetails()
                    {
                        FirstName = "FirstName",
                        LastName = "LastName",
                    },
                    Success = true
                },

                CustomerOrder = new GetOrderServiceResponse()
                {
                    
                }
            };

            var expected = new CustomerOrder()
            {
                Customer = new Customer()
                {
                    FirstName = "FirstName",
                    LastName = "LastName"
                },

                Order = new Order()
            };

            var actual = new GetCustomerOrderMapper().MapFrom(data);

            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void MapFrom_CustomerDetailsSucceeded_WithOrders()
        {
            var products = new List<CustomerOrders.DataAccess.Entities.Product>
            {
                new CustomerOrders.DataAccess.Entities.Product()
                {
                    ProductId = 1,
                    Colour = "colour1",
                    PackWeight = 2.2M,
                    PackWidth = 1.1M,
                    Price = 1.1M,
                    ProductName = "Product1",
                    Quantity = 1,
                    Size = "Size1"
                },
                new CustomerOrders.DataAccess.Entities.Product()
                {
                    ProductId = 2,
                    Colour = "colour2",
                    PackWeight = 2.2M,
                    PackWidth = 1.1M,
                    Price = 2.2M,
                    ProductName = "Product2",
                    Quantity = 2,
                    Size = "Size1"
                },
            };

            var data = new GetCustomerOrderResponse()
            {
                CustomerDetails = new GetCustomerDetailsResponse()
                {
                    CustomerDetails = new CustomerDetails()
                    {
                        FirstName = "FirstName",
                        LastName = "LastName",
                        CustomerId = "111",
                        Email = "email",
                        HouseNumber = "HouseNumber",
                        PostCode = "PostCode",
                        Street = "Street",
                        Town = "Town"
                    },
                    Success = true
                },

                CustomerOrder = new GetOrderServiceResponse()
                {
                    CustomerId = "111",
                    DeliveryExpected = new DateTime(2021, 10, 20),
                    OrderDate = new DateTime(2021, 10, 16),
                    OrderId = 1,
                    Success = true,
                    Products = products,
                    ShippingMode = "shippingMode"
                }
            };

            var expected = new CustomerOrder()
            {
                Customer = new Customer()
                {
                    FirstName = "FirstName",
                    LastName = "LastName",
                },

                Order = new Order()
                {
                    OrderDate = new DateTime(2021, 10, 16),
                    DeliveryExpected = new DateTime(2021, 10, 20),
                    DeliveryAddress = "HouseNumber Street Town PostCode ",
                    OrderNumber = 1,
                    OrderItems = new List<Product>()
                    {
                        new Product()
                        {
                            ProductName = "Product1",
                            Quantity = 1,
                            PriceEach = 1.1M
                        },
                        new Product()
                        {
                            ProductName = "Product2",
                            Quantity = 2,
                            PriceEach = 2.2M
                        }
                    }
                },
                IsSuccess = true
            };

            var actual = new GetCustomerOrderMapper().MapFrom(data);

            actual.Should().BeEquivalentTo(expected);
        }
    }
}
