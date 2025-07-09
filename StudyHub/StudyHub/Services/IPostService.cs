using StudyHub.Entities;
using StudyHub.Model;

namespace StudyHub.Services
{
    public interface IPostService
    {
        Task<Post> AddPostTData(string id, PostDto request);

        Task<Post> UpdatePost(string userId, int id, PostDto updatedPost);
        Task<Post> DeletePostAsync(string userId, int id);

        Task<List<GetPostDto>> GetPostByIdAsync(string userId);

        Task<List<GetPostDto>> GetPostsAsync(int skip, int take,string userId);
        Task<PostDto> GetPostByPostIdAsync(int postId);
        Task<List<GetPostDto>> GetPostByOtherUserIdAsync(int userId);
    }
}