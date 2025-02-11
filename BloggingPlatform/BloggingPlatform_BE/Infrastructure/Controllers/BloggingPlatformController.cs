using BloggingPlatform_BE.Application.DTOs;
using BloggingPlatform_BE.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BloggingPlatform_BE.Infrastructure.Controllers;

[ApiController]
[Route("/api/BloggingPlatform")]
public class BloggingPlatformController : Controller
{

    private readonly IApplicationService _service;

    public BloggingPlatformController(IApplicationService service)
    {
        _service = service;
    }

    #region Users

    [HttpPost]
    [Route("[action]")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult AddUser(UserDto user)
    {
        _service.AddUser(user);
        return Ok();
    }

    [HttpPut]
    [Route("[action]")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult UpdateUser(UserDto user)
    {
        _service.UpdateUser(user);
        return Ok();
    }

    [HttpDelete]
    [Route("[action]/{userGuid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult DeleteUser(string userGuid)
    {
        _service.DeleteUser(Guid.Parse(userGuid));
        return Ok();
    }

    [HttpGet]
    [Route("[action]/{userGuid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetUserByGuid(string userGuid)
    {
        return Ok(_service.GetUserByGuid(Guid.Parse(userGuid)));
    }

    [HttpGet]
    [Route("[action]")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetAllUsers()
    {
        return Ok(_service.GetAllUsers());
    }

    #endregion

    #region BlogPosts

    [HttpPost]
    [Route("[action]")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult AddBlogPost(BlogPostDto blogPost)
    {
        _service.AddBlogPost(blogPost);
        return Ok();
    }

    [HttpPut]
    [Route("[action]")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult UpdateBlogPost(BlogPostDto blogPost)
    {
        _service.UpdateBlogPost(blogPost);
        return Ok();
    }

    [HttpDelete]
    [Route("[action]/{blogPostGuid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult DeleteBlogPost(string blogPostGuid)
    {
        _service.DeleteBlogPost(Guid.Parse(blogPostGuid));
        return Ok();
    }

    [HttpGet]
    [Route("[action]/{blogPostGuid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetBlogPostByGuid(string blogPostGuid)
    {
        return Ok(_service.GetBlogPostByGuid(Guid.Parse(blogPostGuid)));
    }

    [HttpGet]
    [Route("[action]")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetAllBlogPosts()
    {
        return Ok(_service.GetAllBlogPosts());
    }

    #endregion
}
