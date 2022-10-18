using DAC.Models.DTOs;

namespace DAC.Models.ResponseModels;

public class PagedWorkOrdersResponseModel
{
    public int PageSize { get; set; }

    public int CurrentPage { get; set; }

    public int TotalPages { get; set; }

    public List<WorkOrderDTO> WorkOrders { get; set; }
}