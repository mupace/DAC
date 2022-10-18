using System.ComponentModel.DataAnnotations;

namespace DAC.Models.RequestModels;

public class WorkOrderNoteRequestModel
{
    [Required]
    public Guid WorkOrderId { get; set; }

    [Required]
    public string Note { get; set; }
}