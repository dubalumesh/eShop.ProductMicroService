
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProductMicroService.Infrastructure.Context;


namespace ProductMicroService.Infrastructure.Persistence
{
    public static class HostExtensions
    {
        public static IHost SeedData(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var productDbContext = scope.ServiceProvider.GetRequiredService<ProductDbContext>();
                var dbInitializer = new DbInitializer(productDbContext);
                dbInitializer.InitializeAsync().GetAwaiter().GetResult();
            }
            return host;
        }
    }
}