using Microsoft.AspNetCore.Identity;

namespace RealEstateManagement.Entity.Concrete;

public class AppUser : IdentityUser
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string ProfilePicture { get; set; } = "";
    public string IsAgent { get; set; } = "Hayır";
    public string AgencyName { get; set; } = "";
    public string LicenseNumber { get; set; } = "";
    public ICollection<Property>? Properties { get; set; }
    public ICollection<Inquiry>? Inquiries { get; set; }
}
