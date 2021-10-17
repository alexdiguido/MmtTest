using System;
using System.Collections.Generic;

namespace CustomerOrders.Services
{
    public abstract class ResponseBase
    {
        public bool Success { get; set; }

        public IList<Exception> Errors { get; set; } = new List<Exception>();

        public enum Error
        {
            RequestIsNull
        }
    }
}