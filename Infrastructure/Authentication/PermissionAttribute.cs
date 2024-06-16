using Microsoft.AspNetCore.Authorization;


namespace Infrastructure.Authentication;

public class PermissionAttribute : AuthorizeAttribute, IAuthorizationRequirement
{
    public PermissionAttribute(string permission) => Permission = permission;

    public string Permission { get;}

    public IEnumerable<IAuthorizationRequirement> GetRequirements()
    {
        yield return this;
    }
}

