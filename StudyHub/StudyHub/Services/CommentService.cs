using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore;
using StudyHub.Data;
using StudyHub.Entities;
using StudyHub.Model;

namespace StudyHub.Services
{
    public class CommentService : ICommentService
    {
        private readonly MyDbContext context;

        public CommentService(MyDbContext context)
        {
            this.context = context;
        }

        public async Task<Comment> AddCommentAsync(string Id, CommentDto request)
        {
            int parseId = int.Parse(Id);
            Comment commentData = new Comment();
            commentData.User_Id = parseId;
            commentData.Comment_Text = request.Comment_text;
            commentData.Created_At = DateTime.Now;
            commentData.Post_Id = request.Post_Id;
            context.Comment.Add(commentData);
            await context.SaveChangesAsync();
            return commentData;
        }

        public async Task<List<GetCommentDto>> GetCommentByPostAsync(int postId)
        {
            var commentData = await context.Comment.OrderByDescending(c=>c.Comment_Id).Where(c => c.Post_Id == postId).Select(c => new GetCommentDto()
            {
                Comment_Id=c.Comment_Id,
                Comment_text = c.Comment_Text,
                Created_At = c.Created_At,
                User_Id=c.User_Id,
                Username=c.User.Username,
                Name = c.User.Name
            }).ToListAsync();
            return commentData;
        }
        public async Task<Comment> DeleteCommentAsync(int id, string userId)
        {
            int parsedId = int.Parse(userId);
            var comment = await context.Comment.Include(c => c.Reply).FirstOrDefaultAsync(c => c.Comment_Id == id);
            if (comment.User_Id == parsedId)
            {
                context.CommentReply.RemoveRange(comment.Reply);
                context.Comment.Remove(comment);
                await context.SaveChangesAsync();
                return null;
            }
            return null;
        }
    }
}
