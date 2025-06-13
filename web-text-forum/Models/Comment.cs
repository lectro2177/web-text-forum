using System.ComponentModel.DataAnnotations.Schema;

namespace web_text_forum.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        // Navigation properties for EF Core
        [ForeignKey("PostId")]
        public Post? Post { get; set; }

        [ForeignKey("UserId")]
        public User? User { get; set; }
    }
}
