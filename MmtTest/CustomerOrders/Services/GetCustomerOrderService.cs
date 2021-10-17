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

            var customerDetails = await _getCustomerDetailsService.GetAsync(customerDetailsRequest);

            var customerOrder = new GetOrderServiceResponse();
            if (customerDetails.Success)
            {
                var orderServiceRequest = new GetOrderServiceRequest() {CustomerId = getGetCustomerOrderRequest.CustomerId};
                customerOrder = await _getOrderService.GetAsync(orderServiceRequest);
            }

            return new GetCustomerOrderResponse()
            {
                CustomerDetails = customerDetails,
                CustomerOrder = customerOrder
            };
        }
    }
}