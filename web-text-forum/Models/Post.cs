using System.ComponentModel.DataAnnotations.Schema;

namespace web_text_forum.Models
{
    public class Post
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public int? TagId { get; set; }

        // Navigation properties for EF Core
        [ForeignKey("UserId")]
        public User? User { get; set; }

        [ForeignKey("TagId")]
        public Tag? Tag { get; set; }
    }
}
