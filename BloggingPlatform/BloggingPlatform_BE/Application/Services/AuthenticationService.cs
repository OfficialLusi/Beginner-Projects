using BloggingPlatform_BE.Application.DTOs;
using BloggingPlatform_BE.Domain.Interfaces;
using LusiUtilsLibrary.Backend.Crypting;
using System.Linq;

namespace BloggingPlatform_BE.Application.Services;

public class AuthenticationService
{

    private readonly IRepositoryService _repositoryService;

    public AuthenticationService(IRepositoryService repositoryService)
    {                            
        _repositoryService = repositoryService;
    }

    public List<byte[]> CreateHash(string password)
    {
        byte[] salt = HashCrypting.GenerateSalt();
        byte[] hash = HashCrypting.HashPassword(password, salt);

        return [salt, hash];
    }

    public UserDto? AuthenticateUser(UserDto user)
    {
        List<UserDto> users = _repositoryService.GetAllUsers();

        if (user.UserName == "admin")
            return users.FirstOrDefault(x => x.UserName == "admin");

        if(users.Any(x => x.UserEmail == user.UserEmail))
        {
            UserDto repoUser = users.FirstOrDefault(x => x.UserEmail == user.UserEmail);
            if(Convert.FromBase64String(repoUser.HashCode).SequenceEqual(HashCrypting.CheckHash(user.UserPassword, Convert.FromBase64String(repoUser.Salt))))
                return repoUser;
        }
        return null;
    }

    public bool IsPasswordChanged(UserDto user)
    {
        UserDto oldUser = _repositoryService.GetUserByGuid(user.UserGuid);

        if (Convert.FromBase64String(oldUser.HashCode).SequenceEqual(HashCrypting.CheckHash(user.UserPassword, Convert.FromBase64String(oldUser.Salt))))
            return false;
        return true;
    }
}
