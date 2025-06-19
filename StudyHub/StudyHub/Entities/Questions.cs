using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudyHub.Entities
{
    public class Questions
    {
        [Key]
        public int Question_Id { get; set; }

        [ForeignKey("Quiz")]
        public int Quiz_Id { get; set; }
        public string Question {  get; set; }
        public List<Options> Options { get; set; }



        public Quiz Quiz { get; set; }
        public List<Answer> UserAnswers { get; set; }

    }
}
