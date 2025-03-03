using API.Controllers.GenericController;
using Microsoft.AspNetCore.Mvc;
using Services.UtilitySerivices;

namespace API.Controllers.CRM
{
    [ApiController]
    [Route("api/Task")]
    public class TaskController : GenericController<Core.Task>
    {
        public TaskController(IGenericCrud<Core.Task> service) : base(service) { }
    }
}
