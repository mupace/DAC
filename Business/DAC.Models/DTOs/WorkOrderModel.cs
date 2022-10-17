namespace DAC.Models.DTOs;

public class WorkOrderModel
{
    public Guid Id { get; set; }

    public string Responsible { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public IEnumerable<WorkOrderNoteModel> WorkOrderNotes { get; set; }

    public string ResponsibleName { get; set; }
}