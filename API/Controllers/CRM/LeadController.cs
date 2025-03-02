using Microsoft.AspNetCore.Mvc;
using Services.CRMServices;
using Core;
using Microsoft.OpenApi.Writers;

namespace API.Controllers.CRM
{
    /*
     * Controller class for handling requests related to user operations.
     * This class defines end points for retrieving and manipulating user data.
     * 
     * @date :- 28-02-25
    */
    [Route("api/Lead")]
    [ApiController]
    //This controller handles all the http request and response relate to the
    //Lead class.
    public class LeadController : Controller
    {
        //crud variable of type CrudServices class to access the methods.
        private readonly CrudServices crud;
        //lead variable of type Lead class to access the methods.
        private readonly Lead lead;

        //LeadController constructor for constructor injection.
        public LeadController(CrudServices crudServices)
        {
            this.crud = crudServices;
        }

        /*
         * This is get request method to get specific Lead object by it's Id.
         * 
         * @param id:- To get the unique id.
         * 
         * @return :- return specific Lead Object.
         * 
         * @date :- 28-02-25
         */
        [HttpGet("{id}")]
        public IActionResult getLeadById(int id)
        {
            return Ok(crud.getLeadById(id));
        }

        /*
         * This is get request method to get all Lead object.
         * 
         * @return :- return list of Lead Object.
         * 
         * @date :- 28-02-25
         */
        [HttpGet("all")]
        public IActionResult getAllLeads()
        {
            return Ok(crud.getAllLead());
        }

        /*
         * This is post request method to save the Lead object.
         * 
         * @param lead:- To get the Lead object.
         * 
         * @return :- return Lead Object if saved.
         * 
         * @date :- 28-02-25
         */
        [HttpPost]
        public IActionResult saveLead([FromBody] Lead lead)
        {
            return Ok(crud.addlead(lead));
        }

        /*
         * This is put request method to update existing Lead object by it's Id.
         * 
         * @param id:- To get the unique id.
         * 
         * @param updaeLead :- To get he updated Lead object.
         * 
         * @return :- return the updated Lead Object if it's updated.
         * 
         * @date :- 01-03-25
         */
        [HttpPut("{id}")]
        public IActionResult updateLead(int id, Lead updateLead)
        {
            return Ok(crud.updateLead(id, updateLead));
        }

        /*
         * This is delete request method to delete specific Lead object by it's Id.
         * 
         * @param id:- To get the unique id.
         * 
         * @return :- return the Lead Object if it is deleted.
         * 
         * @date :- 01-03-25
         */
        [HttpDelete("{id}")]
        public IActionResult deleteLead(int id)
        {
            return Ok(crud.deleteLead(id));
        }
    }

}
