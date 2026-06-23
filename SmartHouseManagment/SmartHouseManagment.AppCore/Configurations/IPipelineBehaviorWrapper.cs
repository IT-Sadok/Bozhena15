namespace SmartHouseManagment.AppCore.Configurations;

public interface IPipelineBehaviorWrapper<TResponse>
{
    Task<TResponse> HandleAsync(
        IRequest<TResponse> request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken);
}
