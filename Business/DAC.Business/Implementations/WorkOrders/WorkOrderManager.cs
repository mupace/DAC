using DAC.Business.Definitions.WorkOrders;
using DAC.Constants.enums;
using DAC.DB.Models;
using DAC.Mappers.Definitions;
using DAC.Models;
using DAC.Models.DTOs;
using Microsoft.Extensions.Logging;

namespace DAC.Business.WorkOrders;

public class WorkOrderManager: BusinessBase, IWorkOrderManager
{
    private readonly IWorkOrderMapper _workOrderMapper;

    public WorkOrderManager(IWorkOrderMapper workOrderMapper, DACDBContext dacDbContext, ILogger<WorkOrderManager> logger) : base(dacDbContext, logger)
    {
        _workOrderMapper = workOrderMapper;
    }

    public WorkOrderDTO GetWorkOrder(Guid id)
    {
        var entity = _dacDbContext.Workorders.FirstOrDefault(x => x.Id == id);

        return entity == null ? null : _workOrderMapper.DbToDto(entity);
    }

    private Workorder GetById(Guid id)
    {
        return _dacDbContext.Workorders.FirstOrDefault(x => x.Id == id);
    }

    public IQueryable<Workorder> GetWorkOrders()
    {
        return _dacDbContext.Workorders.AsQueryable();
    }

    public async Task<WorkOrderDTO> CreateWorkOrder(WorkOrderDTO workOrder)
    {
        var dbModel = _workOrderMapper.DtoToDb(workOrder);

        dbModel.Id = Guid.NewGuid();
        dbModel.CreateDate = DateTime.UtcNow;

        try
        {
            _dacDbContext.Workorders.Add(dbModel);
            await _dacDbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occured Work Order/Create");
            return workOrder;
        }

        return _workOrderMapper.DbToDto(dbModel);
    }

    public async Task<OperationResultModel<WorkOrderDTO>> UpdateWorkOrder(WorkOrderDTO workOrder)
    {
        var entity = GetById(workOrder.Id);

        if (entity == null)
            return new OperationResultModel<WorkOrderDTO>(OperationResult.NotFound, workOrder);

        entity.Responsible = workOrder.Responsible;
        entity.Name = workOrder.Name;
        entity.Description = workOrder.Description;
        entity.UpdateDate = workOrder.UpdateDate;

        try
        {
            _dacDbContext.Workorders.Update(entity);
            await _dacDbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occured on WorkOrderManager/Update");
            return new OperationResultModel<WorkOrderDTO>(OperationResult.Error, workOrder);
        }

        return new OperationResultModel<WorkOrderDTO>(OperationResult.Done, _workOrderMapper.DbToDto(entity));
    }

    public async Task<OperationResult> DeleteWorkOrder(Guid id)
    {
        var entity = GetById(id);

        if (entity == null)
            return OperationResult.NotFound;
        try
        {
            _dacDbContext.Workorders.Remove(entity);
            await _dacDbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occured on WorkOrderManager/Delete");
            return OperationResult.Error;
        }
        
        return OperationResult.Done;
    }
}