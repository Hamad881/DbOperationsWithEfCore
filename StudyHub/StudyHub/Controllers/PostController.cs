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

        [HttpPut("{id}")]
        public async Task<ActionResult<Post>> UpdatePostData(int id, PostDto request)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var updated = await service.UpdatePost(userId,id,request);
            if (updated == null)
            {
                return BadRequest("Something went wrong!");
            }
            return Ok(updated);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Post>> DeletePost(int id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var delPost= await service.DeletePostAsync(userId,id);
            if (delPost == null)
            {
                return BadRequest("Something went wrong");
            }
            return Ok("Post Deleted");
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


            var result= await service.GetPostsAsync(skip,take);
            return result;

        }
    }
}
