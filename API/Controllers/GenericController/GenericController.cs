using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Services.UtilitySerivices;

namespace API.Controllers.GenericController
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenericController<T> : ControllerBase where T : class
    {
        private readonly IGenericCrud<T> crud;

        public GenericController(IGenericCrud<T> crud)
        {
            this.crud = crud;
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<T>>> getAllEntity()
        {
            return Ok(await crud.GetAllEntityes());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<T>> getEntityById(int id)
        {
            var entity = await crud.GetByIdEntity(id);
            if (entity == null) return NotFound();
            return Ok(entity);
        }

        [HttpPost("add")]
        public async Task<ActionResult<T>> addEntity([FromBody] T entity)
        {
            return Ok(await crud.AddEntity(entity));
        }

        [HttpPut("update")]
        public async Task<ActionResult<T>> updateEntity([FromBody] T entity)
        {
            return Ok(await crud.UpdateEntity(entity));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<T>> deleteEmtity(int id)
        {
            var tempEntity = await crud.GetByIdEntity(id);
            if (tempEntity == null) return NotFound();
            return NoContent();
        }
    }
}
