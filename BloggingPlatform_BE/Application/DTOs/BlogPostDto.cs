namespace BloggingPlatform_BE.Application.DTOs;

public class BlogPostDto
{
    public int PostId { get; set; } // primary key
    public int UserId { get; set; } // foreign key to connect to user
    public Guid PostGuid { get; set; }
    public string PostTitle { get; set; }
    public string PostContent { get; set; }
    public DateTime PostCreatedOn { get; set; }
    public DateTime PostModifiedOn { get; set; } 
}
