using Mapster;
using ProductMicroService.Core.DTO;
using ProductMicroService.Core.Entities;
using ProductMicroService.Core.IRepository;
using ProductMicroService.Core.ServiceContracts;
using ProductMicroService.Core.Validator;

namespace ProductMicroService.Core.ServiceImplementation
{
    public class ProductsService(IProductsRepository _productsRepository,
        ProductAddRequestValidator productAddValidator,
        ProductUpdateRequestValidator productUpdateValidator) : IProductsService
    {
        public async Task<ProductResponse> AddProductAsync(ProductAddRequest productAddRequest)
        {
            if (productAddRequest == null)
            {
                throw new ArgumentNullException(nameof(productAddRequest), "product add request cannot be null")
;
            }
            var result = productAddValidator.Validate(productAddRequest);
            if (!result.IsValid)
            {
                var errors = result.Errors.Select(e => e.ErrorMessage);
                throw new ArgumentException(string.Join(", ", errors));
            }

            Product? newProduct = await _productsRepository.AddProductAsync(productAddRequest.Adapt<Product>());
            if (newProduct != null) { return null; }
            else
                return newProduct.Adapt<ProductResponse>()!;

        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            return await
                  _productsRepository.DeleteProductAsync(id);

        }

        public async Task<ProductResponse> GetProductByIdAsync(int id)
        {
            var product = await _productsRepository.GetProductByIdAsync(id);
            return product.Adapt<ProductResponse>();
        }

        public async Task<ProductResponse> GetProductByNameAsync(string name)
        {
            var product = await _productsRepository.GetProductByNameAsync(name);
            return product.Adapt<ProductResponse>();
        }

        public async Task<IEnumerable<ProductResponse>> GetProductsAsync()
        {
            var products = await _productsRepository.GetProductsAsync();
            return products.Adapt<IEnumerable<ProductResponse>>();
        }

        public async Task<ProductResponse> UpdateProductAsync(ProductUpdateRequest productUpdateRequest)
        {
            if (productUpdateRequest == null)
            {
                throw new ArgumentNullException(nameof(productUpdateRequest), "productUpdateRequest cannot be null");
            }
            var validationResult = productUpdateValidator.Validate(productUpdateRequest);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage);
                throw new ArgumentException(string.Join(", ", errors));
            }
            var updatedProduct = await _productsRepository.UpdateProductAsync(productUpdateRequest.Adapt<Product>());

            return updatedProduct.Adapt<ProductResponse>();
        }
    }
}
