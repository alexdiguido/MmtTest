using System.Data;

namespace CustomerOrders.DataAccess
{
    public interface IConnectionProvider
    {
        IDbConnection GetDbConnection();
    }
}