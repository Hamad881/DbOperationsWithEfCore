using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudyHub.Entities
{
    public class React
    {
        [Key]
        public int React_Id { get; set; }

        [ForeignKey("User")]

        public int User_Id { get; set; }

        [ForeignKey("Post")]
        public int Post_Id { get; set; }    

        public ReactType? ReactType { get; set; }

        public User? User { get; set; }
        public Post? Post { get; set; }
    }

    public enum ReactType
    {
        Like,
        Dislike
    }
}
