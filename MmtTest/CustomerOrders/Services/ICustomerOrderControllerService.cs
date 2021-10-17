using System.Threading.Tasks;
using CustomerOrders.Core;

namespace CustomerOrders.Services
{
    public interface ICustomerOrderControllerService
    {
        Task<ICustomerOrder> GetAsync(string user, string customerId);
    }
}