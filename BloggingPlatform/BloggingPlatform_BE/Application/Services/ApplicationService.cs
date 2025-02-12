using BloggingPlatform_BE.Application.DTOs;
using BloggingPlatform_BE.Domain.Interfaces;

namespace BloggingPlatform_BE.Application.Services;

public class ApplicationService : IApplicationService
{
    private readonly string _filePath;
    private readonly string _connectionString;
    private readonly string _directory;
    private readonly IRepositoryService _repositoryService;
    private readonly ILogger<ApplicationService> _logger;

    public ApplicationService(string filePath, string connectionString, IRepositoryService repositoryService, string directory, ILogger<ApplicationService> logger)
    {
        _filePath = filePath;
        _connectionString = connectionString;
        _repositoryService = repositoryService;
        _directory = directory;
        _logger = logger;
    }

    #region User

    public void AddUser(UserDto user)
    {
        try
        {
            _repositoryService.AddUser(user);
            _logger.LogInformation("Application Service - user with userGuid : {userGuid} added succesfully", user.UserGuid);
        }
        catch (Exception ex)
        {
            throw new Exception($"Application Service - Error during adding user with guid {user.UserGuid}", ex);
        }
    }

    public void UpdateUser(UserDto user)
    {
        try
        {
            _repositoryService.UpdateUser(user);
            _logger.LogInformation("Application Service - user with userGuid : {userGuid} updated succesfully", user.UserGuid);
        }
        catch (Exception ex)
        {
            throw new Exception($"Application Service - Error during updating user with guid {user.UserGuid}", ex);
        }
    }

    public void DeleteUser(Guid userGuid)
    {
        try
        {
            _repositoryService.DeleteUser(userGuid);
            _logger.LogInformation("Application Service - user with userGuid : {userGuid} deleted succesfully", userGuid);
        }
        catch (Exception ex)
        {
            throw new Exception("Application Service - Error during retrieving user by guid.", ex);
        }
    }

    public UserDto GetUserByGuid(Guid userGuid)
    {
        try
        {
            UserDto user = _repositoryService.GetUserByGuid(userGuid);
            if (user != null)
                return user;

            _logger.LogInformation("Application Service - No user found with guid {userGuid}", userGuid);
            return null;
        }
        catch (Exception ex)
        {
            throw new Exception("Application Service - Error during retrieving user by guid.", ex);
        }
    }

    public List<UserDto> GetAllUsers()
    {
        try
        {
            List<UserDto> users = _repositoryService.GetAllUsers();
            _logger.LogInformation("Application Service - List of all users returned correctly");
            return users;
        }
        catch (Exception ex)
        {
            throw new Exception("Application Service - Error during retrieving all users.", ex); // todo db null return
        }
    }

    #endregion


    #region BlogPost

    public void AddBlogPost(BlogPostDto blogPost)
    {
        try
        {
            _repositoryService.AddBlogPost(blogPost);
            _logger.LogInformation("Application Service - post with post guid: {postGuid} added succesfully", blogPost.PostGuid);
        }
        catch (Exception ex)
        {
            throw new Exception($"Application Service - Error during adding blog post with guid {blogPost.PostGuid}", ex);
        }
    }

    public void UpdateBlogPost(BlogPostDto blogPost)
    {
        try
        {
            _repositoryService.UpdateBlogPost(blogPost);
            _logger.LogInformation("Application Service - post with post guid: {postGuid} updated succesfully", blogPost.PostGuid);
        }
        catch (Exception ex)
        {
            throw new Exception($"Application Service - Error during updating blog post with guid {blogPost.PostGuid}", ex);
        }
    }

    public void DeleteBlogPost(Guid blogPostGuid)
    {
        try
        {
            _repositoryService.DeleteBlogPost(blogPostGuid);
            _logger.LogInformation("Application Service - post with post guid: {postGuid} deleted succesfully", blogPostGuid);
        }
        catch (Exception ex)
        {
            throw new Exception($"Application Service - Error during deleting blog post with guid {blogPostGuid}", ex);
        }
    }

    public BlogPostDto GetBlogPostByGuid(Guid blogPostGuid)
    {
        try
        {
            BlogPostDto blogPost = _repositoryService.GetBlogPostByGuid(blogPostGuid);
            if (blogPost != null)
                return blogPost;

            _logger.LogInformation("Application Service - No blog post found with guid {blogPostGuid}", blogPostGuid);
            return null;
        }
        catch (Exception ex)
        {
            throw new Exception("Application Service - Error during retrieving blog post by guid.", ex);
        }
    }

    public List<BlogPostDto> GetAllBlogPosts()
    {
        try
        {
            List<BlogPostDto> blogPosts = _repositoryService.GetAllBlogPosts();
            _logger.LogInformation("Application Service - List of all blog posts returned correctly");
            return blogPosts;
        }
        catch (Exception ex)
        {
            throw new Exception("Application Service - Error during retrieving all blog posts.", ex);
        }
    }

    #endregion

    #region initialize
    public void CreateDataBase()
    {
        if (!File.Exists(_filePath))
        {
            _logger.LogInformation("Database file not found at {filePath}. Creating new database...", _filePath);
            _repositoryService.InitializeTables(_directory ,_filePath);
        }
        else
        {
            _logger.LogInformation("Database file found at {filePath}. No need to create tables.", _filePath);
        }
    }
    #endregion
}
