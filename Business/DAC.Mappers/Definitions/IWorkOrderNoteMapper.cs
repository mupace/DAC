using DAC.DB.Models;
using DAC.Models.DTOs;
using DAC.Models.RequestModels;

namespace DAC.Mappers.Definitions;

public interface IWorkOrderNoteMapper
{
    WorkOrderNoteDTO DbToDto(WorkOrderNote note);

    WorkOrderNote DtoToDb(WorkOrderNoteDTO note);

    WorkOrderNoteDTO CreateRequestModelToDto(WorkOrderNoteRequestModel note);
}