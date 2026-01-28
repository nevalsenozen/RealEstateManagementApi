namespace RealEstateManagement.Business.Dto
{
    public class UserDto
    {
        public string Id { get; set; } = "";

        public string Email { get; set; } = "";

        public string FirstName { get; set; } = "";

        public string LastName { get; set; } = "";

        public string? PhoneNumber { get; set; }

        public string? ProfilePicture { get; set; }

        public bool IsAgent { get; set; }

        public string? AgencyName { get; set; }

        public string? LicenseNumber { get; set; }

        public IList<string> Roles { get; set; } = new List<string>();

        public DateTime CreatedAt { get; set; }
    }
}