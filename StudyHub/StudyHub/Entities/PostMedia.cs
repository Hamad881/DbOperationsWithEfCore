using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudyHub.Entities
{
    public class PostMedia
    {
        [Key]
        public int MediaId { get; set; } 
        public string Media { get; set; }

        [ForeignKey("Post")]
        public string PostId { get; set; }

        public Post Post { get; set; }
    }
}
