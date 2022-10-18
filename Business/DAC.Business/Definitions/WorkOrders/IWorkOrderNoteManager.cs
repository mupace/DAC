using DAC.Constants.enums;
using DAC.DB.Models;
using DAC.Models.DTOs;

namespace DAC.Business.Definitions.WorkOrders;

public interface IWorkOrderNoteManager
{
    /// <summary>
    ///     Query by unique id
    /// </summary>
    /// <param name="id">Work Order Note Id</param>
    /// <returns>Returns null if not found</returns>
    WorkOrderNoteDTO? GetById(Guid id);

    /// <summary>
    ///     Exposes WorkOrderNotes table as queryable
    /// </summary>
    /// <returns></returns>
    IQueryable<WorkOrderNote> GetWorkOrderNotes();

    /// <summary>
    ///     Query notes by work order id
    /// </summary>
    /// <param name="workOrderId">Work Order Id</param>
    /// <returns></returns>
    IEnumerable<WorkOrderNoteDTO>? GetWorkOrderNotesOfWorkOrder(Guid workOrderId);

    /// <summary>
    ///     dee
    ///     Creates a new work order note
    /// </summary>
    /// <param name="note"></param>
    /// <returns>Returns created DTO on success</returns>
    Task<WorkOrderNoteDTO> CreateWorkOrderNote(WorkOrderNoteDTO note);

    /// <summary>
    ///     Deletes single work order note
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Returns OperationResult</returns>
    Task<OperationResult> DeleteWorkOrderNote(Guid id);

    /// <summary>
    ///     Deletes all work order notes given in parameter, must belong to same work order
    /// </summary>
    /// <param name="noteIdList">Note Id list</param>
    /// <param name="workOrderId">Work Order Id</param>
    /// <returns>Returns OperationResult</returns>
    Task<OperationResult> DeleteWorkOrderNote(List<Guid> noteIdList, Guid workOrderId);


    /// <summary>
    ///     Deletes all notes of work order
    /// </summary>
    /// <param name="workOrderId"></param>
    /// <returns>Returns OperationResult</returns>
    Task<OperationResult> DeleteWorkOrderNotesOfWorkOrder(Guid workOrderId);
}