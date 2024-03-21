using MusicAPI.Models;
using MusicAPI.Dto;

namespace MusicAPI.IRepository
{
    public interface IUserRepository
    {
        ICollection<LocalUser> GetUsers();

        LocalUser GetUser(int id);
        LocalUser GetUser(string username);

        LocalUser GetUserTrimToUpper(UserDto userCreate);

        bool UserExists(int userId);
        bool UserExists(string name);


        bool CreateUser(LocalUser user);
        bool UpdateUser(LocalUser user);

        bool DeleteUser(LocalUser userId);

        bool Save();

    }
}
