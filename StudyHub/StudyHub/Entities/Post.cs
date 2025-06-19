using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudyHub.Entities
{
    public class Post
    {
        [Key]
        public int PostId { get; set; }

        public DateTime DateCreated { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        [ForeignKey("Categories")]
        public int Cat_Id { get; set; }

        public string Data { get; set; }

       public User User { get; set; }
       
        public Categories Categories { get; set; }
    }
}
 