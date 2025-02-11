namespace BloggingPlatform_BE.Application.DTOs;

public class UserDto
{
    public int UserId { get; set; } // primary key
    public Guid UserGuid { get; set; }
    public string UserName { get; set; }
    public string UserSurname { get; set; }
    public string UserEmail { get; set; }
    public string UserPassword { get; set; }
    public DateTime UserCreatedOn { get; set; } // todo verify if from frontend this is a string, if yes, check repository method
}
