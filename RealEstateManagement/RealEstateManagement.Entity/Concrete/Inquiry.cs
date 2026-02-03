using RealEstateManagement.Entity.Abstract;

namespace RealEstateManagement.Entity.Concrete
{
    public class Inquiry : BaseClass
    {
        public string FullName { get; set; } = "";
        public string Email { get; set; } = "";
        public string Phone { get; set; } = "";
        public string Message { get; set; } = "";
        //public int UserId { get; set; }
        public string Status { get; set; } = "";
        public Property? Property { get; set; }
        public AppUser User { get; set; } = new AppUser();
    }

}