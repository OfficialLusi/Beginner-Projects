using BloggingPlatform_FE.Models;
using LusiUtilsLibrary.Backend.APIs_REST;
using LusiUtilsLibrary.Backend.Initialization;
using Microsoft.Extensions.Logging;
using System.Net;

namespace BloggingPlatform_FE.Services;

public class RequestService_FE
{
    private readonly ILogger<RequestService_FE> _logger;
    private readonly REST_RequestService _service;

    public RequestService_FE(ILogger<RequestService_FE> logger, REST_RequestService service)
    {
        #region Initialize
        InitializeChecks.InitialCheck(logger, "Logger cannot be null");
        InitializeChecks.InitialCheck(service, "REST_RequestService cannot be null");
        #endregion

        _logger = logger;
        _service = service;
    }

    #region User

    public async void AddUser(UserDto user)
    {
        try
        {
            await _service.ExecuteRequestAsync<UserDto>("AddUser", RequestType.POST, user);
            _logger.LogInformation("RequestService_FE - Add user call executed correctly");
        }
        catch (Exception ex)
        {
            throw new Exception("RequestService_FE - Add user call not executed.", ex);
        }
    }

    public async void UpdateUser(UserDto user)
    {
        try
        {
            await _service.ExecuteRequestAsync<UserDto>("UpdateUser", RequestType.PUT, user);
            _logger.LogInformation("RequestService_FE - Update user call executed correctly");
        }
        catch (Exception ex)
        {
            throw new Exception("RequestService_FE - Update user call not executed.", ex);
        }
    }

    public async void DeleteUser(Guid userGuid)
    {
        try
        {
            string[] args = [userGuid.ToString()];
            await _service.ExecuteRequestAsync<UserDto>("DeleteUser", RequestType.DELETE, null, args);
            _logger.LogInformation("RequestService_FE - Delete user call executed correctly");
        }
        catch (Exception ex)
        {
            throw new Exception("RequestService_FE - Delete user call not executed.", ex);
        }
    }

    public async Task<UserDto> GetUserByGuid(Guid userGuid)
    {
        try
        {
            string[] args = [userGuid.ToString()];
            UserDto user = await _service.ExecuteRequestAsync<UserDto>("GetUserByGuid", RequestType.GET, null, args);
            _logger.LogInformation("RequestService_FE - Get user by guid call executed correctly");
            return user;
        }
        catch (Exception ex)
        {
            throw new Exception("RequestService_FE - Get user by guid call not executed.", ex);
        }
    }

    public async Task<List<UserDto>> GetAllUsers()
    {
        try
        {
            List<UserDto> users = await _service.ExecuteRequestAsync<List<UserDto>>("GetAllUsers", RequestType.GET, null);
            _logger.LogInformation("RequestService_FE - Get all users call executed correctly");
            return users;
        }
        catch (Exception ex)
        {
            throw new Exception("RequestService_FE - Get all users call not executed.", ex);
        }
    }

    #endregion

    #region BlogPost

    public async void AddBlogPost(BlogPostDto blogPost)
    {
        try
        {
            await _service.ExecuteRequestAsync<BlogPostDto>("AddBlogPost", RequestType.POST, blogPost);
            _logger.LogInformation("RequestService_FE - Add blog post call executed correctly");
        }
        catch (Exception ex)
        {
            throw new Exception("RequestService_FE - Add blog post call not executed.", ex);
        }
    }
    public async void UpdateBlogPost(BlogPostDto blogPost)
    {
        try
        {
            await _service.ExecuteRequestAsync<BlogPostDto>("UpdateBlogPost", RequestType.PUT, blogPost);
            _logger.LogInformation("RequestService_FE - Update blog post call executed correctly");
        }
        catch (Exception ex)
        {
            throw new Exception("RequestService_FE - Update blog post call not executed.", ex);
        }
    }

    public async void DeleteBlogPost(Guid blogPostGuid)
    {
        try
        {
            string[] args = [blogPostGuid.ToString()];
            await _service.ExecuteRequestAsync<BlogPostDto>("DeletePostDto", RequestType.DELETE, null, args);
            _logger.LogInformation("RequestService_FE - Delete blog post call executed correctly");
        }
        catch (Exception ex)
        {
            throw new Exception("RequestService_FE - Delete blog post call not executed.", ex);
        }
    }

    public async Task<ApiResponse<BlogPostDto>> GetBlogPostByGuid(Guid blogPostGuid)
    {
        try
        {
            string[] args = [blogPostGuid.ToString()];
            ApiResponse<BlogPostDto> response = await _service.ExecuteRequestAsync<BlogPostDto>("GetAllBlogPosts", RequestType.GET, null, args);
            _logger.LogInformation("RequestService_FE - Get blog post by guid call executed correctly");
            return response;
        }
        catch (Exception ex)
        {
            throw new Exception("RequestService_FE - Get blog post by guid call not executed.", ex);
        }
    }

    public async Task<ApiResponse<List<BlogPostDto>>> GetAllBlogPosts()
    {
        try
        {
            ApiResponse<List<BlogPostDto>> response = await _service.ExecuteRequestAsync<List<BlogPostDto>>("GetAllBlogPosts", RequestType.GET, null);
            _logger.LogInformation("RequestService_FE - Get all blog posts call executed correctly");
            return response;
        }
        catch (Exception ex)
        {
            throw new Exception("RequestService_FE - Get all blog posts call not executed.", ex);
        }
    }
    #endregion

    public async Task<UserDto> AuthenticateUser(UserDto user)
    {
        try
        {
            await _service.ExecuteRequestAsync<UserDto>("AuthenticateUser", RequestType.POST, user).;
            _logger.LogInformation("RequestService_FE - Authenticate user call executed correctly");
            return returnedUser;
        }
        catch (Exception ex)
        {
            throw new Exception("RequestService_FE - Authenticate user call not executed.", ex);
        }
    }
}
