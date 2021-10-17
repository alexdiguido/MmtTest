namespace CustomerOrders.Core
{
    public interface ICustomerOrder
    {
        ICustomer Customer { get; set; }
        IOrder Order { get; set; }
    }
}