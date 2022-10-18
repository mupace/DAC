using DAC.Business.Definitions.WorkOrders;
using DAC.DB.Models;
using DAC.Mappers.Definitions;
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

    public IQueryable<Workorder> GetWorkOrders()
    {
        return _dacDbContext.Workorders.AsQueryable();
    }

    public async Task<WorkOrderDTO> CreateWorkOrder(WorkOrderDTO workOrder)
    {
        var dbModel = _workOrderMapper.DtoToDb(workOrder);

        dbModel.CreateDate = DateTime.UtcNow;

        try
        {
            _dacDbContext.Workorders.Add(dbModel);
            await _dacDbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occured creating Work Order");
            return workOrder;
        }

        return _workOrderMapper.DbToDto(dbModel);
    }

    public WorkOrderDTO UpdateWorkOrder(WorkOrderDTO workOrder)
    {
        var dbModel = _workOrderMapper.DtoToDb(workOrder);

        try
        {
            _dacDbContext.Workorders.Update(dbModel);
            _dacDbContext.SaveChanges();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occured on WorkOrderManager Update");
        }

        return _workOrderMapper.DbToDto(dbModel);
    }

    public bool DeleteWorkOrder(Guid id)
    {
        var entity = GetWorkOrders().FirstOrDefault(x => x.Id == id);

        if (entity == null)
            return false;

        _dacDbContext.Workorders.Remove(entity);
        return true;
    }
}