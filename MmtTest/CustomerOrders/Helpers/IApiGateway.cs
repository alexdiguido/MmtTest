using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomerOrders.Helpers
{
    public interface IApiGateway
    {
        Task<T> GetAsync<T>(string serviceName, string url, IList<string> pathParameters);
    }
}