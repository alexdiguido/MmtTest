using System;
using System.Collections.Generic;
using CustomerOrders.DataAccess.Entities;

namespace CustomerOrders.Services
{
    public class GetOrderServiceResponse : ResponseBase
    {
        public int OrderId { get; set; }
        public string CustomerId { get; set; }

        public List<Product> Products { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime DeliveryExpected { get; set; }

        public bool ContainsGift { get; set; }

        public string ShippingMode { get; set; }
    }       
}