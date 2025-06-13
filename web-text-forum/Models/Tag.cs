namespace web_text_forum.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string TagDescription { get; set; } = string.Empty;

        // Optional: navigation property for related posts
        public ICollection<Post> Posts { get; set; } = new List<Post>();
    }
}
