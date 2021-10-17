using System.Threading.Tasks;

namespace CustomerOrders.Services
{
    public class GetCustomerOrderService : IGetCustomerOrderService
    {
        private readonly IGetCustomerDetailsService _getCustomerDetailsService;
        private readonly IGetOrderService _getOrderService;

        public GetCustomerOrderService(IGetCustomerDetailsService getCustomerDetailsService,
            IGetOrderService getOrderService)
        {
            _getCustomerDetailsService = getCustomerDetailsService;
            _getOrderService = getOrderService;
        }

        public async Task<GetCustomerOrderResponse> GetAsync(GetCustomerOrderRequest getGetCustomerOrderRequest)
        {
            var customerOrderResponse = new GetCustomerOrderResponse();

            var customerDetailsRequest = new GetCustomerDetailsRequest()
            {
                Email = getGetCustomerOrderRequest.User, 
                CustomerId = getGetCustomerOrderRequest.CustomerId
            };
            customerOrderResponse.CustomerDetails = await _getCustomerDetailsService.GetAsync(customerDetailsRequest);
            
            if (customerOrderResponse.CustomerDetails.Success)
            {
                var orderServiceRequest = new GetOrderServiceRequest() {CustomerId = getGetCustomerOrderRequest.CustomerId};
                customerOrderResponse.CustomerOrder = await _getOrderService.GetAsync(orderServiceRequest);
            }

            return customerOrderResponse;
        }
    }
}