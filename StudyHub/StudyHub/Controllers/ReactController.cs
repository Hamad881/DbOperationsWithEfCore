using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudyHub.Entities;
using StudyHub.Model;
using StudyHub.Services;

namespace StudyHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReactController : ControllerBase
    {
        private readonly IReactService service;

        public ReactController(IReactService service)
        {
            this.service = service;
        }

        [HttpPost("addReact/{id:int}")]
        public async Task<ActionResult<React>> AddReactToPost([FromRoute] int id, [FromBody] ReactDto request)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var result = await service.AddReactAsync(id, userId, request);
            if (result == null) { return BadRequest(new { message = "Something Went Wrong!" }); }
            return Ok(new {message="React Added!"});
        }

        [HttpGet("reactCount/{id:int}")]
        public async Task<ActionResult<ReactCountDto>> GetReactByPostId([FromRoute] int id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var result = await service.GetReactByPostIdAsync(id,userId);
            return Ok(result);
        }
        [HttpDelete("removeReact/{id:int}")]

        public async Task<ActionResult<React?>> RemoveReact([FromRoute]int id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var result = await service.RemoveReactAsync(id,userId);
            return Ok(new { message = "React removed!!" });
        }




    }
}
