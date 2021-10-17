namespace CustomerOrders.DataAccess.Entities
{
    public class Product
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public decimal PackWeight { get; set; }

        public decimal PackWidth { get; set; }

        public string Colour { get; set; }

        public string Size { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}