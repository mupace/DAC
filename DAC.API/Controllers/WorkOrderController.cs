using DAC.API.Models.ResponseModels;
using DAC.Business.Definitions.WorkOrders;
using DAC.Extensions;
using DAC.Mappers.Definitions;
using DAC.Models.RequestModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DAC.API.Controllers;

[Route("api/[controller]")]
[ApiController, Authorize]
public class WorkOrderController : ControllerBase
{
    private readonly IWorkOrderManager _workOrderManager;
    private readonly IWorkOrderMapper _workOrderMapper;

    public WorkOrderController(IWorkOrderManager workOrderManager, IWorkOrderMapper workOrderMapper)
    {
        _workOrderManager = workOrderManager;
        _workOrderMapper = workOrderMapper;
    }

    // GET: api/<WorkOrderController>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Get(int pageSize = 50, int page = 0)
    {
        var totalItems = _workOrderManager.GetWorkOrders().Count();
        var skip = pageSize * page;

        if (totalItems == 0)
            return Ok(new PagedWorkOrdersResponseModel()
            {
                CurrentPage = page,
                PageSize = pageSize,
                TotalPages = 0,
                WorkOrders = null
            });

        if (totalItems <= skip)
            return BadRequest("Out of bounds");

        var workOrders = _workOrderManager.GetWorkOrders()
            .Skip(skip)
            .Take(pageSize)
            .AsEnumerable()
            .Select(_workOrderMapper.DbToDto)
            .ToList();

        return Ok(new PagedWorkOrdersResponseModel
        {
            CurrentPage = page,
            PageSize = pageSize,
            TotalPages = totalItems % pageSize == 0 ? totalItems / pageSize : totalItems / pageSize + 1,
            WorkOrders = workOrders
        });
    }

    // GET api/<WorkOrderController>/5
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Get(Guid id)
    {
        var workOrder = _workOrderManager.GetWorkOrder(id);

        return workOrder == null ? NoContent() : Ok(workOrder);
    }

    // POST api/<WorkOrderController>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Post([FromBody] WorkOrderCreateRequestModel model)
    {
        if (ModelState.IsValid)
        {
            var dto = _workOrderMapper.CreateRequestModelToDto(model);

            var obj = await _workOrderManager.CreateWorkOrder(dto);

            if (obj.Id != Guid.Empty)
                return CreatedAtAction(nameof(Get), new { id = obj.Id }, obj);

            return Problem(statusCode: StatusCodes.Status500InternalServerError, detail: "An error occured.Please try again");
        }

        return BadRequest();
    }

    // PUT api/<WorkOrderController>/5
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Put(Guid id, [FromBody] WorkOrderUpdateRequestModel value)
    {
        if (ModelState.IsValid)
        {
            var dto = _workOrderMapper.UpdateRequestModelToDto(value);

            dto.Id = id;

            var workOrder = await _workOrderManager.UpdateWorkOrder(dto);

            return workOrder.OperationResult.ToActionResult(this);
        }

        return BadRequest();
    }

    // DELETE api/<WorkOrderController>/5
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _workOrderManager.DeleteWorkOrder(id);

        return result.ToActionResult(this);
    }
}