using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Application.Dto;
using Domain.Models;
using Application.Interfaces.IRepository;
using Domain.Enums;
using AutoMapper;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public UserRepository(IMapper mapper,DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task Add(User user)
        {
            var roleEntity = await _context.Roles
            .SingleOrDefaultAsync(r => r.Id == (int)Role.User)
            ?? throw new InvalidOperationException();

            ICollection<RoleEntity> roles = new List<RoleEntity> { };

            roles.Append(roleEntity);

            var userEntity = new UserEntity()
            {
                Id = user.Id,
                UserName = user.UserName,
                PasswordHash = user.PasswordHash,
                Email = user.Email,
                Roles = roles
            };

            var userRole = new UserRole()
            {
                UserId = userEntity.Id,
                RoleId = roleEntity.Id
            };

            await _context.UserRoles.AddAsync(userRole);
            await _context.Users.AddAsync(userEntity);
            await _context.SaveChangesAsync();
        }


        public async Task<User> GetByEmail(string email)
        {
            var userEntity = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email) ?? throw new ArgumentNullException
                (nameof(email));


            return _mapper.Map<User>(userEntity);
        }


        public async Task<HashSet<Permission>> GetUserPermissions(Guid userId)
        {
            var roles = await _context.Users
                .AsNoTracking()
                .Include(u => u.Roles)
                .ThenInclude(r => r.Permissions)
                .Where(u => u.Id == userId)
                .Select(u => u.Roles)
                .ToArrayAsync();

            return roles
                .SelectMany(r => r)
                .SelectMany(r => r.Permissions)
                .Select(p => (Permission)p.Id)
                .ToHashSet();
        }

        public async Task<ICollection<User>> GetUsers(CancellationToken cancellationToken = default)
        {
            var usersEntity = await _context.Users
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return _mapper.Map<ICollection<User>>(usersEntity);
        }
        public async Task<User> GetUserById(Guid userId, CancellationToken cancellationToken = default)
        {
            var userEntity = await _context.Users
                .AsNoTracking()
                .Where(u => u.Id == userId)
                .FirstOrDefaultAsync(cancellationToken);

            return _mapper.Map<User>(userEntity);
        }

        public async Task Delete(Guid userId, CancellationToken cancellationToken = default)
        {
            var userEntity = await _context.Users
                .AsNoTracking()
                .Where(u => u.Id == userId)
                .FirstOrDefaultAsync(cancellationToken);
            _context.Remove(userEntity);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
