using DAC.Business.Definitions.WorkOrders;
using DAC.Business.WorkOrders;
using DAC.Mappers;
using Microsoft.Extensions.DependencyInjection;

namespace DAC.Business;

public static class ServiceExtension
{
    public static IServiceCollection AddDacBusiness(this IServiceCollection services)
    {
        //Add dependencies
        services = services.AddDacMappers();

        //Add self services
        services.AddSingleton<IWorkOrderManager, WorkOrderManager>();

        return services;
    }
}