using System.Threading.Tasks;

namespace CustomerOrders.Services
{
    public interface IGetCustomerDetailsService
    {
        Task<GetCustomerDetailsResponse> GetAsync(GetCustomerDetailsRequest getGetCustomerOrderRequest);
    }
}