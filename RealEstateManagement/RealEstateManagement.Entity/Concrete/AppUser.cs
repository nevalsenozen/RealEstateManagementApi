using Microsoft.AspNetCore.Identity;

namespace RealEstateManagement.Entity.Concrete;

public class AppUser : IdentityUser
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}
