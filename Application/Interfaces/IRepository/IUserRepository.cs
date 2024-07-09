using Application.Dto;
using Domain.Enums;
using Domain.Models;

namespace Application.Interfaces.IRepository
{
    public interface IUserRepository
    {
        public Task Add(User user);

        public Task<User> GetByEmail(string email);

        public Task<HashSet<Permission>> GetUserPermissions(Guid userId);

        public Task<ICollection<User>> GetUsers(CancellationToken cancellationToken = default);

        public Task<User> GetUserById(Guid userId, CancellationToken cancellationToken = default);

        public Task Update(User user, CancellationToken cancellationToken = default);

        public Task Delete(Guid userId, CancellationToken cancellationToken = default);

    }
}
