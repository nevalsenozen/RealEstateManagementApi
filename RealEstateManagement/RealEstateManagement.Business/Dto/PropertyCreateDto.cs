using System;

namespace RealEstateManagement.Business.Dto;

public class PropertyCreateDto
{
    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public decimal Price { get; set; } 

    public string Address { get; set; } = string.Empty;

    public string City { get; set; } = string.Empty;

    public string District { get; set; } = string.Empty;

    // ev özelikleri

    public int Rooms { get; set; }

    public int Bathrooms { get; set; }

    public int Area { get; set; }

    public int Floor { get; set; }
    
    public int TotalFloors { get; set; }

    public int YearBuilt { get; set; }

    public int PropertyTypeId { get; set; }
}
