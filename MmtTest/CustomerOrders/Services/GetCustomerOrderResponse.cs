namespace CustomerOrders.Services
{
    public class GetCustomerOrderResponse : ResponseBase
    {
        public GetCustomerDetailsResponse CustomerDetails { get; set; }
        public GetOrderServiceResponse CustomerOrder { get; set; }
    }
}