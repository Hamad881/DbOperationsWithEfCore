using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudyHub.Entities
{
    public class Chat
    {
        [Key]
        public int Chat_Id { get; set; }

        [ForeignKey("User")]
        public string User1_Id { get; set; }
        [ForeignKey("User")]
        public string User2_Id { get; set; }


        public User User {  get; set; }


    }
}
