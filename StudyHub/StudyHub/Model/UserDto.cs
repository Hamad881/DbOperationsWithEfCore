namespace StudyHub.Model
{
    public class UserDto
    {
        public  string? Name { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
        public  string? Email { get; set; }
       
    }
}
