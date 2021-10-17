using System.Threading.Tasks;

namespace CustomerOrders.Services
{
    public interface IGetOrderService
    {
        Task<GetOrderServiceResponse> GetAsync(GetOrderServiceRequest orderServiceRequest);
    }
}