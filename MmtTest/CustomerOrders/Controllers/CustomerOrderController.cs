using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CustomerOrders.Core;
using CustomerOrders.Services;

namespace CustomerOrders.Controllers
{
    [ApiController]
    [Route("customer-order")]
    [Produces("application/json")]
    public class CustomerOrderController : ControllerBase
    {
        private readonly ICustomerOrderControllerService _controllerService;
        private readonly ICustomerOrderControllerResult _customerOrderControllerResult;

        public CustomerOrderController(
            ICustomerOrderControllerService controllerService, 
            ICustomerOrderControllerResult customerOrderControllerResult)
        {
            _controllerService = controllerService;
            _customerOrderControllerResult = customerOrderControllerResult;
        }

        [HttpPost("next-order")]
        [ProducesResponseType(typeof(CustomerOrder), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Get([FromBody]GetNextOrderApiRequest request)
        {
            var response = await _controllerService.GetAsync(request.User, request.CustomerId);
            return await _customerOrderControllerResult.HandleAsync(response);
        }
    }
}
