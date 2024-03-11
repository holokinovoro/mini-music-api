using MusicAPI.Models;

namespace MusicAPI.Interfaces
{
    public interface IUserRepository
    {
        ICollection<User> GetUsers();

        User GetUser(int id);
        User GetUser(string username);

        bool UserExists(int userId);
        bool UserExists(string name);


        void CreateUser(string name, string password);
    }
}
