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
            post.Cat_Id = updatedPost.Cat_Id;
            post.Data = updatedPost.Data;
            await context.SaveChangesAsync();
            return post;


        }
        public async Task<Post> DeletePostAsync(string userId, int id)
        {
            int parsedId = int.Parse(userId);
            var post = await context.Post.Include(p => p.Comments).FirstOrDefaultAsync(p => p.PostId == id);
            if (parsedId == post.UserId)
            {
                context.Comment.RemoveRange(post.Comments);
                context.Post.Remove(post);
                await context.SaveChangesAsync();
                return post;
            }
            return null;
        }
        public async Task<List<GetPostDto>> GetPostByIdAsync(string userId)
        {

            int parsedId = int.Parse(userId);
            var userPosts = await context.Post.OrderByDescending(p=>p.PostId).Where(p => p.UserId == parsedId).Select(p => new GetPostDto()
            {
                Post_Id = p.PostId,
                User_Id= p.UserId,
                Data = p.Data,
                Cat_Id = p.Cat_Id,
                DateCreated = p.DateCreated,
                Name = p.User.Name,
                Cat_Name = p.Categories.Cat_Name
            }).ToListAsync();
            return userPosts;

        }
        public async Task<List<GetPostDto>> GetPostsAsync(int skip, int take, string userId)
        {

            var parseId= int.Parse(userId);
            var posts = await context.Post
                .OrderByDescending(p => p.PostId)
                .Skip(skip)
                .Take(take)
                .Include(p=>p.Reacts)
                .Select(p => new GetPostDto()
                {
                    Post_Id = p.PostId,
                    Data = p.Data,
                    Cat_Id = p.Cat_Id,
                    DateCreated = p.DateCreated,
                    Name = p.User.Name,
                    Username=p.User.Username,
                    Cat_Name = p.Categories.Cat_Name,
                    User_Id=p.UserId,
                    Likes=p.Reacts.Where(r=>r.Post_Id==p.PostId && r.ReactType== ReactType.Like).Count(),
                    Dislikes= p.Reacts.Where(r => r.Post_Id == p.PostId && r.ReactType == ReactType.Dislike).Count(),
                    React_Id = p.Reacts.Where(r => r.Post_Id == p.PostId && r.User_Id == parseId).Select(p=>p.React_Id).FirstOrDefault()
                }
                ).ToListAsync();


            return posts;
        }
        public async Task<PostDto> GetPostByPostIdAsync(int postId)
        {
            var post =  await context.Post.FindAsync(postId);
            if (post == null )
            {
                return null;
            }
            PostDto singlePost = new PostDto()
            {
                Data = post.Data,
                Cat_Id=post.Cat_Id
            };
            return singlePost;
        }
        public async Task<List<GetPostDto>> GetPostByOtherUserIdAsync(int userId)
        {
            var userPosts = await context.Post.OrderByDescending(p => p.PostId).Where(p => p.UserId == userId).Select(p => new GetPostDto()
            {
                Post_Id = p.PostId,
                User_Id = p.UserId,
                Data = p.Data,
                Cat_Id = p.Cat_Id,
                DateCreated = p.DateCreated,
                Name = p.User.Name,
                Cat_Name = p.Categories.Cat_Name,
                Username = p.User.Username,
                
            }).ToListAsync();
            if (userPosts == null)
            {
                return null;
            }
            return userPosts;

        }

    }


}
