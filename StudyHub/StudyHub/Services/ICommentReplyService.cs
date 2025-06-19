using StudyHub.Entities;
using StudyHub.Model;

namespace StudyHub.Services
{
    public interface ICommentReplyService
    {
        Task<CommentReply> AddCommentReplyAsync(string Id, int replyId, CommentReplyDto request);
        Task<List<GetCommentReplyDto>> GetCommentReplyByCommentAsync(int commentId);

        Task<CommentReply> DeleteCommentReplyAsync(int id, string userId);
    }
}