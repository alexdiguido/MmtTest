using System;
using System.Collections.Generic;

namespace CustomerOrders.Core
{
    public class Order : IOrder
    {   
        public int OrderNumber { get; set; }

        public DateTime OrderDate { get; set; }

        public string DeliveryAddress { get; set; }
            
        public List<Product> OrderItems { get; set; }
        public DateTime DeliveryExpected { get; set; }
    }
}