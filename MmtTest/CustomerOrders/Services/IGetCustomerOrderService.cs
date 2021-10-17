using System.Threading.Tasks;

namespace CustomerOrders.Services
{
    public interface IGetCustomerOrderService
    {
        Task<GetCustomerOrderResponse> GetAsync(GetCustomerOrderRequest getGetCustomerOrderRequest);
    }
}