using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace CustomerOrders.Core
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class Order : IOrder
    {   
        public int OrderNumber { get; set; }

        public DateTime OrderDate { get; set; }

        public string DeliveryAddress { get; set; }
            
        public List<Product> OrderItems { get; set; }
        public DateTime DeliveryExpected { get; set; }
    }
}