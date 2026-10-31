namespace MediatorBasic.Interfaces;

public interface IRequest<out TResponse> : IBasicRequest { }

public interface IRequest : IBasicRequest { }

public interface IBasicRequest { }