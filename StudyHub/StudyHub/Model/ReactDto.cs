using StudyHub.Entities;

namespace StudyHub.Model
{
    public class ReactDto
    {
        public ReactType? React { get; set; }
    }

    public class ReactCountDto

    {
         public int React_Id { get; set; }
        public int PostLikes { get; set; }

        public int PostDislikes { get; set; }
    }
}
