namespace SmartHouseManagment.AppCore.Configurations;

public class PipelineBehaviorWrapper<TRequest, TResponse>(
    IRequestHandler<TRequest, TResponse> handler,
    IEnumerable<IPipelineBehavior<TRequest, TResponse>> behaviors)
    : IRequestHandler<TResponse>
    where TRequest : IRequest<TResponse>
{
    public Task<TResponse> HandleAsync(IRequest<TResponse> request, CancellationToken cancellationToken)
    {
        RequestHandlerDelegate<TResponse> pipeline =
            () => handler.HandleAsync((TRequest)request, cancellationToken);

        foreach (var behavior in behaviors.Reverse())
        {
            var next = pipeline;
            pipeline = () => behavior.HandleAsync((TRequest)request, next, cancellationToken);
        }

        return pipeline();
    }
}
