using Microsoft.Extensions.DependencyInjection;

namespace SmartHouseManagment.AppCore.Configurations;

public class Mediator(
    IServiceProvider serviceProvider) : IMediator
{
    public async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
    {
        var requestType = request.GetType();

        var handlerType = typeof(IRequestHandler<,>).MakeGenericType(requestType, typeof(TResponse));
        var handler = serviceProvider.GetRequiredService(handlerType);

        RequestHandlerDelegate<TResponse> handlerDelegate = () =>
        {
            var handleMethod = handlerType.GetMethod(nameof(IRequestHandler<IRequest<TResponse>, TResponse>.HandleAsync))!;
            return (Task<TResponse>)handleMethod.Invoke(handler, [request, cancellationToken])!;
        };

        var behaviorType = typeof(IPipelineBehavior<,>).MakeGenericType(requestType, typeof(TResponse));
        var wrapperType = typeof(PipelineBehaviorWrapper<,>).MakeGenericType(requestType, typeof(TResponse)); 

        var behaviors = serviceProvider.GetServices(behaviorType)
            .Select(b => (IPipelineBehaviorWrapper<TResponse>)Activator.CreateInstance(wrapperType, b)!)
            .Reverse();

        foreach (var behavior in behaviors)
        {
            var next = handlerDelegate;
            handlerDelegate = () => behavior.HandleAsync(request, next, cancellationToken);
        }

        return await handlerDelegate();
    }
}
