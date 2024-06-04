using System.ComponentModel.DataAnnotations;

namespace MusicAPI.Contracts.Users
{
    public record RegisterUserRequest(
        [Required] string UserName,
        [Required] string Password,
        [Required] string Email);

}