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
    public class PostController : ControllerBase
    {
        private readonly IPostService service;

        public PostController(IPostService service)
        {
            this.service = service;
        }
        [HttpPost("Add")] 
        public async Task<ActionResult<Post>> AddPost(PostDto datapost)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return BadRequest("User not found!!");
            }
            var postData = await this.service.AddPostTData(userId,datapost);
            return Ok(new { message = "Post Added!!" });
        }

        [HttpPut("update/{id:int}" )]
        public async Task<ActionResult<Post>> UpdatePostData([FromRoute]int id, [FromBody]PostDto request)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var updated = await service.UpdatePost(userId,id,request);
            if (updated == null)
            {
                return BadRequest(new { message = "Something went wrong!" });
            }
            return Ok(new { message = "Post Updated!!" });
        }
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<Post>> DeletePost([FromRoute]int id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var delPost= await service.DeletePostAsync(userId,id);
            if (delPost == null)
            {
                return BadRequest("Something went wrong");
            }
            return Ok(new { message = "Post Deleted" });
        }

        [HttpGet("UserOnly")]
        public async Task<List<GetPostDto>> GetCurrUserPosts()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userPosts = await service.GetPostByIdAsync(userId);
            return userPosts;

        }

        [HttpGet("all")]
        public async Task<List<GetPostDto>> GetPosts(int skip,int take)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;


            var result = await service.GetPostsAsync(skip,take, userId);
            return result;

        }
        [HttpGet("byPostId/{id:int}")]
        public async Task<ActionResult<PostDto>> GetPostByPostId([FromRoute] int id)
        {
            var result = await service.GetPostByPostIdAsync(id);
            if (result == null)
            {
                return NotFound("No result Found!!");
            }
            return Ok(result);  
        }

        [HttpGet("byOtherUserId/{id:int}")]
        public async Task<ActionResult<List<GetPostDto>>> GetPostByOtherUserId([FromRoute]int id)
        {
            var result = await service.GetPostByOtherUserIdAsync(id);
            if (result == null)
            {
                return NotFound(new { message = "Post not Found!!" });
            }
            return Ok(result);
        }
    }
}
