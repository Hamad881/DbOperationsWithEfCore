using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudyHub.Entities;
using StudyHub.Model;
using StudyHub.Services;

namespace StudyHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService service;

        public AuthController(IAuthService service)
        {
            this.service = service;
        }

        [HttpPost("register")]
        public async Task<ActionResult<User?>> Register(UserDto request)
        {
            var user = await service.RegisterAsync(request);
            if (user == null)
            {
                return BadRequest("Username Already Exists!");
            }

            return Ok(user);

        }
        [HttpPost("login")]
        public async Task<ActionResult<string?>> Login(UserDto request)
        {
            var token = await service.LoginAsync(request);
            if (token is null)
            {
                return BadRequest("Username/Password is wrong");
            }
            return Ok(new { token = token });
        }
        [HttpGet("auth-endpoint")]
        [Authorize]
        public ActionResult AuthCheck()
        {
            return Ok();
        }

        [HttpGet]
        public async Task<List<User>> GetallUsers()
        {
            var result = await service.GetUserAsync();
            return result;
        }

        [Authorize]
        [HttpGet("userinfo")]
        public async Task<IActionResult> GetCurrentUserDetails()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var result = await service.GetUserDetails(userId);

            return Ok(result);
        }
        [Authorize]
        [HttpPut("updateInfo")]
        public async Task<IActionResult> UpdateUserDetails(UserDetailsDto userinfo)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var result = await service.UpdateUserDetails(userId, userinfo);
            return Ok(new { message = "User Updated!!" });
        }

        [HttpGet("userId")]
        public async Task<UserIdDto> GetUserId()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var username = User.FindFirst(ClaimTypes.Name)?.Value;
            var result = await service.GetUserId(userId,username);
            return result;
        }
        [HttpGet("userDetailsById/{id:int}")]
        public async Task<ActionResult<UserDetailsDto>> GetUserDetailsById([FromRoute]int id)
        {
            var result = await service.GetUserDetailsByIdAsync(id);
            if (result == null)
            {
                return NotFound(new {message= "User Not Found!!"});
            }
            return Ok(result);
        }

    }
}
