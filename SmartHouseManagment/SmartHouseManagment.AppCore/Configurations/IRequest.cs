namespace SmartHouseManagment.AppCore.Configurations;

public interface IBaseRequest;

public interface IRequest<out TResponse> : IBaseRequest;

public interface IRequestHandler<in TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    Task<TResponse> HandleAsync(TRequest request, CancellationToken cancellationToken);
}

public interface IRequestHandler<TResponse>
{
    Task<TResponse> HandleAsync(IRequest<TResponse> request, CancellationToken ct);
}
