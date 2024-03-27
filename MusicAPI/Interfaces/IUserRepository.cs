using MusicAPI.Dto;
using MusicAPI.Models;

namespace MusicAPI.Interfaces
{
    public interface IUserRepository
    {
        ICollection<User> GetUsers();

        User GetUser(int id);
        User GetUser(string username);

        User GetUserTrimToUpper(UserDto userCreate);

        bool UserExists(int userId);
        bool UserExists(string name);


        bool CreateUser(User user);
        bool UpdateUser(User user);

        bool DeleteUser(User userId);

        bool Save();

    }
}
