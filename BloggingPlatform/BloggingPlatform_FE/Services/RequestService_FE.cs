using BloggingPlatform_FE.Models;
using LusiUtilsLibrary.Backend.APIs_REST;
using LusiUtilsLibrary.Backend.Initialization;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.SwaggerGen;
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
    
    /// <summary>
    /// Execute request to add a user to the database
    /// </summary>
    /// <param name="user">user object to add to the db</param>
    /// <returns>class with returned object (if present) and status code</returns>
    /// <exception cref="Exception">catch exception if call fail and rethrow it</exception>
    public async Task<ApiResponse<UserDto>> AddUser(UserDto user)
    {
        try
        {
            ApiResponse<UserDto> data = await _service.ExecuteRequestAsync<UserDto>("AddUser", RequestType.POST, user);
            _logger.LogInformation("RequestService_FE - Add user call executed correctly");
            return data;
        }
        catch (Exception ex)
        {
            throw new Exception("RequestService_FE - Add user call not executed.", ex);
        }
    }

    /// <summary>
    /// Execute request to update a user to the database
    /// </summary>
    /// <param name="user">user object to add to the db</param>
    /// <returns>class with returned object (if present) and status code</returns>
    /// <exception cref="Exception">catch exception if call fails and rethrows it</exception>
    public async Task<ApiResponse<UserDto>> UpdateUser(UserDto user)
    {
        try
        {
            ApiResponse<UserDto> data = await _service.ExecuteRequestAsync<UserDto>("UpdateUser", RequestType.PUT, user);
            _logger.LogInformation("RequestService_FE - Update user call executed correctly");
            return data;
        }
        catch (Exception ex)
        {
            throw new Exception("RequestService_FE - Update user call not executed.", ex);
        }
    }

    /// <summary>
    /// Execute request to delete a user from the database
    /// </summary>
    /// <param name="userGuid">guid of the user that has to be deleted</param>
    /// <exception cref="Exception">catch exception if call fails and rethrows it</exception>
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

    /// <summary>
    /// Execute request to get a user from his guid
    /// </summary>
    /// <param name="userGuid">guid of the user that has to be returned</param>
    /// <returns>user object and status code of the request</returns>
    /// <exception cref="Exception">catch exception if call fails and rethrows it</exception>
    public async Task<ApiResponse<UserDto>> GetUserByGuid(Guid userGuid)
    {
        try
        {
            string[] args = [userGuid.ToString()];
            ApiResponse<UserDto> data = await _service.ExecuteRequestAsync<UserDto>("GetUserByGuid", RequestType.GET, null, args);
            _logger.LogInformation("RequestService_FE - Get user by guid call executed correctly");
            return data;
        }
        catch (Exception ex)
        {
            throw new Exception("RequestService_FE - Get user by guid call not executed.", ex);
        }
    }

    /// <summary>
    /// Execute request to get all users
    /// </summary>
    /// <returns>list of user object and status code of the request</returns>
    /// <exception cref="Exception">catch exception if call fails and rethrows it</exception>
    public async Task<ApiResponse<List<UserDto>>> GetAllUsers()
    {
        try
        {
            ApiResponse<List<UserDto>> data = await _service.ExecuteRequestAsync<List<UserDto>>("GetAllUsers", RequestType.GET, null);
            _logger.LogInformation("RequestService_FE - Get all users call executed correctly");
            return data;
        }
        catch (Exception ex)
        {
            throw new Exception("RequestService_FE - Get all users call not executed.", ex);
        }
    }

    #endregion

    #region BlogPost

    /// <summary>
    /// Executes request to add a blog post to the database
    /// </summary>
    /// <param name="blogPost">blog post object to add to the database</param>
    /// <returns>blogPost object (if returned) and status code of the request</returns>
    /// <exception cref="Exception">catch exception if call fails and rethrows it</exception>
    public async Task<ApiResponse<BlogPostDto>> AddBlogPost(BlogPostDto blogPost)
    {
        try
        {
            ApiResponse<BlogPostDto> data = await _service.ExecuteRequestAsync<BlogPostDto>("AddBlogPost", RequestType.POST, blogPost);
            _logger.LogInformation("RequestService_FE - Add blog post call executed correctly");
            return data;
        }
        catch (Exception ex)
        {
            throw new Exception("RequestService_FE - Add blog post call not executed.", ex);
        }
    }

    /// <summary>
    /// Execute request to update a blog post from the database
    /// </summary>
    /// <param name="blogPost">blog post object to update</param>
    /// <returns>blog post object updated and status code of the request</returns>
    /// <exception cref="Exception">catch exception if call fails and rethrows it</exception>
    public async Task<ApiResponse<BlogPostDto>> UpdateBlogPost(BlogPostDto blogPost)
    {
        try
        {
            ApiResponse<BlogPostDto> data = await _service.ExecuteRequestAsync<BlogPostDto>("UpdateBlogPost", RequestType.PUT, blogPost);
            _logger.LogInformation("RequestService_FE - Update blog post call executed correctly");
            return data;
        }
        catch (Exception ex)
        {
            throw new Exception("RequestService_FE - Update blog post call not executed.", ex);
        }
    }

    /// <summary>
    /// Execute request to delete a blog post from the database
    /// </summary>
    /// <param name="blogPostGuid">guid of the user to delete</param>
    /// <exception cref="Exception">catch exception if call fails and rethrows it</exception>
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

    /// <summary>
    /// Execute request to get a blog post from a guid
    /// </summary>
    /// <param name="blogPostGuid">guid of the blog post that has to be returned</param>
    /// <returns>blog post object and status code of the request</returns>
    /// <exception cref="Exception">catch exception if the call fails and rethrows it</exception>
    public async Task<ApiResponse<BlogPostDto>> GetBlogPostByGuid(Guid blogPostGuid)
    {
        try
        {
            string[] args = [blogPostGuid.ToString()];
            ApiResponse<BlogPostDto> data = await _service.ExecuteRequestAsync<BlogPostDto>("GetAllBlogPosts", RequestType.GET, null, args);
            _logger.LogInformation("RequestService_FE - Get blog post by guid call executed correctly");
            return data;
        }
        catch (Exception ex)
        {
            throw new Exception("RequestService_FE - Get blog post by guid call not executed.", ex);
        }
    }
     /// <summary>
     /// Execute request to get all blog posts
     /// </summary>
     /// <returns>a list of blog post objects and status code of the request</returns>
     /// <exception cref="Exception">catch exception if call fails and rethrow it</exception>
    public async Task<ApiResponse<List<BlogPostDto>>> GetAllBlogPosts()
    {
        try
        {
            ApiResponse<List<BlogPostDto>> data = await _service.ExecuteRequestAsync<List<BlogPostDto>>("GetAllBlogPosts", RequestType.GET, null);
            _logger.LogInformation("RequestService_FE - Get all blog posts call executed correctly");
            return data;
        }
        catch (Exception ex)
        {
            throw new Exception("RequestService_FE - Get all blog posts call not executed.", ex);
        }
    }
    #endregion

    /// <summary>
    /// Execute request to authenticate user
    /// </summary>
    /// <param name="user">user object to authenticate</param>
    /// <returns>full user object and status code of the request</returns>
    /// <exception cref="Exception">catch exception if call fails and rethrows it</exception>
    public async Task<ApiResponse<UserDto>> AuthenticateUser(UserDto user)
    {
        try
        {
            ApiResponse<UserDto> data = await _service.ExecuteRequestAsync<UserDto>("AuthenticateUser", RequestType.POST, user);
            _logger.LogInformation("RequestService_FE - Authenticate user call executed correctly");
            return data;
        }
        catch (Exception ex)
        {
            throw new Exception("RequestService_FE - Authenticate user call not executed.", ex);
        }
    }
}
