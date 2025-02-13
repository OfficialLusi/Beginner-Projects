namespace BloggingPlatform_BE.Application.DTOs;

public class UserDto
{
    public int UserId { get; set; } // primary key
    public Guid UserGuid { get; set; }
    public string UserName { get; set; }
    public string UserSurname { get; set; }
    public string UserEmail { get; set; }
    public string? UserPassword { get; set; }
    public DateTime UserCreatedOn { get; set; } 
    public string Salt { get; set; }    
    public string HashCode { get; set; }
}
