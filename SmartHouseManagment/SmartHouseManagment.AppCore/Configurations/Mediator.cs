using Microsoft.Extensions.DependencyInjection;

namespace SmartHouseManagment.AppCore.Configurations;

public class Mediator(
    IServiceProvider serviceProvider) : IMediator
{
    public Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
    {
        var wrapper = serviceProvider.GetRequiredService<IRequestHandler<TResponse>>();
        return wrapper.HandleAsync(request, cancellationToken);
    }
}
