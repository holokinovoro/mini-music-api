using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Application.Dto;
using Domain.Models;
using Application.Interfaces.IRepository;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public async Task Add(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetByEmail(string email)
        {
            var user = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email) ?? throw new ArgumentNullException
                (nameof(email));

            return user;
        }
    }
}
