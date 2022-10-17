using DAC.Mappers.Definitions;
using DAC.Mappers.Implementations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace DAC.Mappers;

public static class ServiceExtension
{
    public static IServiceCollection AddDacMappers(this IServiceCollection services)
    {
        
        services.TryAddSingleton<IWorkOrderMapper, WorkOrderMapper>();

        //services.AddSingleton<IWorkOrderMapper, WorkOrderMapper>();
        services.TryAddSingleton<IWorkOrderNoteMapper, WorkOrderNoteMapper>();

        return services;
    }
}