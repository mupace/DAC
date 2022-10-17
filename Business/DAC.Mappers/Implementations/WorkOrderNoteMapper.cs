using DAC.DB.Models;
using DAC.Mappers.Definitions;
using DAC.Models.DTOs;

namespace DAC.Mappers.Implementations;

public class WorkOrderNoteMapper : IWorkOrderNoteMapper
{
    public WorkOrderNoteDTO DbToDto(WorkOrderNote note)
    {
        return new WorkOrderNoteDTO
        {
            Id = note.Id,
            WorkOrderId = note.WorkOrderId,
            CreateDate = note.CreateDate,
            Note = note.Note
        };
    }

    public WorkOrderNote DtoToDto(WorkOrderNoteDTO note)
    {
        return new WorkOrderNote
        {
            Id = note.Id,
            Note = note.Note,
            WorkOrderId = note.WorkOrderId,
            CreateDate = note.CreateDate
        };
    }
}