using DAC.Business.Definitions.WorkOrders;
using DAC.Constants.enums;
using DAC.DB.Models;
using DAC.Mappers.Definitions;
using DAC.Mappers.Implementations;
using DAC.Models;
using DAC.Models.DTOs;
using DAC.Models.RequestModels;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace DAC.Business.Implementations.WorkOrders;

public class WorkOrderNoteManager : BusinessBase, IWorkOrderNoteManager
{
    private readonly IWorkOrderNoteMapper _workOrderNoteMapper;

    public WorkOrderNoteManager(IWorkOrderNoteMapper workOrderNoteMapper, DACDBContext dacDbContext, ILogger<WorkOrderNoteManager> logger) : base(dacDbContext, logger)
    {
        _workOrderNoteMapper = workOrderNoteMapper;
    }

    private WorkOrderNote? GetWorkOrderNote(Guid id)
    {
        return _dacDbContext.WorkOrderNotes.FirstOrDefault(x => x.Id == id);
    }

    public WorkOrderNoteDTO? GetById(Guid id)
    {
        var note = GetWorkOrderNote(id);

        return note == null ? null : _workOrderNoteMapper.DbToDto(note);
    }

    public IQueryable<WorkOrderNote> GetWorkOrderNotes()
    {
        return _dacDbContext.WorkOrderNotes.AsQueryable();
    }

    public IEnumerable<WorkOrderNoteDTO>? GetWorkOrderNotesOfWorkOrder(Guid workOrderId)
    {
        var notes = _dacDbContext.WorkOrderNotes.Where(x => x.WorkOrderId == workOrderId).ToList();

        return notes.Any() ? notes.Select(_workOrderNoteMapper.DbToDto).OrderBy(x => x.CreateDate).ToList() : null;
    }

    public async Task<WorkOrderNoteDTO> CreateWorkOrderNote(WorkOrderNoteDTO note)
    {
        var dbModel = _workOrderNoteMapper.DtoToDb(note);

        dbModel.Id = Guid.NewGuid();
        dbModel.CreateDate = DateTime.UtcNow;

        try
        {
            _dacDbContext.WorkOrderNotes.Add(dbModel);
            await _dacDbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred Work Order/Create");
            return note;
        }

        return _workOrderNoteMapper.DbToDto(dbModel);


    }

    public async Task<OperationResult> DeleteWorkOrderNote(Guid id)
    {
        var entity = GetWorkOrderNote(id);

        if (entity == null)
            return OperationResult.NotFound;
        try
        {
            _dacDbContext.WorkOrderNotes.Remove(entity);
            await _dacDbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occured on WorkOrderManager/Delete");
            return OperationResult.Error;
        }

        return OperationResult.Done;
    }

    public async Task<OperationResult> DeleteWorkOrderNote(List<Guid> noteIdList, Guid workOrderId)
    {
        var entities = GetWorkOrderNotes().Where(x => noteIdList.Contains(x.Id)).ToList();

        if (entities.Count() != noteIdList.Count)
        {
            return OperationResult.NotFound;
        }

        if (entities.Any(x => x.WorkOrderId != workOrderId))
        {
            return OperationResult.NotFound;
        }

        try
        {
            _dacDbContext.WorkOrderNotes.RemoveRange(entities);
            await _dacDbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred on WorkOrderNoteManager/DeleteWorkOrderNote bulk");
            return OperationResult.Error;
        }

        return OperationResult.Done;
    }

    public async Task<OperationResult> DeleteWorkOrderNotesOfWorkOrder(Guid workOrderId)
    {
        var entities = GetWorkOrderNotes().Where(x => x.WorkOrderId == workOrderId).ToList();

        if (!entities.Any())
        {
            return OperationResult.NotFound;
        }

        try
        {
            _dacDbContext.WorkOrderNotes.RemoveRange(entities);
            await _dacDbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred on WorkOrderNoteManager/DeleteWorkOrderNotesOfWorkOrder");
            return OperationResult.Error;
        }

        return OperationResult.Done;
    }
}