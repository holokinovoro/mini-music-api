using MusicAPI.Data;
using MusicAPI.Interfaces;
using MusicAPI.Models;
using System.Collections.ObjectModel;

namespace MusicAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<User> GetUsers()
        {
            return _context.Users.OrderBy(p => p.Id).ToList();
        }

        public void CreateUser(string name, string password)
        {
            var user = new User
            {
                UserName = name,
                Password = password
            };
            _context.Users.Add(user);
            _context.SaveChanges();
        }

    }
}
