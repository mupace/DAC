using DAC.Models.DTOs;

namespace DAC.Business.Definitions.WorkOrders;

public interface IWorkOrderManager
{
    /// <summary>
    ///     Query work order with its id
    /// </summary>
    /// <param name="id">Guid value representing work order</param>
    /// <returns>Work order model - null if cannot find it</returns>
    WorkOrderModel GetWorkOrder(Guid id);

    /// <summary>
    ///     Get work orders
    /// </summary>
    /// <returns>Returns all work orders over cache</returns>
    IEnumerable<WorkOrderModel> GetWorkOrders();

    /// <summary>
    ///     Overwrites existing work order with new object
    /// </summary>
    /// <param name="workOrder">Updated object</param>
    /// <returns>Updated object</returns>
    WorkOrderModel UpdateWorkOrder(WorkOrderModel workOrder);

    /// <summary>
    ///     Delete work order with given id
    /// </summary>
    /// <param name="id">Guid value representing work order</param>
    /// <returns>Returns true if successful</returns>
    bool DeleteWorkOrder(Guid id);
}