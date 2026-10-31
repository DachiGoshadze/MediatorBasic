namespace MediatorBasic.Interfaces;

public interface IMediatorBasic
{
    public Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request,
        CancellationToken cancellationToken = default)
        where TResponse : class;

    public Task SendAsync(IRequest request, CancellationToken cancellationToken = default);
}