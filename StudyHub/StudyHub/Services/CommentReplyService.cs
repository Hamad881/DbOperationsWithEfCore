using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using StudyHub.Data;
using StudyHub.Entities;
using StudyHub.Model;
using Microsoft.EntityFrameworkCore;

namespace StudyHub.Services
{
    public class CommentReplyService : ICommentReplyService
    {
        private readonly MyDbContext context;

        public CommentReplyService(MyDbContext context)
        {
            this.context = context;
        }
        public async Task<CommentReply> AddCommentReplyAsync(string Id,int replyId, CommentReplyDto request)
        {
            int parseId = int.Parse(Id);
            CommentReply commentReplyData = new CommentReply();
            commentReplyData.User_Id = parseId;
            commentReplyData.Comment_Id = request.CommentId;
            commentReplyData.Comment_Reply = request.ReplyText;
            commentReplyData.Created_At= DateTime.Now;
            commentReplyData.IsReply= request.IsReply;
            if(commentReplyData.IsReply==true)
            {
                commentReplyData.ReplyIdOfReply= replyId;
            }

           
            context.CommentReply.Add(commentReplyData);
            await context.SaveChangesAsync();
            return commentReplyData;
        }

        public async Task<List<GetCommentReplyDto>> GetCommentReplyByCommentAsync(int commentId)
        {
            var commentReplyData = await context.CommentReply.Where(cr => cr.Comment_Id == commentId).Select(cr => new GetCommentReplyDto()
            {
                ReplyText = cr.Comment_Reply,
                CreatedAt = cr.Created_At,
                UserName = cr.User.Name

            }).ToListAsync();
            return commentReplyData;
        }
        public async Task<CommentReply> DeleteCommentReplyAsync(int id, string userId)
        {
            int parsedId = int.Parse(userId);
            var commentReply = await context.CommentReply.FindAsync(id);
            if (commentReply.User_Id == parsedId)
            {

                await DeleteSubReply(commentReply);
                
                context.CommentReply.Remove(commentReply);
               
                    await context.SaveChangesAsync();
                return null;
            }
            return null;
        }
        private async Task DeleteSubReply(CommentReply commentReply)
        {
            var subReply = await context.CommentReply.Where(cr=> cr.ReplyIdOfReply == commentReply.CommentRply_Id).ToListAsync();
            foreach (var reply in subReply) 
            { 
                await DeleteSubReply(reply);
                context.CommentReply.Remove(reply);
            }

        }
    }
}
