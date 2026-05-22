
using ProductMicroService.Core.DTO;
using ProductMicroService.Core.Entities;

namespace ProductMicroService.Core.ServiceContracts
{
    public interface IProductsService
    {
        Task<IEnumerable<ProductResponse>> GetProductsAsync();
        Task<ProductResponse> GetProductByIdAsync(int id);
        Task<ProductResponse> GetProductByNameAsync(string name);
        Task<ProductResponse> AddProductAsync(ProductAddRequest productAddRequest);
        Task<ProductResponse> UpdateProductAsync(ProductUpdateRequest productUpdateRequest);
        Task<bool> DeleteProductAsync(int id);
    }
}
