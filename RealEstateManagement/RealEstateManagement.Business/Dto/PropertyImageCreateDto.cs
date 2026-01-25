using System;

namespace RealEstateManagement.Business.Dto;

public class PropertyImageCreateDto
{
        public string ImageUrl { get; set; } = "";

        public int PropertyId { get; set; }
}
