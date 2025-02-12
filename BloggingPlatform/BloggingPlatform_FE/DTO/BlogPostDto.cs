namespace BloggingPlatform_BE.Application.DTOs;

public class BlogPostDto
{
    public int PostId { get; set; }
    public int UserId { get; set; }
    public Guid PostGuid { get; set; }
    public string PostTitle { get; set; }
    public string PostContent { get; set; }
    public string PostTags { get; set; }
    public DateTime PostCreatedOn { get; set; }
    public DateTime PostModifiedOn { get; set; } 
}
