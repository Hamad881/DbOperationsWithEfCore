namespace StudyHub.Model
{
    public class CommentDto
    {


        public string Comment_text { get; set; }

        public int Post_Id { get; set; }
    }

    public class GetCommentDto
    {
        public int Comment_Id { get; set; }
        public string Comment_text { get; set; }

        public int User_Id { get; set; }

        public string UserName { get; set; }
        public DateTime Created_At { get; set; }
    }
}
