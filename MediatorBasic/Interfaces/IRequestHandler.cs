namespace MediatorBasic.Interfaces;

public interface IRequestHandler<in TRequest, TResponse> 
    where TRequest : IRequest<TResponse>
    where TResponse : class
{
    public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
}