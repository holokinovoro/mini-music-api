namespace Infrastructure;

public class AuthorizationOptions
{
    public RolePermissions[] RolePermissions { get; set; } = null!;
}

public class RolePermissions
{
    public string Role { get; set; } = string.Empty;

    public string[] Permission { get; set; } = null!;
}

