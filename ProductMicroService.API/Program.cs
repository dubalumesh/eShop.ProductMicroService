
using ProductMicroService.API.ApiPointEndpoint;
using ProductMicroService.API.Handler;
using ProductMicroService.Core;

using ProductMicroService.Infrastructure;
using ProductMicroService.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddCore(builder.Configuration);

//Configure Swagger for API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers().AddJsonOptions(option =>
{
    option.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
});

// Add services
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails(); //


var app = builder.Build();
app.UseExceptionHandler();

app.SeedData();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

// Configure the HTTP request pipeline.
app.UseRouting();

// Configure authentication and authorization middleware
app.UseAuthentication();
app.UseAuthorization();
// Configure Swagger middleware for API documentation in development environment

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Product Microservice API V1");
    options.RoutePrefix = string.Empty;
});
app.MapControllers();

await app.MapProductApiEndpoint();

app.Run();
