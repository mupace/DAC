using System.ComponentModel.DataAnnotations;

namespace DAC.Models.RequestModels;

public class WorkOrderUpdateRequestModel
{
    [Required]
    public Guid Responsible { get; set; }

    [Required]
    [MaxLength(100, ErrorMessage = "Cannot be longer than 100 characters")]
    public string Name { get; set; }

    [MaxLength(256, ErrorMessage = "Cannot be longer than 256 characters")]
    public string Description { get; set; }
}