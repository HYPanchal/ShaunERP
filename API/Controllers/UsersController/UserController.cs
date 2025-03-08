using Core;
using Microsoft.AspNetCore.Mvc;
using Services.UtilitySerivices;
using API.Controllers.GenericController;

namespace API.Controllers.UsersController
{
    [ApiController]
    [Route("api/User")]
    public class UserController : GenericController<User>
    {
        public UserController(IGenericCrud<User> service) : base(service) { }
    }
}
