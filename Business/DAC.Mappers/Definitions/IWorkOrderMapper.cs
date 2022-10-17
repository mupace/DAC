using DAC.DB.Models;
using DAC.Models.DTOs;
using DAC.Models.RequestModels;

namespace DAC.Mappers.Definitions;

public interface IWorkOrderMapper
{
    WorkOrderDTO DbToDto(Workorder order);

    Workorder DtoToDb(WorkOrderDTO order);

    WorkOrderDTO RequestModelToDto(WorkOrderRequestModel order);
}