using Microsoft.EntityFrameworkCore;
using RealEstateManagement.Entity.Concrete;

namespace RealEstateManagement.Data
{
    public class RealEstateManagementDbContext : DbContext
    {
        public RealEstateManagementDbContext(
            DbContextOptions<RealEstateManagementDbContext> options)
            : base(options)
        {
        }

        public DbSet<Property> Properties { get; set; }
    }
}

