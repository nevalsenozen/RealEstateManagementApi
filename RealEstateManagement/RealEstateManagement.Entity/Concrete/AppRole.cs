using Microsoft.AspNetCore.Identity;

namespace RealEstateManagement.Entity.Concrete;

public class AppRole : IdentityRole
{
    public string? Description { get; set; }
}
