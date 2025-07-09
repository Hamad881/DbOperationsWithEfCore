using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudyHub.Entities
{
    public class CommentReply
    {
        [Key]
        public int CommentRply_Id { get; set; }

        [ForeignKey("Comment")]
        public int Comment_Id {  get; set; }
        public string Comment_Reply {  get; set; }
        [ForeignKey("User")]
        public int User_Id { get; set; }   
        public DateTime Created_At { get; set; }

        //[ForeignKey("Reply")]
        //public int? ReplyIdOfReply {  get; set; }

        //public bool IsReply { get; set; } = false;

        public Comment Comment { get; set; }
        public User User { get; set; }

       // public CommentReply? Reply { get; set; }
    }
}
