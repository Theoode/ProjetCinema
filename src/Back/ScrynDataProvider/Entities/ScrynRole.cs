using Microsoft.AspNetCore.Identity;
using ScrynDomain.Entities;

namespace ScrynDataProvider.Entities;

public class ScrynRole: IdentityRole, IRole
{
    private string _role = string.Empty;
    
    public ScrynRole() : base()
    {
    }
    public ScrynRole(string role) : base(role)
    {
        _role = role;
    }
}