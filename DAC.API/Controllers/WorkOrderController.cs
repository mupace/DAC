using System.Net;
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
        public string Get(Guid id)
        {
            return "value";
        }

        // POST api/<WorkOrderController>
        [HttpPost]
        public HttpResponseMessage Post([FromBody] WorkOrderRequestModel model)
        {
            if (ModelState.IsValid)
            {
                var dto = _workOrderMapper.RequestModelToDto(model);

                var obj = _workOrderManager.CreateWorkOrder(dto);

                if (obj.Id != Guid.Empty)
                    return new HttpResponseMessage(HttpStatusCode.Created);

                return new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = JsonContent.Create("An error occured. Please try again")
                };
            }
            return new HttpResponseMessage(HttpStatusCode.BadRequest);
        }

        // PUT api/<WorkOrderController>/5
        [HttpPut("{id}")]
        public void Put(Guid id, [FromBody] WorkOrderRequestModel value)
        {
        }

        // DELETE api/<WorkOrderController>/5
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
        }
    }
}
