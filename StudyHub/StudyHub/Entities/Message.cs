using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudyHub.Entities
{
    public class Message
    {
        [Key]
        public int Message_Id { get; set; }

        [ForeignKey("Chat")]
        public int Chat_Id { get; set; }
        [ForeignKey("User")]
        public int Sender_Id { get; set; }
        public string Message_Content { get; set; }

        public DateTime SendAt { get; set; }

        public Chat Chat { get; set; }

        public User User { get; set; }
    }
}
