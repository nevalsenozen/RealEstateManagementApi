using System;

namespace RealEstateManagement.Business.Dto;

public class RefreshTokenCreateDto
{
    public string Token { get; set; } = string.Empty;
    public int UserId { get; set; }
}
