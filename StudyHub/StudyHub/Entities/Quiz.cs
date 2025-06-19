using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudyHub.Entities
{
    public class Quiz
    {
        [Key]
        public int Quiz_Id { get; set; }
        public string Quiz_Name { get; set; }

        [ForeignKey("User")]
        public int User_Id { get; set; }


        public User User { get; set; }  
    }
}
