using DAC.Business.Definitions.WorkOrders;
using DAC.DB.Models;
using DAC.Models.DTOs;

namespace DAC.Business.WorkOrders;

public class WorkOrderManager: IWorkOrderManager
{
    


    public WorkOrderModel GetWorkOrder(Guid id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<WorkOrderModel> GetWorkOrders()
    {
        throw new NotImplementedException();
    }

    public WorkOrderModel UpdateWorkOrder(WorkOrderModel workOrder)
    {
        throw new NotImplementedException();
    }

    public bool DeleteWorkOrder(Guid id)
    {
        throw new NotImplementedException();
    }
}