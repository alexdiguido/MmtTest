using System;
using System.Collections.Generic;

namespace CustomerOrders.Core
{
    public interface IOrder
    {
        string DeliveryAddress { get; set; }
        DateTime OrderDate { get; set; }
        int OrderNumber { get; set; }
        List<Product> OrderItems { get; set; }
        DateTime DeliveryExpected { get; set; }
    }
}