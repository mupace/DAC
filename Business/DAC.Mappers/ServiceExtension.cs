using DAC.Mappers.Definitions;
using DAC.Mappers.Implementations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace DAC.Mappers;

public static class ServiceExtension
{
    public static IServiceCollection AddDacMappers(this IServiceCollection services)
    {
        services.TryAddTransient<IWorkOrderMapper, WorkOrderMapper>();
        services.TryAddTransient<IWorkOrderNoteMapper, WorkOrderNoteMapper>();

        return services;
    }
}