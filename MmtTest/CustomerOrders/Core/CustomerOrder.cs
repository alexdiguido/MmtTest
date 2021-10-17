using CustomerOrders.Controllers;

namespace CustomerOrders.Core
{
    public class CustomerOrder : ICustomerOrder
    {
        public ICustomer Customer { get; set; }
        public IOrder Order { get; set; }
        public bool IsSuccess { get; set; }
    }
}