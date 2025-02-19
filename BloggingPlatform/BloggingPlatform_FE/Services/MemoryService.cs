using BloggingPlatform_FE.Models;
using BloggingPlatform_FE.Interfaces;

namespace BloggingPlatform_FE.Services;

public class MemoryService : IMemoryService
{
    private UserDto _currentUser;

    public UserDto GetCurrentUser()
    {
        return _currentUser;
    }
    public void SetCurrentUser(UserDto user)
    {
        _currentUser = user;
    }
}
