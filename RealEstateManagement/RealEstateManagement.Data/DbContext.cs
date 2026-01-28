using Microsoft.EntityFrameworkCore;
using RealEstateManagement.Entity.Concrete;
using RealEstateManagement.Entity.Enums;

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
        public DbSet<PropertyImage> PropertyImages { get; set; }
        public DbSet<PropertyType> PropertyTypes { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<AppRole> AppRoles { get; set; }
        public DbSet<Inquiry> Inquiries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed PropertyTypes
            modelBuilder.Entity<PropertyType>().HasData(
                new PropertyType { Id = 1, Name = "Apartment", Description = "Modern apartment", CreatedAt = DateTime.UtcNow },
                new PropertyType { Id = 2, Name = "House", Description = "Single family house", CreatedAt = DateTime.UtcNow },
                new PropertyType { Id = 3, Name = "Villa", Description = "Luxury villa", CreatedAt = DateTime.UtcNow },
                new PropertyType { Id = 4, Name = "Commercial", Description = "Commercial property", CreatedAt = DateTime.UtcNow },
                new PropertyType { Id = 5, Name = "Land", Description = "Raw land", CreatedAt = DateTime.UtcNow }
            );

            // Seed Properties
            modelBuilder.Entity<Property>().HasData(
                new Property
                {
                    Id = 1,
                    Title = "Beautiful Apartment in City Center",
                    Description = "Spacious 2-bedroom apartment with modern amenities in the heart of the city",
                    Price = 250000,
                    Address = "123 Main Street",
                    City = "Istanbul",
                    District = "Beyoglu",
                    Rooms = 2,
                    Bathrooms = 1,
                    Area = 85,
                    Floor = 5,
                    TotalFloors = 10,
                    YearBuilt = 2020,
                    Status = PropertyStatus.Available,
                    PropertyTypeId = 1,
                    CreatedAt = DateTime.UtcNow
                },
                new Property
                {
                    Id = 2,
                    Title = "Luxury Villa with Garden",
                    Description = "Stunning 4-bedroom villa with swimming pool and large garden",
                    Price = 850000,
                    Address = "456 Park Avenue",
                    City = "Istanbul",
                    District = "Besiktas",
                    Rooms = 4,
                    Bathrooms = 3,
                    Area = 320,
                    Floor = 2,
                    TotalFloors = 2,
                    YearBuilt = 2018,
                    Status = PropertyStatus.Available,
                    PropertyTypeId = 3,
                    CreatedAt = DateTime.UtcNow
                },
                new Property
                {
                    Id = 3,
                    Title = "Cozy House with Backyard",
                    Description = "Perfect family home with 3 bedrooms and nice backyard",
                    Price = 450000,
                    Address = "789 Oak Lane",
                    City = "Ankara",
                    District = "Cankaya",
                    Rooms = 3,
                    Bathrooms = 2,
                    Area = 150,
                    Floor = 1,
                    TotalFloors = 1,
                    YearBuilt = 2015,
                    Status = PropertyStatus.Available,
                    PropertyTypeId = 2,
                    CreatedAt = DateTime.UtcNow
                },
                new Property
                {
                    Id = 4,
                    Title = "Commercial Office Space",
                    Description = "Modern office space in business district",
                    Price = 180000,
                    Address = "321 Business Street",
                    City = "Izmir",
                    District = "Alsancak",
                    Rooms = 5,
                    Bathrooms = 2,
                    Area = 200,
                    Floor = 3,
                    TotalFloors = 8,
                    YearBuilt = 2019,
                    Status = PropertyStatus.Available,
                    PropertyTypeId = 4,
                    CreatedAt = DateTime.UtcNow
                }
            );

            // Seed PropertyImages
            modelBuilder.Entity<PropertyImage>().HasData(
                new PropertyImage { Id = 1, PropertyId = 1, ImageUrl = "https://images.unsplash.com/photo-1522708323590-d24dbb6b0267?w=500", CreatedAt = DateTime.UtcNow },
                new PropertyImage { Id = 2, PropertyId = 1, ImageUrl = "https://images.unsplash.com/photo-1502672260266-1c1ef2d93688?w=500", CreatedAt = DateTime.UtcNow },
                new PropertyImage { Id = 3, PropertyId = 2, ImageUrl = "https://images.unsplash.com/photo-1570129477492-45a003537e1f?w=500", CreatedAt = DateTime.UtcNow },
                new PropertyImage { Id = 4, PropertyId = 3, ImageUrl = "https://images.unsplash.com/photo-1564013799919-ab600027ffc6?w=500", CreatedAt = DateTime.UtcNow }
            );
        }
    }
}

