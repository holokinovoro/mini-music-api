using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicAPI.Data;
using MusicAPI.Interfaces;
using MusicAPI.Models;
using MusicAPI.Repositories;

namespace MusicAPI.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
        public async Task<IActionResult> GetUsers()
        {
            var users =  _userRepository.GetUsers();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(users);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(string name, string password)
        {
            _userRepository.CreateUser(name, password);
            return Ok();
        }
    }
}
