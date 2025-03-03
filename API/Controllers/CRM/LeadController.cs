using Microsoft.AspNetCore.Mvc;
using Services.CRMServices;
using Core;
using Microsoft.OpenApi.Writers;
using API.Controllers.GenericController;
using Services.UtilitySerivices;

namespace API.Controllers.CRM
{
    /*
     * Controller class for handling requests related to user operations.
     * This class defines end points for retrieving and manipulating user data.
     * 
     * @date :- 28-02-25
    */
    [ApiController]
    [Route("api/Lead")]
    //This controller handles all the http request and response relate to the
    //Lead class.
    public class LeadController : GenericController<Lead>
    {
        public LeadController(IGenericCrud<Lead> service) : base(service) { }
    }

}
