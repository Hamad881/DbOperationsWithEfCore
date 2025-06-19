using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StudyHub.Entities
{
    public class Answer
    {
        [Key]
            public int Answer_Id { get; set; }

            [ForeignKey("User")]
            public int User_Id { get; set; }

            [ForeignKey("Question")]
            public int Question_Id { get; set; }

            [ForeignKey("Options")]
            public int AnsOption_Id { get; set; }


            public User User { get; set; }
            public Questions Question { get; set; }
            public Options Options { get; set; }
        
    }
}
