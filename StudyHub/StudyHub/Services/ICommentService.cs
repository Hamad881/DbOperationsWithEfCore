using StudyHub.Entities;
using StudyHub.Model;

namespace StudyHub.Services
{
    public interface ICommentService
    {
        Task<Comment> AddCommentAsync(string Id, CommentDto request);

        Task<List<GetCommentDto>> GetCommentByPostAsync(int postId);

        Task<Comment> DeleteCommentAsync(int id, string userId);
    }
}