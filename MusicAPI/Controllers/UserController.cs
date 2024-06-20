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
        private readonly UserService userService;

        public UserController(UserService userService)
        {
            this.userService = userService;
        }


        [HttpPost("/register")]
        public async Task<IResult> Register(
            [FromQuery]RegisterUserRequest request)
        {
            await userService.Register(request.UserName, request.Email, request.Password);


            return Results.Ok();
        }

        [HttpPost("/login")]
        public async Task<IResult> Login(
            [FromQuery]LoginUserRequest request)
        {
            var context = HttpContext;

            var token = await userService.Login(request.Email, request.Password);

            context.Response.Cookies.Append("pookie-cookies",token);

            Console.WriteLine("Элиза котек");


            return Results.Ok();
        }
    }
}