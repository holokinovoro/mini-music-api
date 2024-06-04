using Application.Dto;
using Domain.Models;

namespace Application.Interfaces.IRepository
{
    public interface IUserRepository
    {
        public Task Add(User user);

        public Task<User> GetByEmail(string email);

    }
}
