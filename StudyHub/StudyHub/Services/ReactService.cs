using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudyHub.Data;
using StudyHub.Entities;
using StudyHub.Model;

namespace StudyHub.Services
{
    public class ReactService : IReactService
    {
        private readonly MyDbContext context;

        public ReactService(MyDbContext context)
        {
            this.context = context;
        }

        public async Task<React> AddReactAsync(int postId, string userId, ReactDto request)
        {
            var parsedId = int.Parse(userId);

            var existingReact = await context.React.FirstOrDefaultAsync(r=>r.Post_Id == postId && r.User_Id == parsedId && (r.ReactType==ReactType.Like || r.ReactType==ReactType.Dislike));
            if (existingReact!=null) {
                return null;
            }
            React react = new React()
            {
                User_Id = parsedId,
                ReactType = request.React.Value,
                Post_Id = postId,
            };
            context.React.Add(react);
            await context.SaveChangesAsync();
            return react;
        }
        public async Task<ReactCountDto> GetReactByPostIdAsync(int postId,string userId)
        {

            var parseId = int.Parse(userId);

            var likes =  context.React.Where(r=>r.Post_Id==postId && r.ReactType==ReactType.Like).Count();

            var dislikes = context.React.Where(r => r.Post_Id == postId && r.ReactType==ReactType.Dislike).Count();
            var reactId = context.React.Where(r => r.Post_Id == postId && r.User_Id == parseId).Select(p => p.React_Id).FirstOrDefault();
            ReactCountDto count = new ReactCountDto()
            {
                React_Id= reactId,
                PostLikes = likes,
                PostDislikes = dislikes
            };
            return count;
        }

        public async Task<React?> RemoveReactAsync(int reactId, string userId)
        {
            var parseId= int.Parse(userId);
            var result =await context.React.FirstOrDefaultAsync(r => r.React_Id == reactId && r.User_Id==parseId);
             context.React.Remove(result);
            await context.SaveChangesAsync();
            return null;
        }
    }
}

   
