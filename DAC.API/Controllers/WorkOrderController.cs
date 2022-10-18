using System.Net;
using System.Reflection.Metadata.Ecma335;
using DAC.Business.Definitions.WorkOrders;
using DAC.Mappers.Definitions;
using DAC.Models.RequestModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DAC.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<WorkOrderController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {

            return Content("asd");
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
                var dto = _workOrderMapper.RequestModelToDto(model);

                var obj = await _workOrderManager.CreateWorkOrder(dto);

                if (obj.Id != Guid.Empty)
                    return CreatedAtAction(nameof(Get), new {id = obj.Id}, obj);

                return Problem(statusCode: StatusCodes.Status500InternalServerError, detail: "An error occured.Please try again");
            }

            return BadRequest();
        }

        // PUT api/<WorkOrderController>/5
        [HttpPut("{id}")]
        public void Put(Guid id, [FromBody] WorkOrderCreateRequestModel value)
        {
        }

        // DELETE api/<WorkOrderController>/5
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
        }
    }
}
