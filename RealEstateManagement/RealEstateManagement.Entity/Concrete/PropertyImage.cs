using RealEstateManagement.Entity.Abstract;

namespace RealEstateManagement.Entity.Concrete
{
    public class PropertyImage : BaseClass
    {
        public string ImageUrl { get; set; } = "";

        public int PropertyId { get; set; }
        public Property? Property { get; set; }
    }
}