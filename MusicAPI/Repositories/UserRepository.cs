using Microsoft.EntityFrameworkCore;
using MusicAPI.Data;
using MusicAPI.Dto;
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
            return  _context.Users.OrderBy(p => p.Id).ToList();
        }

        

        public  User GetUser(int id)
        {
            return  _context.Users.Where(p => p.Id == id).FirstOrDefault();
        }

        public User GetUser(string name)
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

        public User GetUserTrimToUpper(UserDto userCreate)
        {
            return GetUsers().Where(e => e.UserName.Trim().ToUpper() == userCreate.UserName.TrimEnd().ToUpper())
                .FirstOrDefault();
        }

        public bool CreateUser(User user)
        {
            _context.Add(user);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
