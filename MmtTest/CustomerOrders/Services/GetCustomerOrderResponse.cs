namespace CustomerOrders.Services
{
    public class GetCustomerOrderResponse
    {
        public GetCustomerDetailsResponse CustomerDetails { get; set; }
        public GetOrderServiceResponse CustomerOrder { get; set; }
    }
}