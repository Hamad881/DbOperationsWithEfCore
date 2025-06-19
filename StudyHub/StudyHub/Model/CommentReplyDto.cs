namespace StudyHub.Model
{
    public class CommentReplyDto
    {
        public string ReplyText { get; set; }

        public int CommentId { get; set; }
        public bool IsReply { get; set; }

    }

    public class GetCommentReplyDto
    {
        public string ReplyText { get; set; }

        public string UserName { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
