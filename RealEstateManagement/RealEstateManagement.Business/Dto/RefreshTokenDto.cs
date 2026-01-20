using System;

namespace RealEstateManagement.Business.Dto;

public class RefreshTokenDto
{
    public string Token { get; set; } = string.Empty;
    public DateTime Expiration { get; set; }
}
