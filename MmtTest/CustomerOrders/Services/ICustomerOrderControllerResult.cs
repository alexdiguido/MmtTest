using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using CustomerOrders.Core;
using Microsoft.AspNetCore.Mvc;

namespace CustomerOrders.Services
{
    public interface ICustomerOrderControllerResult
    {
        Task<IActionResult> HandleAsync(ICustomerOrder response);
    }

    public class CustomerOrderControllerResult : ICustomerOrderControllerResult
    {
        public Task<IActionResult> HandleAsync(ICustomerOrder response)
        {
            var statusCode = HttpStatusCode.OK;

            var result = new ObjectResult(response) {StatusCode = (int) statusCode};

            return Task.FromResult<IActionResult>(result);
        }
    }
}