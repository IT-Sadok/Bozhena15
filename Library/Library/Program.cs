using Library.Helpers;
using Library.Services;
using Microsoft.Extensions.DependencyInjection;

FileHelper.CreateDataFilesIfNotExist();

var services = ConfigureServices();
var provider = services.BuildServiceProvider();

var consoleMenuService = provider.GetRequiredService<IConsoleMenuService>();
consoleMenuService.ShowConsoleMenu();

return;

static IServiceCollection ConfigureServices()
{
    var services = new ServiceCollection();
    
    services.AddSingleton<IConsoleMenuService, ConsoleMenuService>();
    services.AddTransient<IBookService, BookService>();
    services.AddTransient<IRepositoryService, RepositoryService>();
    services.AddTransient<IInputConsoleService, InputConsoleService>();

    return services;
}