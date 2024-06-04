using System.ComponentModel.DataAnnotations;

namespace MusicAPI.Contracts.Users
{
    public record LoginUserRequest(
        [Required] string Email,
        [Required] string Password);
    
}
