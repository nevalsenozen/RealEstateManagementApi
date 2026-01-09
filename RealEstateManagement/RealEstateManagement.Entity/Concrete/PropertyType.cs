using RealEstateManagement.Entity.Abstract;

namespace RealEstateManagement.Entity.Concrete
{
    public class PropertyType : BaseClass
    {
        public string Name { get; set; } = "";
        public string? Description { get; set; }

        public ICollection<Property> Properties {get;set;} = new List<Property>();
    }
}