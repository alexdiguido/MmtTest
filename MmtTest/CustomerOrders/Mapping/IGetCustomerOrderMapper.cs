using CustomerOrders.Core;
using CustomerOrders.Services;

namespace CustomerOrders.Mapping
{
    public interface IGetCustomerOrderMapper
    {
        ICustomerOrder MapFrom(GetCustomerOrderResponse getCustomerOrderResponse);
    }
}