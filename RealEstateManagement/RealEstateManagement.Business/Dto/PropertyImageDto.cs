namespace RealEstateManagement.Business.Dto
{
    public class PropertyImageDto
    {
        public int Id { get; set; }

        public string ImageUrl { get; set; } = null!;

        public int DisplayOrder { get; set; }

        public bool IsPrimary { get; set; }

        public int PropertyId { get; set; }
    }
}
