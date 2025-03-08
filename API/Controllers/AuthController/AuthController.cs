using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Services.UtilitySerivices;
using System.Threading.Tasks;
using Core;

namespace API.Controllers.AuthController
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;

        private readonly IServiceProvider _serviceProvider;

        private readonly IGenericCrud<Lead> _genericService;

        public AuthController(IConfiguration config, IServiceProvider serviceProvider, IGenericCrud<Lead> service)
        {
            _config = config;
            _serviceProvider = serviceProvider;
            _genericService = service;
        }

        //[HttpPost("login/{entitytype}/{id}")]
        [HttpPost("login/{id}")]
        public async Task<IActionResult> Login(int id)
        {
            //var genericServiceType = typeof(IGenericCrud<>).MakeGenericType(Type.GetType(entitytype));

            //var service = _serviceProvider.GetService(genericServiceType) as dynamic;

            //if(service == null) { return BadRequest(new { message = "Invalid entity type" }); }

            var temp = _genericService.GetByIdEntity(id);

            if (temp == null) { return NotFound(new { message = "Entity not found" }); }

            var token = GenerateJwtToken(id);
                
            return Ok(token);
        }

        private string GenerateJwtToken(int userid)
        {
            var jwtSettings = _config.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, userid.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                jwtSettings["Issuer"],
                jwtSettings["Audience"],
                claims,
                expires: DateTime.UtcNow.AddSeconds(30),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
