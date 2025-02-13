﻿using BloggingPlatform_BE.Application.DTOs;
using BloggingPlatform_BE.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace BloggingPlatform_BE.Infrastructure.Controllers
{
    [ApiController]
    [Route("api/Authentication")]
    public class AuthenticationController : Controller
    {

        private readonly ILogger<AuthenticationController> _logger;
        private readonly AuthenticationService _authService;

        public AuthenticationController(ILogger<AuthenticationController> logger, AuthenticationService authService)
        {
            _logger = logger;
            _authService = authService;
        }

        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult AuthenticateUser(UserDto user)
        {
            {
                try
                {
                    UserDto? repoUser = _authService.AuthenticateUser(user);

                    if (repoUser != null)
                    {
                        // the returned user must not show the salt and the hashcode for security reasons
                        repoUser.Salt = "";
                        repoUser.HashCode = "";
                        // -- 

                        _logger.LogInformation("Authentication Controller - Authenticate user call executed succesfully with status code <{statusCode}>", StatusCodes.Status200OK);
                        return Ok(repoUser);
                    }
                    _logger.LogInformation("Authentication Controller - Authenticate user call executed succesfully but user was not authenticated");
                    return Problem("User not authenticated");
                }
                catch (Exception ex)
                {
                    _logger.LogError("Blogging Platform Controller - Authenticate user call not executed. {exMessage}", ex.Message);
                    return BadRequest();
                }
            }
        }
    }
}
