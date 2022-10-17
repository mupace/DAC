using System.Data;
using Microsoft.Extensions.DependencyInjection;

namespace DAC.Extensions;

public static class ServiceCollectionExtensions
{
    public static ServiceDescriptor GetServiceDescription<T>(this IServiceCollection services)
    {
        return services.FirstOrDefault(x => x.ServiceType == typeof(T));
    }

    public static IServiceCollection RemoveService<T>(this IServiceCollection services)
    {
        if (services.IsReadOnly)
        {
            throw new ReadOnlyException($"{nameof(services)} is read only");
        }

        var serviceDescriptor = services.GetServiceDescription<T>();

        if (serviceDescriptor != null)
        {
            services.Remove(serviceDescriptor);
        }

        return services;
    }
}