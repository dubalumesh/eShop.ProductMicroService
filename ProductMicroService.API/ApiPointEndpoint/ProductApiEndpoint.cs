using ProductMicroService.Core.DTO;
using ProductMicroService.Core.ServiceContracts;
using ProductMicroService.Core.Validator;

namespace ProductMicroService.API.ApiPointEndpoint
{
    /// <summary>
    /// Product API Endpoints
    /// </summary>
    public static class ProductApiEndpoint
    {
        /// <summary>
        /// Extnetsion method to register API Endpoints
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static async Task<IEndpointRouteBuilder> MapProductApiEndpoint(this IEndpointRouteBuilder builder)
        {
            // Get All Products Endpoint
            builder.MapGet("/api/products", async (IProductsService productService) =>
            {
                return Results.Ok(await productService.GetProductsAsync());

            });
            // Get Product By Id Endpoint
            builder.MapGet("/api/product/{productId}", async (IProductsService productService, int productId) =>
            {
                return Results.Ok(await productService.GetProductByIdAsync(productId));
            });
            // Get Product By Name Endpoint
            builder.MapGet("/api/productByName/{productName}", async (IProductsService productService, string productName) =>
            {
                return Results.Ok(await productService.GetProductByNameAsync(productName));
            });
            // Add Product Endpoint

            builder.MapPost("/api/product",
                async (IProductsService productsService, ProductAddRequest productAddRequest, ProductAddRequestValidator validator) =>
                {
                    if (productAddRequest is null)
                    {
                        return Results.BadRequest("Product data is required.");
                    }
                    var validationResult = await validator.ValidateAsync(productAddRequest);
                    if (!validationResult.IsValid)
                    {
                        var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                        return Results.BadRequest(new { Errors = errors });
                    }
                    ProductResponse productResponse = await productsService.AddProductAsync(productAddRequest);
                    return Results.Ok(productResponse);
                });
            // Update Product Endpoint

            builder.MapPut("/api/product", async (IProductsService productsService, ProductUpdateRequest productUpdateRequest, ProductUpdateRequestValidator validator) =>
               {
                   if (productUpdateRequest is null)
                   {
                       return Results.BadRequest("Product data is required.");
                   }
                   var validationResult = await validator.ValidateAsync(productUpdateRequest);
                   if (!validationResult.IsValid)
                   {
                       var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                       return Results.BadRequest(new { Errors = errors });
                   }
                   ProductResponse productResponse = await productsService.UpdateProductAsync(productUpdateRequest);
                   return Results.Ok(productResponse);
               });
            // Delete Product Endpoint
            builder.MapDelete("/api/product/{productId}", async (IProductsService productsService, int productId) =>
            {
                bool isDeleted = await productsService.DeleteProductAsync(productId);
                if (isDeleted)
                {
                    return Results.Ok($"Product with Id {productId} has been deleted successfully.");
                }
                else
                {
                    return Results.NotFound($"Product with Id {productId} not found.");
                }
            });

            return builder;
        }

    }
}
