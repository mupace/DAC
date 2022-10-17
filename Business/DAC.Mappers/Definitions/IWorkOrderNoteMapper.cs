using DAC.DB.Models;
using DAC.Models.DTOs;

namespace DAC.Mappers.Definitions;

public interface IWorkOrderNoteMapper
{
    WorkOrderNoteDTO DbToDto(WorkOrderNote note);

    WorkOrderNote DtoToDto(WorkOrderNoteDTO note);
}