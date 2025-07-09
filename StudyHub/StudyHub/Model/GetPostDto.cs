namespace StudyHub.Model
{
    public class GetPostDto
    {
        public int Post_Id { get; set; }
        public string Data { get; set; }

        public int User_Id { get; set; }
        public int Cat_Id { get; set; }

        public string Username { get; set; }

        public DateTime DateCreated { get; set; }

        public string Cat_Name { get; set; }
        public string Name { get; set; }

        public int Likes { get; set; }
        public int Dislikes     { get; set; }
        public int React_Id     { get; set; }

    } 
    
}
