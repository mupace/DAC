using DAC.Business.Definitions.WorkOrders;
using DAC.Extensions;
using DAC.Mappers.Definitions;
using DAC.Models.RequestModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DAC.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkOrderNoteController : ControllerBase
    {
        private readonly IWorkOrderNoteManager _workOrderNoteManager;
        private readonly IWorkOrderNoteMapper _workOrderNoteMapper;

        public WorkOrderNoteController(IWorkOrderNoteManager workOrderNoteManager, IWorkOrderNoteMapper workOrderNoteMapper)
        {
            _workOrderNoteManager = workOrderNoteManager;
            _workOrderNoteMapper = workOrderNoteMapper;
        }

        // GET api/<WorkOrderNoteController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Get(Guid id)
        {
            var note = _workOrderNoteManager.GetById(id);

            return note == null ? NoContent() : Ok(note);
        }

        // GET: api/<WorkOrderNoteController>
        [HttpGet("GetByWorkOrder/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult GetByWorkOrderId(Guid id)
        {
            var notes = _workOrderNoteManager.GetWorkOrderNotesOfWorkOrder(id);

            return notes != null && notes.Any() ? Ok(notes) : NoContent();
        }

        // POST api/<WorkOrderNoteController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] WorkOrderNoteRequestModel model)
        {
            if (ModelState.IsValid)
            {
                var dto = _workOrderNoteMapper.CreateRequestModelToDto(model);

                var obj = await _workOrderNoteManager.CreateWorkOrderNote(dto);

                if (obj.Id != Guid.Empty)
                    return CreatedAtAction(nameof(Get), new { id = obj.Id }, obj);

                return Problem(statusCode: StatusCodes.Status500InternalServerError, detail: "An error occurred.Please try again");
            }

            return BadRequest();
        }
        
        // DELETE api/<WorkOrderNoteController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _workOrderNoteManager.DeleteWorkOrderNote(id);

            return result.ToActionResult(this);
        }

        [HttpDelete("DeleteNodes/{workOrderId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteNotes(Guid workOrderId, [FromBody]List<Guid> ids)
        {
            var result = await _workOrderNoteManager.DeleteWorkOrderNote(ids.ToList(), workOrderId);

            return result.ToActionResult(this);
        }

        [HttpDelete("DeleteByWorkOrder/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteByWorkOrder(Guid id)
        {
            var result = await _workOrderNoteManager.DeleteWorkOrderNotesOfWorkOrder(id);

            return result.ToActionResult(this);
        }
    }
}
