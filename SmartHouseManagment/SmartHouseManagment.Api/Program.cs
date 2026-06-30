using SmartHouseManagment.Api.Extensions;
using SmartHouseManagment.Api.Middleware;
using SmartHouseManagment.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCoreServices(builder.Configuration);
builder.Services.AddExceptionHandler<ExceptionHandler>();

var app = builder.Build();

await app.AddAppServices();

var apiGroup = app.MapGroup("/api");
apiGroup.MapV1Groups();

app.Run();