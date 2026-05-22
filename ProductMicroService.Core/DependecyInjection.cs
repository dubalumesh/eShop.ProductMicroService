using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductMicroService.Core.ServiceContracts;
using ProductMicroService.Core.ServiceImplementation;
using ProductMicroService.Core.Validator;

namespace ProductMicroService.Core
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddCore(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddValidatorsFromAssemblyContaining<ProductAddRequestValidator>();
            services.AddScoped<IProductsService, ProductsService>();
            return services;
        }
    }
}
