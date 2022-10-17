using System.ComponentModel.DataAnnotations;
using DAC.Models.DTOs;

namespace DAC.Models.RequestModels;

public class WorkOrderRequestModel
{
    [Required]
    public Guid Id { get; set; }

    [Required] 
    public string Responsible { get; set; }

    [Required]
    [MaxLength(100, ErrorMessage = "Cannot be longer than 100 characters")]
    public string Name { get; set; }

    [MaxLength(256, ErrorMessage = "Cannot be longer than 100 characters")]
    public string Description { get; set; }

    [Required] public DateTime CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public IEnumerable<WorkOrderNoteDTO> WorkOrderNotes { get; set; }

    public string ResponsibleName { get; set; }
}