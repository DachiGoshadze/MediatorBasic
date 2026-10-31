using MediatorBasic.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace MediatorBasic.Implementations;

public class BasicMediator(IServiceProvider serviceProvider) : IMediatorBasic
{
    public async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request,
        CancellationToken cancellationToken = default) where TResponse : class
    {
        var type = typeof(IRequestHandler<,>).MakeGenericType(request.GetType(), typeof(TResponse));

        var serviceObj = serviceProvider.GetServices(type);

        if (serviceObj is null) throw new InvalidOperationException("Request Handler Not Found");

        var serviceArr = serviceObj as object?[] ?? serviceObj.ToArray();

        if (serviceArr.Length > 1)
            throw new InvalidOperationException("More Then One Same Request/Response Handler Not Allowed");

        dynamic handler = serviceArr.FirstOrDefault()!;

        TResponse response = await handler.Handle((dynamic)request, cancellationToken);

        return response;
    }

    public async Task SendAsync(IRequest request, CancellationToken cancellationToken = default)
    {
        var type = typeof(IRequestHandler<,>).MakeGenericType(request.GetType(), typeof(void));

        var serviceObj = serviceProvider.GetRequiredService(type);

        dynamic handler = serviceObj;

        await handler.Handle((dynamic)request, cancellationToken);
    }
}