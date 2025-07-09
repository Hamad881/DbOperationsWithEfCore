using System.ComponentModel.DataAnnotations;

namespace StudyHub.Model
{
    public class UserDetailsDto
    {
        public string Name { get; set; }
        public string Username { get; set; }

        public string Email { get; set; } 

        
        public string Education { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;

        [StringLength(15)]
        public string Phone { get; set; }

        public string AboutInfo { get; set; } = string.Empty;

    }
    public class UserIdDto
    {
        public int User_Id { get; set; }
        public string Username { get; set; }
    }
}
