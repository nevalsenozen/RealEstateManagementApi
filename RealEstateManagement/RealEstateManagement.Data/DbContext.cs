using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using RealEstateManagement.Entity.Concrete;
using RealEstateManagement.Entity.Enums;

namespace RealEstateManagement.Data
{
    public class RealEstateManagementDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public RealEstateManagementDbContext(DbContextOptions<RealEstateManagementDbContext> options) 
        : base(options)
        {
        }

        public DbSet<Property> Property { get; set; }
        public DbSet<PropertyImage> PropertyImages { get; set; }
        public DbSet<PropertyType> PropertyTypes { get; set; }
        public DbSet<Inquiry> Inquiries { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.ConfigureWarnings(w => w.Ignore(RelationalEventId.PendingModelChangesWarning));
    }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Property>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<PropertyImage>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<PropertyType>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<Inquiry>().HasQueryFilter(x => !x.IsDeleted);


            #region Rol Bilgileri
            var appRoles = new AppRole[]
            {
                new AppRole { Id = "dd66d9d0-5aac-42a3-bd53-12ac0574cf1c", Name = "Admin", NormalizedName = "ADMIN", ConcurrencyStamp = "c4addbe1-adbc-4002-90dd-c4c2391eafb5", Description="Yönetici Rolü" },
                new AppRole { Id = "e676b617-bec3-4b92-a746-dfc5043ebe08", Name = "User", NormalizedName = "USER", ConcurrencyStamp = "ef25dd62-cbbc-45aa-8405-ffa6d8926664", Description="Kullanıcı Rolü" }
            };
            modelBuilder.Entity<AppRole>().HasData(appRoles);
            #endregion

            #region Kullanıcı Bilgileri
            var hasher = new PasswordHasher<AppUser>();
            var appUsers = new List<AppUser>();

            var appUser1 = new AppUser { Id = "819bee56-04d5-4ba7-8bd4-109d7607af95", FirstName = "Deniz", LastName = "Kerem", Email = "denizkerem@example.com", EmailConfirmed = true, UserName = "denizkerem", NormalizedEmail = "DENIZKEREM@EXAMPLE.COM", NormalizedUserName = "DENIZKEREM", ConcurrencyStamp = "087729db-48a9-434f-90a0-4fe1af527ff8", SecurityStamp = "a9aa84f6-6a60-45de-9070-40d23d2f403b" };

            var appUser2 = new AppUser { Id = "6c5e6042-9145-42cb-8220-e4aab7ea0cdb", FirstName = "Selin", LastName = "Dağ", Email = "selindag@example.com", EmailConfirmed = true, UserName = "selindag", NormalizedEmail = "SELINDAG@EXAMPLE.COM", NormalizedUserName = "SELINDAG", ConcurrencyStamp = "2f9ab541-0444-4ed4-beb6-10edae0e65bf", SecurityStamp = "8d096aa5-6071-4e16-8c8e-ecf542ec361d", };

            var appUser3 = new AppUser { Id = "a8c5e701-43ad-4949-ac22-32385e7cfd88", FirstName = "Ferda", LastName = "Can", Email = "ferdacan@example.com", EmailConfirmed = true, UserName = "ferdacan", NormalizedEmail = "FERDACAN@EXAMPLE.COM", NormalizedUserName = "FERDACAN", ConcurrencyStamp = "cfe87fd5-71af-41b1-98fe-8f16123a4966", SecurityStamp = "c1eeafd8-8177-4e60-ba9b-d67ab8f53be8" };


            appUser1.PasswordHash = hasher.HashPassword(appUser1, "Qwe123.,");
            appUser2.PasswordHash = hasher.HashPassword(appUser2, "Qwe123.,");
            appUser3.PasswordHash = hasher.HashPassword(appUser3, "Qwe123.,");

            modelBuilder.Entity<AppUser>().HasData(appUser1, appUser2, appUser3);
            #endregion

            #region Kullanıcı/Rol Atamaları
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    UserId = "819bee56-04d5-4ba7-8bd4-109d7607af95",
                    RoleId = "dd66d9d0-5aac-42a3-bd53-12ac0574cf1c"
                },
                new IdentityUserRole<string>
                {
                    UserId = "6c5e6042-9145-42cb-8220-e4aab7ea0cdb",
                    RoleId = "e676b617-bec3-4b92-a746-dfc5043ebe08"
                },
                new IdentityUserRole<string>
                {
                    UserId = "a8c5e701-43ad-4949-ac22-32385e7cfd88",
                    RoleId = "e676b617-bec3-4b92-a746-dfc5043ebe08"
                }
            );
            #endregion

            #region PropertyTypes
            var propertyTypes = new PropertyType[]
            {
                new PropertyType 
                { 
                    Id = 1, 
                    Name = "Apartman", 
                    Description = "Modern Apartman Dairesi", 
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new PropertyType 
                { 
                    Id = 2, 
                    Name = "Ev", 
                    Description = "Aile evi", 
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new PropertyType 
                { 
                    Id = 3, 
                    Name = "Villa", 
                    Description = "Lüx villa", 
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new PropertyType 
                { 
                    Id = 4, 
                    Name = "Ticari", 
                    Description = "Ticari mülk", 
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new PropertyType 
                { 
                    Id = 5, 
                    Name = "Arsa", 
                    Description = "Arsa parseli", 
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            };

            modelBuilder.Entity<PropertyType>().HasData(propertyTypes);
            #endregion PropertyTypes

            #region Properties
            var properties = new Property[]
            {
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
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
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
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
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
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
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
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            };
            modelBuilder.Entity<Property>().HasData(properties);
            #endregion Properties

            #region PropertyImages
            var propertyImages = new PropertyImage[]
            {
                new PropertyImage 
                { 
                    Id = 1, 
                    PropertyId = 1, 
                    ImageUrl = "https://images.unsplash.com/photo-1522708323590-d24dbb6b0267?w=500", 
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new PropertyImage 
                { 
                    Id = 2, 
                    PropertyId = 2, 
                    ImageUrl = "https://images.unsplash.com/photo-1502672260266-1c1ef2d93688?w=500", 
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new PropertyImage 
                { 
                    Id = 3, 
                    PropertyId = 3, 
                    ImageUrl = "https://images.unsplash.com/photo-1570129477492-45a003537e1f?w=500",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new PropertyImage 
                { 
                    Id = 4, 
                    PropertyId = 4, 
                    ImageUrl = "https://images.unsplash.com/photo-1564013799919-ab600027ffc6?w=500",
                    CreatedAt = DateTime.UtcNow, 
                    UpdatedAt = DateTime.UtcNow 
                }
            };
            modelBuilder.Entity<PropertyImage>().HasData(propertyImages);
            #endregion PropertyImages
        }
    }
}