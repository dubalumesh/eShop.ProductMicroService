

namespace ProductMicroService.Core.Entities
{
    public class Product
    {
        public int ProductId { get; set; }
        public required string ProductName { get; set; }
        public required string Category { get; set; }
        public double UnitPrice { get; set; }
        public int QuantityInStock { get; set; }
    }
}
