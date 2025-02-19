using BloggingPlatform_FE.Models;

namespace BloggingPlatform_FE.Interfaces;

public interface IMemoryService
{
    public UserDto GetCurrentUser();
    public void SetCurrentUser(UserDto user);
}
