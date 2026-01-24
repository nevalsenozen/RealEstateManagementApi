using System;

namespace RealEstateManagement.Business.Dto;

public class PropertyFilterDto
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public string? City { get; set; }
    public string? District { get; set; }
    public int? MinRooms { get; set; }
    public int? MaxRooms { get; set; }
    public decimal? MinArea { get; set; }
    public decimal? MaxArea { get; set; }
    public int? PropertyTypeId { get; set; }
    public string? Status { get; set; }
    public string? AgentId { get; set; }
    public int? MinYear { get; set; }
    public int? MaxYear { get; set; }
    public string SortBy { get; set; } = "createdAt";  
    public string SortOrder { get; set; } = "desc";
}


