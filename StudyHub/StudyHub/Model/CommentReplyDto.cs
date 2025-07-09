namespace StudyHub.Model
{
    public class CommentReplyDto
    {
        public string ReplyText { get; set; }

        //public int CommentId { get; set; }
       // public bool IsReply { get; set; }

    }

    public class GetCommentReplyDto
    {
        public int Reply_Id { get; set; }
        public string ReplyText { get; set; }

        public string Name { get; set; }
        public string Username { get; set; }
        public DateTime CreatedAt { get; set; }
        public int User_Id { get; set; }
    }
}
