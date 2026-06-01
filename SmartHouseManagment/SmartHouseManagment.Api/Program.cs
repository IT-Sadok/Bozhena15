using SmartHouseManagment.Api.Extensions;
using SmartHouseManagment.Api.Middleware;
using SmartHouseManagment.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCoreServices(builder.Configuration);
builder.Services.AddScoped<ExceptionHandlingMiddleware>();
    
var app = builder.Build();

await app.AddAppServices();
app.Run();