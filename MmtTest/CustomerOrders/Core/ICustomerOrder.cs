namespace CustomerOrders.Core
{
    public interface ICustomerOrder
    {
        ICustomer Customer { get; set; }
        IOrder Order { get; set; }
        bool IsSuccess { get; set; }
    }
}