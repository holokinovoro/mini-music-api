using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Application.Dto;
using Domain.Models;
using Application.Services;
using MusicAPI.Contracts.Users;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using Application.Features.Queries.User;
using Application.Features.Commands.UserCommands.Delete;

namespace MusicAPI.Controllers
{
    [ApiController]
    [Route("api/users")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<UserController> _logger;
        private readonly UserService _userService;

        public UserController(
            IMediator mediator,
            ILogger<UserController> logger,
            UserService userService)
        {
            _mediator = mediator;
            _logger = logger;
            _userService = userService;
        }


        [HttpPost("/register")]
        [AllowAnonymous]
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
        [AllowAnonymous]
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

        [HttpGet("/users")]
        [Authorize(Policy = "PermissionCreate")]
        public async Task<IActionResult> GetAllUsers()
        {
            var request = new GetUsersQuery();
            var response = await _mediator.Send(request);

            if (!ModelState.IsValid)
            {
                _logger.LogError("Failed get session for user");
                return BadRequest();
            }

            _logger.LogInformation("Get session for user");
            return Ok(response);
        }

        [HttpGet("/users/{Id}")]
        [Authorize(Policy = "PermissionCreate")]
        public async Task<IActionResult> GetUserById(Guid Id)
        {
            var request = new GetUserById
            {
                Id = Id
            };

            var response = await _mediator.Send(request);

            if (!ModelState.IsValid)
            {
                _logger.LogError("Failed get session for user {Id}", request.Id);
                return BadRequest();
            }

            _logger.LogInformation("Get session for user {Id}", request.Id);
            return Ok(response);
        }

        [HttpDelete("{Id}")]
        [Authorize(Policy = "PermissionDelete")]
        public async Task<IActionResult> DeleteUser(Guid Id)
        {
            var request = new DeleteUserCommand
            {
                userId = Id
            };

            var response = await _mediator.Send(request);

            if (!ModelState.IsValid)
            {
                _logger.LogError("Failed delete session for user {Id}", request.userId);
                return BadRequest();
            }

            _logger.LogInformation("Delete session for user {Id}", request.userId);

            return Ok(response);
        }



    }
}