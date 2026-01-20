using System;

namespace RealEstateManagement.Entity.Concrete
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public string Token { get; set; } = string.Empty;
        public int UserId { get; set; }
        public DateTime Expiration { get; set; }
        public bool IsRevoked { get; set; } = false;

        public AppUser User { get; set; } = null!;
    }
}