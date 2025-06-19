using StudyHub.Entities;
using StudyHub.Model;

namespace StudyHub.Services
{
    public interface IAuthService
    {
        Task<List<User>> GetUserAsync();
        Task<string?> LoginAsync(UserDto request);
        Task<User?> RegisterAsync(UserDto request);

        Task<UserDetailsDto> GetUserDetails(string userId);
        Task<User> UpdateUserDetails(string userId, UserDetailsDto updateDetails);
        Task<UserIdDto> GetUserId(string userId);
    }
}