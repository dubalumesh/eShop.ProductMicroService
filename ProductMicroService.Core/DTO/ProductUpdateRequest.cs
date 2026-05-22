using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductMicroService.Core.DTO
{
    public record ProductUpdateRequest(int ProductId, string? ProductName, string? Category, double UnitPrice, int QuantityInStock)
    {
        public ProductUpdateRequest() : this(default, default, default, default, default)
        { }
    }
}
