using System.ComponentModel.DataAnnotations.Schema;

namespace web_text_forum.Models
{
    public class Like
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation properties for EF Core
        [ForeignKey("PostId")]
        public Post? Post { get; set; }

        [ForeignKey("UserId")]
        public User? User { get; set; }
    }
}
