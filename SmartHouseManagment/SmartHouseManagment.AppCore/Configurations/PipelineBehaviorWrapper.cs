using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHouseManagment.AppCore.Configurations;

public class PipelineBehaviorWrapper<TRequest, TResponse>(
    IPipelineBehavior<TRequest, TResponse> behavior)
    : IPipelineBehaviorWrapper<TResponse> where TRequest : IRequest<TResponse>
{
    public Task<TResponse> HandleAsync(
        IRequest<TResponse> request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
        => behavior.HandleAsync((TRequest)request, next, cancellationToken);
}
