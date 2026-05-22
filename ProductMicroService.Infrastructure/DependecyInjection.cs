using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductMicroService.Core.IRepository;
using ProductMicroService.Infrastructure.Persistence;
using ProductMicroService.Infrastructure.Repositories;

namespace ProductMicroService.Infrastructure
{
    public static class DependecyInjection
    {
        /// <summary>
        /// Extension method to Inject dependancies of Infrastructure classes
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            //read the connetcion string template from appsettings.json
            string connectionTemplate = configuration.GetConnectionString("DefaultConnection")!;

            //read SQL server host, SQL_DB , SQL_USER, SQL_PASSWORD from environment
            string ENV_SQL_SERVER_HOST = Environment.GetEnvironmentVariable("SQL_SERVER_HOST")!.Trim();
            string ENV_SQL_DB = Environment.GetEnvironmentVariable("SQL_DB")!.Trim();
            string ENV_SQL_USER = Environment.GetEnvironmentVariable("SQL_USER")!.Trim();
            string ENV_SQL_PASSWORD = Environment.GetEnvironmentVariable("SQL_PASSWORD")!.Trim();

            Console.WriteLine("SQL_SERVER_HOST:" + ENV_SQL_SERVER_HOST);
            Console.WriteLine("SQL_DB:" + ENV_SQL_DB);
            Console.WriteLine("SQL_USER:" + ENV_SQL_USER);
            Console.WriteLine("SQL_PASSWORD:" + ENV_SQL_PASSWORD);

            //Configure the connection string with help for environment variable
            string connectionString = connectionTemplate.Replace("$SQL_SERVER_HOST", ENV_SQL_SERVER_HOST)
                                    .Replace("$DB", ENV_SQL_DB)
                                    .Replace("$SQL_USER", ENV_SQL_USER)
                                    .Replace("$SQL_PASSWORD", ENV_SQL_PASSWORD);

            Console.WriteLine("Final Connection String: " + connectionString);

            services.AddDbContext<Context.ProductDbContext>(options => options.UseSqlServer(connectionString));

            services.AddScoped<IProductsRepository, ProductRepository>();

            return services;
        }
    }
}
