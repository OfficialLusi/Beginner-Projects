namespace BloggingPlatform_BE.Application.DTOs;

public class BlogPostDto
{
    public int PostId { get; set; } // primary key
    public int UserId { get; set; } // foreign key to connect to user
    public Guid PostGuid { get; set; }
    public required string PostTitle { get; set; }
    public required string PostContent { get; set; }
    public required string PostTags { get; set; }
    public DateTime PostCreatedOn { get; set; }
    public DateTime PostModifiedOn { get; set; } 
}
