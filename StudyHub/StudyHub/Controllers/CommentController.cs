using System.ComponentModel;
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
    public class CommentController : ControllerBase
    {
        private readonly ICommentService service;

        public CommentController(ICommentService service)
        {
            this.service = service;
        }
        [HttpPost("AddComment")]
        public async Task<ActionResult<Comment>> AddComment(CommentDto request)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return BadRequest("User not found!!");
            }
            var commentData = await this.service.AddCommentAsync(userId, request);

            return Ok(new { message = "Comment Added!!" });
        }
        [HttpGet("getcomments")]
        public async Task<List<GetCommentDto>> GetCommentByPost(int postId)
        {
            var comment = await service.GetCommentByPostAsync(postId);
            return (comment);

        }
        [HttpDelete("deletecomment")]
        public async Task<ActionResult<Comment>> DeleteComment(int id)
        { 
             var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var deleteCommnet= await this.service.DeleteCommentAsync(id, userId);
            return Ok(new { message = "Comment Deleted!" });
        }
    }
}
