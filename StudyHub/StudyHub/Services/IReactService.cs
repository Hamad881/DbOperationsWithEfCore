using StudyHub.Entities;
using StudyHub.Model;

namespace StudyHub.Services
{
    public interface IReactService
    {
        Task<React> AddReactAsync(int postId, string userId, ReactDto request);
        Task<ReactCountDto> GetReactByPostIdAsync(int postId,string userId);
        Task<React?> RemoveReactAsync(int reactId, string userId);
    }
}