using System.ComponentModel.DataAnnotations;

namespace StudyHub.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; }= string.Empty;

        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public string Education {  get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty ;
        public string Country { get; set; } = string.Empty;

        [StringLength(15)]
        public string Phone { get; set; } = string.Empty;
        public string AboutInfo {  get; set; } = string.Empty;  

        
       public List<Post> Posts { get; set; }

        public List<Comment> Comments { get; set; }
        public List<CommentReply> CommentReplys { get; set; }
    }

  
}
