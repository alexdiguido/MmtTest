namespace CustomerOrders.DataAccess.Entities
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public bool Returnable { get; set; }
    }
}

