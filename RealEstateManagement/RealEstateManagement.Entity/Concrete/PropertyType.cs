using RealEstateManagement.Entity.Abstract;

namespace RealEstateManagement.Entity.Concrete
{
    public class PropertyType : BaseEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }
    }
}