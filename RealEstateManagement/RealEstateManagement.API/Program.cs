using Microsoft.EntityFrameworkCore;
using RealEstateManagement.Data;
using AutoMapper; // AutoMapper için ekle
using RealEstateManagement.Business.Mapping; // MappingProfile’in namespace'i

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// AutoMapper register et
builder.Services.AddAutoMapper(typeof(MappingProfile));

// DbContext register
builder.Services.AddDbContext<RealEstateManagementDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSql")));

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();