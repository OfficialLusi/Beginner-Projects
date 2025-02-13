using BloggingPlatform_BE.Application.DTOs;

namespace BloggingPlatform_BE.Domain.Interfaces
{
    public interface IAuthenticationService
    {
        public List<byte[]> CreateHash(string password);

        public UserDto? AuthenticateUser(UserDto user);

        public bool IsPasswordChanged(UserDto user);
    }
}
