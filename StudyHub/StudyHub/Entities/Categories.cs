using System.ComponentModel.DataAnnotations;

namespace StudyHub.Entities
{
    public class Categories
    {
        [Key]
        public int Cat_Id { get; set; }

        public string Cat_Name { get; set; }
    }
}
