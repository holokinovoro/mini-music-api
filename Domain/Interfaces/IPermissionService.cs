
using Domain.Enums;

namespace Domain.Interfaces;

public interface IPermissionService
{
    Task<HashSet<Permission>> GetPermissionsAsync(Guid userId);
}