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
    public class CommentReplyController : ControllerBase
    {
        private readonly ICommentReplyService service;

        public CommentReplyController(ICommentReplyService service)
        {
            this.service = service;
        }

        [HttpPost("AddReply")]
        public async Task<ActionResult<CommentReply>> AddComment(CommentReplyDto request,int replyId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return BadRequest("User not found!!");
            }
            var commentReplyData = await this.service.AddCommentReplyAsync(userId,  replyId, request);

            return Ok(new { message = "Reply Added!!" });
        }
        [HttpGet("getcommentreply")]
        public async Task<List<GetCommentReplyDto>> GetCommentByPost(int commentId)
        {
            var commentReply = await service.GetCommentReplyByCommentAsync(commentId);
            return (commentReply);

        }
        [HttpDelete("deletecommentreply")]
        public async Task<ActionResult<Comment>> DeleteCommentReply(int id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var deleteCommnetReply = await this.service.DeleteCommentReplyAsync(id, userId);
            return Ok(new { message = "Comment Deleted!" });
        }
    }
}
