using System.Reflection;
using MediatorBasic.Implementations;
using MediatorBasic.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace MediatorBasic;

public static class ServiceExtension
{
    public static void AddBasicMediator(this IServiceCollection serviceCollection, Assembly assembly)
    {
        foreach (var type in assembly.GetTypes())
        {
            if (type is not { IsClass: true, IsAbstract: false }) continue;
            var intr = type.GetInterfaces().FirstOrDefault(i =>
                i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>));
            if (intr is null) continue;
            serviceCollection.AddTransient(intr, type);
        }

        serviceCollection.AddTransient<IMediatorBasic, BasicMediator>();
    }
}