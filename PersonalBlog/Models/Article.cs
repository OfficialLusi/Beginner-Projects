using System.ComponentModel.DataAnnotations;

namespace PersonalBlog.Models
{
    public class Article
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime PublishedAt { get; set; } = DateTime.Now;
    }
}
