using Microsoft.EntityFrameworkCore;
using MusicAPI.Data;
using MusicAPI.IRepository;
using MusicAPI.Models;
using MusicAPI.Dto;
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

        public ICollection<LocalUser> GetUsers()
        {
            return  _context.Users.OrderBy(p => p.Id).ToList();
        }

        

        public  LocalUser GetUser(int id)
        {
            return  _context.Users.Where(p => p.Id == id).FirstOrDefault();
        }

        public LocalUser GetUser(string name)
        {
            return  _context.Users.Where(p => p.UserName == name).FirstOrDefault();
        }

        public bool UserExists(int userId)
        {
            return _context.Users.Any(p => p.Id == userId);
        }

        public bool UserExists(string name)
        {
            return _context.Users.Any(p => p.UserName == name);
        }

        public LocalUser GetUserTrimToUpper(UserDto userCreate)
        {
            return GetUsers().Where(e => e.UserName.Trim().ToUpper() == userCreate.UserName.TrimEnd().ToUpper())
                .FirstOrDefault();
        }

        public bool CreateUser(LocalUser user)
        {
            _context.Add(user);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateUser(LocalUser user)
        {
            _context.Update(user);
            return Save();
        }

        public bool DeleteUser(LocalUser user)
        {
            _context.Remove(user);
            return Save();
        }
    }
}
