using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Application.Dto;
using Domain.Models;
using Application.Services;
using MusicAPI.Contracts.Users;
using Microsoft.AspNetCore.Authorization;

namespace MusicAPI.Controllers
{
    [ApiController]
    [Route("api/users")]
    [AllowAnonymous]
    public class UserController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly UserService _userService;

        public UserController(
            ILogger logger,
            UserService userService)
        {
            _logger = logger;
            _userService = userService;
        }


        [HttpPost("/register")]
        public async Task<IResult> Register(
            [FromQuery]RegisterUserRequest request)
        {
            if (request == null)
            {
                _logger.LogError("Request is empty");
            }
            await _userService.Register(request.UserName, request.Email, request.Password);

            if (!ModelState.IsValid)
            {
                _logger.LogError("Failed registraion session for user");
                return Results.BadRequest();
            }

            _logger.LogInformation("Registration session for user");
            return Results.Ok();
        }

        [HttpPost("/login")]
        public async Task<IResult> Login(
            [FromQuery]LoginUserRequest request)
        {

            if (request == null)
            {
                _logger.LogError("Request is empty");
            }
            var context = HttpContext;

            var token = await _userService.Login(request.Email, request.Password);

            context.Response.Cookies.Append("pookie-cookies",token);

            if (!ModelState.IsValid)
            {
                _logger.LogError("Failed login session for user");
                return Results.BadRequest();
            }
            _logger.LogInformation("Login session for user");
            return Results.Ok();
        }
    }
}