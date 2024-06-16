using Domain.Enums;
using Microsoft.AspNetCore.Authorization;

namespace Infrastructure.Authentication;

public class PermissionRequirement : IAuthorizationRequirement
{

    public PermissionRequirement(Permission[] permissions )
    {
        Permissions = permissions;
    }
    public Permission[] Permissions { get; set; }
}
