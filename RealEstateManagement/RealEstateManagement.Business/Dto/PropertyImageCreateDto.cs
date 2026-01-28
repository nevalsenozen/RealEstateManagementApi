using System;

namespace RealEstateManagement.Business.Dto;

public class PropertyImageCreateDto
{
        public string ImageUrl { get; set; } = "";

        public int PropertyId { get; set; }

        public int DisplayOrder { get; set; }

        public bool IsPrimary { get; set; }
}
