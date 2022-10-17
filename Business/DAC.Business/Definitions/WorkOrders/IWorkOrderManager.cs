using DAC.DB.Models;
using DAC.Models.DTOs;

namespace DAC.Business.Definitions.WorkOrders;

public interface IWorkOrderManager
{
    /// <summary>
    ///     Query work order with its id
    /// </summary>
    /// <param name="id">Guid value representing work order</param>
    /// <returns>Work order model - null if cannot find it</returns>
    WorkOrderDTO GetWorkOrder(Guid id);

    /// <summary>
    ///     Get work orders
    /// </summary>
    /// <returns></returns>
    IQueryable<Workorder> GetWorkOrders();

    /// <summary>
    /// Creates a new work order
    /// </summary>
    /// <param name="workOrder">WorkOrder DTO object</param>
    /// <returns></returns>
    WorkOrderDTO CreateWorkOrder(WorkOrderDTO workOrder);

    /// <summary>
    ///     Overwrites existing work order with new object
    /// </summary>
    /// <param name="workOrder">Updated object</param>
    /// <returns>Updated object</returns>
    WorkOrderDTO UpdateWorkOrder(WorkOrderDTO workOrder);

    /// <summary>
    ///     Delete work order with given id
    /// </summary>
    /// <param name="id">Guid value representing work order</param>
    /// <returns>Returns true if successful</returns>
    bool DeleteWorkOrder(Guid id);
}