using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using StudyHub.Data;
using StudyHub.Entities;
using StudyHub.Model;

namespace StudyHub.Services
{
    public class PostService : IPostService

    {
        private readonly MyDbContext context;

        public PostService(MyDbContext context)
        {
            this.context = context;
        }

        public async Task<Post> AddPostTData(string id, PostDto request)
        {
            int parseId = int.Parse(id);

            Post post = new Post();
            post.Cat_Id = request.Cat_Id;
            post.DateCreated = DateTime.Now;
            post.UserId = parseId;
            post.Data = request.Data;
            context.Post.Add(post);
            await context.SaveChangesAsync();
            return post;
        }
        public async Task<Post> UpdatePost(string userId, int id, PostDto updatedPost)
        {
            var post = await context.Post.FindAsync(id);
            if (post == null)
            {
                return null;
            }
            int parsedId = int.Parse(userId);
            if (parsedId != post.UserId)
            {
                return null;
            }

            post.Data = updatedPost.Data;
            await context.SaveChangesAsync();
            return post;


        }
        public async Task<Post> DeletePostAsync(string userId, int id)
        {
            var post = await context.Post.FindAsync(id);

            int parsedId = int.Parse(userId);
            if (parsedId == post.UserId)
            {
                context.Post.Remove(post);
                await context.SaveChangesAsync();
                return post;
            }
            return null;
        }
        public async Task<List<GetPostDto>> GetPostByIdAsync(string userId)
        {

            int parsedId = int.Parse(userId);
            var userPosts = await context.Post.Where(p => p.UserId == parsedId).Select(p => new GetPostDto()
            {
                Data = p.Data,
                Cat_Id = p.Cat_Id,
                DateCreated = p.DateCreated,
                Name = p.User.Name,
                Cat_Name = p.Categories.Cat_Name
            }).ToListAsync();
            return userPosts;

        }
        public async Task<List<GetPostDto>> GetPostsAsync(int skip, int take )
        {
            
          
            var posts = await context.Post
                .OrderByDescending(p => p.PostId)
                .Skip(skip)
                .Take(take)
                .Select(p => new GetPostDto()
                {
                   Post_Id = p.PostId,
                    Data = p.Data,
                    Cat_Id = p.Cat_Id,
                    DateCreated = p.DateCreated,
                    Name = p.User.Name,
                    Cat_Name = p.Categories.Cat_Name
                }).ToListAsync();
            

            return posts;
        }
    }


}
