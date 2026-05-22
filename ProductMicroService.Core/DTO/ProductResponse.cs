

namespace ProductMicroService.Core.DTO
{
    public record ProductResponse(int ProductId, string? ProductName, string? Category, double UnitPrice, int QuantityInStock)
    {
        public ProductResponse() : this(default, default, default, default, default)
        { }
    }
}
