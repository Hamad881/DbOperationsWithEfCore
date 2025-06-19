using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudyHub.Entities
{
    public class Options
    {
        [Key]
        public int Option_Id { get; set; }

        public string Option_Text { get; set; }

        [ForeignKey("Question")]
        public int Question_Id { get; set; }

        public bool IsCorrect { get; set; }
        public Questions Questions { get; set; }

        public List<Answer> UserAnswers { get; set; }
    }
}
