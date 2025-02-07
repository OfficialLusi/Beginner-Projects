namespace BloggingPlatform_BE.Application.DTOs;

public class UserDto
{
    public required int UserId { get; set; } // primary key
    public required Guid UserGuid { get; set; }
    public required string UserName { get; set; }
    public required string UserSurname { get; set; }
    public required string UserEmail { get; set; }
    public required string UserPassword { get; set; }
    public required DateTime UserCreatedOn { get; set; }
}
