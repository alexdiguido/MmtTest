using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CustomerOrders.Helpers;

namespace CustomerOrders.Services
{
    public class GetCustomerDetailsService : IGetCustomerDetailsService
    {
        private readonly IApiGateway _apiGateway;

        private readonly string apiKey = "1CrsOooSHlV15C7OYnLY0DHjBHyjzoI8LNHITV04cNCyNCahecPDhw==";

        public GetCustomerDetailsService(IApiGateway apiGateway)
        {
            _apiGateway = apiGateway;
        }
        public async Task<GetCustomerDetailsResponse> GetAsync(GetCustomerDetailsRequest getGetCustomerOrderRequest)
        {
            var response = new GetCustomerDetailsResponse();

            try
            {
                response.CustomerDetails = await _apiGateway.GetAsync<CustomerDetails>("CustomerDetails", $"api/GetUserDetails?code={apiKey}&email={getGetCustomerOrderRequest.Email}", new List<string>());

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