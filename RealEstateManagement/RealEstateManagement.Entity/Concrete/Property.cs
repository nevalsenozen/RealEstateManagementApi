using RealEstateManagement.Entity.Abstract;
using RealEstateManagement.Entity.Enums;

namespace RealEstateManagement.Entity.Concrete
{
    public class Property : BaseClass
    {
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public decimal Price { get; set; }

        public string Address { get; set; } = "";
        public string City { get; set; } = "";
        public string? District { get; set; }

        public int Rooms { get; set; }
        public int? Bathrooms { get; set; }
        public decimal Area { get; set; }

        public int Floor { get; set; }
        public int? TotalFloors { get; set; }
        public int YearBuilt { get; set; }

        public PropertyStatus Status { get; set; }

        public int PropertyTypeId { get; set; }
        public PropertyType? PropertyType { get; set; } 

        public ICollection<PropertyImage>? Images { get; set; } 
        public ICollection<Inquiry>? Inquiries { get; set; }

    }
}