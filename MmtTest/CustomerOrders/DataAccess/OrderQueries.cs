using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using CustomerOrders.DataAccess.Entities;
using Dapper;

namespace CustomerOrders.DataAccess
{
    public class OrderQueries : IOrderQueries
    {
        private readonly IConnectionProvider _connectionProvider;
        private static string _sql;

        public OrderQueries(IConnectionProvider connectionProvider)
        {
            _connectionProvider = connectionProvider;
        }


        public async Task<OrderByCustomerIdResponse> GetOrdersByCustomerIdV2(string customerId)
        {
            var orderList = new List<Order>();

            using (var connection = _connectionProvider.GetDbConnection())
            {
                _sql =
                    $"select * from ORDERS o inner join ORDERITEMS i on o.ORDERID = I.ORDERID inner join PRODUCTS p on i.PRODUCTID = p.PRODUCTID  where CUSTOMERID='{customerId}'";


                var orders = await connection.QueryAsync<Order, OrderItem, Product, Order>(_sql,
                    (order, orderItem, product) =>
                    {
                        product.Quantity = orderItem.Quantity;
                        product.Price = orderItem.Price;
                        if (order.Products == null)
                        {
                            order.Products = new List<Product>();
                        }

                        order.Products.Add(product);

                        return order;
                    },
                    splitOn: "ORDERID, PRODUCTID");

                orderList = orders?.ToList();
            }

            return orderList.Any() ? 
                    OrderByCustomerIdMapper.MapFrom(orderList) : 
                    new OrderByCustomerIdResponse();
        }

        public async Task<List<Product>> GetProductsByOrderId(int orderId)
        {
            var productsList = new List<Product>();

            using (var connection = _connectionProvider.GetDbConnection())
            {
                try
                {
                    _sql =
                        $"select * from PRODUCTS p inner join ORDERITEMS i on p.PRODUCTID = i.PRODUCTID  where ORDERID='{orderId}'";

                    var products = await connection.QueryAsync<Product, OrderItem, Product>(_sql,
                        (product, orderItem) =>
                        {
                            product.Quantity = orderItem.Quantity;
                            product.Price = orderItem.Price;

                            return product;
                        },
                        splitOn: "ORDERITEMID");

                    productsList = products?.ToList();
                }
                catch (Exception exception)
                {

                }
               
            }

            return productsList;
        }

        public async Task<List<Order>> GetOrdersByCustomerId(string customerId)
        {
            var orderList = new List<Order>();

            using (var connection = _connectionProvider.GetDbConnection())
            {
                _sql = $"select * from ORDERS where CUSTOMERID='{customerId}'";


                var orders = await connection.QueryAsync<Order>(_sql);
                orderList = orders?.ToList();
            }

            return orderList;
        }
    }
}

    public class OrderByCustomerIdMapper
    {
        public static OrderByCustomerIdResponse MapFrom(IEnumerable<Order> orders)
        {
            var mostRecentOrder = orders.OrderByDescending(order => order.OrderDate).FirstOrDefault();
            orders = orders.Where(ord => ord.OrderId == mostRecentOrder.OrderId);

            var products = new List<Product>();

            foreach (var order in orders)
            {
                products.AddRange(order.Products);
            }

            return new OrderByCustomerIdResponse()
            {
                OrderId = mostRecentOrder.OrderId,
                OrderDate = mostRecentOrder.OrderDate,
                ContainsGift = mostRecentOrder.ContainsGift,
                CustomerId = mostRecentOrder.CustomerId,
                DeliveryExpected = mostRecentOrder.DeliveryExpected,
                ShippingMode = mostRecentOrder.ShippingMode,
                Products = products
            };
        }
    }

    public class OrderByCustomerIdResponse
    {
        public int OrderId { get; set; }
        public string CustomerId { get; set; }

        public List<Product> Products { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime DeliveryExpected { get; set; }

        public bool ContainsGift { get; set; }

        public string ShippingMode { get; set; }
    }