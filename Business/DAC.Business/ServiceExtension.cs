using DAC.Business.Definitions.WorkOrders;
using DAC.Business.Implementations.WorkOrders;
using DAC.Mappers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace DAC.Business;

public static class ServiceExtension
{
    public static IServiceCollection AddDacBusiness(this IServiceCollection services)
    {
        //Add dependencies
        services = services.AddDacMappers();

        //Add self services
        services.TryAddScoped<IWorkOrderManager, WorkOrderManager>();
        services.TryAddScoped<IWorkOrderNoteManager, WorkOrderNoteManager>();

        return services;
    }
}