using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductMicroService.Core.DTO
{
    public record ProductAddRequest(string? ProductName, string? Category, double UnitPrice, int QuantityInStock)
    {
        public ProductAddRequest() : this(default, default, default, default)
        { }
    }
}
