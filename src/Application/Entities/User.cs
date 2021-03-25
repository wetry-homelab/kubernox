using System.ComponentModel.DataAnnotations;

namespace Application.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Email { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public bool Enable { get; set; }
    }
}
