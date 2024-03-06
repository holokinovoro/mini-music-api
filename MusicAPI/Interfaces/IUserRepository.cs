using MusicAPI.Models;

namespace MusicAPI.Interfaces
{
    public interface IUserRepository
    {
        ICollection<User> GetUsers();

        void CreateUser(string name, string password);
    }
}
