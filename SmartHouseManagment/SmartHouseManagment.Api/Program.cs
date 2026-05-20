using SmartHouseManagment.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCoreServices(builder.Configuration);
    
var app = builder.Build();

app.UseHttpsRedirection();
app.Run();
