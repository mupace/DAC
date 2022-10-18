using DAC.DB.Models;
using DAC.Mappers.Definitions;
using DAC.Models.DTOs;
using DAC.Models.RequestModels;

namespace DAC.Mappers.Implementations;

public class WorkOrderMapper : IWorkOrderMapper
{
    public WorkOrderDTO DbToDto(Workorder order)
    {
        return new WorkOrderDTO
        {
            Id = order.Id,
            Name = order.Name,
            Description = order.Description,
            CreateDate = order.CreateDate,
            UpdateDate = order.UpdateDate,
            Responsible = order.Responsible
        };
    }

    public Workorder DtoToDb(WorkOrderDTO order)
    {
        return new Workorder
        {
            Id = order.Id,
            Responsible = order.Responsible,
            Name = order.Name,
            Description = order.Description,
            CreateDate = order.CreateDate,
            UpdateDate = order.UpdateDate
        };
    }

    public WorkOrderDTO RequestModelToDto(WorkOrderCreateRequestModel orderCreate)
    {
        return new WorkOrderDTO
        {
            Name = orderCreate.Name,
            Description = orderCreate.Description,
            Responsible = orderCreate.Responsible.ToString()
        };
    }
}