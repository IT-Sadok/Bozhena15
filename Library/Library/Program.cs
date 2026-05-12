using Library.Services;
using Microsoft.Extensions.DependencyInjection;

var services = ConfigureServices();
var provider = services.BuildServiceProvider();

var consoleMenuService = provider.GetRequiredService<IConsoleMenuService>();
var fileService = provider.GetRequiredService<IFileService>();

fileService.CreateDataFilesIfNotExist();
consoleMenuService.ShowConsoleMenu();

return;

static IServiceCollection ConfigureServices()
{
    var services = new ServiceCollection();
    
    services.AddSingleton<IConsoleMenuService, ConsoleMenuService>();
    services.AddSingleton<IFileService, FileService>();
    services.AddTransient<IBookService, BookService>();
    services.AddTransient<IRepository, Repository>();
    services.AddTransient<IInputConsoleService, InputConsoleService>();

    services.AddLogging();
    
    return services;
}