using System.Collections.Generic;
using System.Threading.Tasks;
using CustomerOrders.Core;
using Microsoft.AspNetCore.Mvc;

namespace CustomerOrders.Services
{
    public interface ICustomerOrderControllerResult
    {
        Task<IActionResult> HandleAsync(ICustomerOrder response);
    }
}