using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CustomerOrders.Helpers;
using Microsoft.Extensions.Configuration;

namespace CustomerOrders.Services
{
    public class GetCustomerDetailsService : IGetCustomerDetailsService
    {
        private readonly IApiGateway _apiGateway;
        private readonly string _apiKey;


        public GetCustomerDetailsService(IApiGateway apiGateway, IConfiguration configuration)
        {
            _apiGateway = apiGateway;
            _apiKey = configuration["CustomerDetailsEndpoint:Token"];
        }
        public async Task<GetCustomerDetailsResponse> GetAsync(GetCustomerDetailsRequest getGetCustomerOrderRequest)
        {
            var response = new GetCustomerDetailsResponse();

            try
            {
                response.CustomerDetails = await _apiGateway.GetAsync<CustomerDetails>("CustomerDetails", $"api/GetUserDetails?code={_apiKey}&email={getGetCustomerOrderRequest.Email}", new List<string>());

                if (response.CustomerDetails.CustomerId != getGetCustomerOrderRequest.CustomerId)
                {
                    throw new Exception("credetials are wrong");
                }

                response.Success = true;
            }
            catch (Exception exception)
            {
                response.Errors.Add(exception);
                response.Success = false;
            }

            return response;
        }
    }
}