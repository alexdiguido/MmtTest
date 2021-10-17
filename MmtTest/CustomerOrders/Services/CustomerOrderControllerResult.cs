﻿using System.Net;
using System.Threading.Tasks;
using CustomerOrders.Core;
using Microsoft.AspNetCore.Mvc;

namespace CustomerOrders.Services
{
    public class CustomerOrderControllerResult : ICustomerOrderControllerResult
    {
        public Task<IActionResult> HandleAsync(ICustomerOrder response)
        {
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            else
            {
                if (!string.IsNullOrEmpty(response.Customer?.LastName))
                {
                    return BadRequest("Autentication Failed");
                }

                return InternalServerError("An error occurred");
            }
        }

        public Task<IActionResult> Ok(ICustomerOrder response)
        {
            var statusCode = HttpStatusCode.OK;

            var result = new ObjectResult(response) { StatusCode = (int)statusCode };

            return Task.FromResult<IActionResult>(result);
        }

        public Task<IActionResult> BadRequest(string message)
        {
            var statusCode = HttpStatusCode.BadRequest;

            var result = new ObjectResult(message) { StatusCode = (int)statusCode };

            return Task.FromResult<IActionResult>(result);
        }

        public Task<IActionResult> InternalServerError(string message)
        {
            var statusCode = HttpStatusCode.InternalServerError;

            var result = new ObjectResult(message) { StatusCode = (int)statusCode };

            return Task.FromResult<IActionResult>(result);
        }
    }
}