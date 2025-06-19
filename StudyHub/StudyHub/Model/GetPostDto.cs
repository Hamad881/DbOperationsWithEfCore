namespace StudyHub.Model
{
    public class GetPostDto
    {
        public int Post_Id { get; set; }
        public string Data { get; set; }


        public int Cat_Id { get; set; }

        public DateTime DateCreated { get; set; }

        public string Cat_Name { get; set; }
        public string Name { get; set; }

    }
}
