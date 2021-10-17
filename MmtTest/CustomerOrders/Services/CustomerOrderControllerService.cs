using System.Collections.Generic;
using System.Threading.Tasks;
using CustomerOrders.Core;

namespace CustomerOrders.Services
{
    public class CustomerOrderControllerService : ICustomerOrderControllerService
    {
        private readonly IGetCustomerOrderService _service;
        private readonly IGetCustomerOrderMapper _mapper;

        public CustomerOrderControllerService(IGetCustomerOrderService service, IGetCustomerOrderMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public async Task<ICustomerOrder> GetAsync(string user, string customerId)
        {
            var getCustomerOrderRequest = new GetCustomerOrderRequest {User = user, CustomerId = customerId};
            var getCustomerOrderResponse = await _service.GetAsync(getCustomerOrderRequest);

            var response = _mapper.MapFrom(getCustomerOrderResponse);

            return response;
        }
    }
}