using CustomerOrders.DataAccess.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomerOrders.DataAccess
{
    public interface IOrderQueries
    {
        Task<List<Order>> GetOrdersByCustomerId(string customerId);

        Task<List<Product>> GetProductsByOrderId(int orderId);
    }
}