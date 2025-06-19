using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudyHub.Entities
{
    public class Comment
    {
        [Key]
        public int Comment_Id { get; set; }

        [ForeignKey("User")]
        public int User_Id { get; set; }

        [ForeignKey("Post")]
        public int Post_Id { get; set; }
        public string Comment_Text { get; set; }

        public DateTime Created_At { get; set; }


        public Post Post { get; set; }

        public List<CommentReply> Reply { get; set; }
        public User User { get; set; }
    }
}
