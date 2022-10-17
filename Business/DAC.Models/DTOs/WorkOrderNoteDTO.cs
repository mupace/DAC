namespace DAC.Models.DTOs;

public class WorkOrderNoteDTO
{
    public Guid Id { get; set; }

    public string Note { get; set; }

    public Guid WorkOrderId { get; set; }

    public DateTime CreateDate { get; set; }
}