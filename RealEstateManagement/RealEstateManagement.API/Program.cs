using Microsoft.EntityFrameworkCore;
using RealEstateManagement.Data;
using RealEstateManagement.Data.Abstract;
using RealEstateManagement.Data.Concrete;
using RealEstateManagement.Business.Mapping;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using RealEstateManagement.Business.Concrete;
using RealEstateManagement.Business.Abstract;
using RealEstateManagement.API.Middleware;
using FluentValidation;
using AspNetCoreRateLimit;
using FluentValidation.AspNetCore;
using RealEstateManagement.API.Filters;
using RealEstateManagement.Business.Configs;
using RealEstateManagement.Business.Validators;
using RealEstateManagement.Entity.Concrete;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers(options =>
{
    options.Filters.Add<ValidationFilter>();
});


builder.Services.AddOpenApi();



var connectionString = builder.Configuration.GetConnectionString("PostgreSqlConnection");

if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("Connection string 'PostgreSqlConnection' bulunamadı!");
}

builder.Services.AddDbContext<RealEstateManagementDbContext>(options => options.UseNpgsql(connectionString));

// Repository & Unit of Work
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

// Business Services
builder.Services.AddScoped<IPropertyImageService, PropertyImageService>();
builder.Services.AddScoped<IPropertyService, PropertyService>();
builder.Services.AddScoped<IPropertyTypeService, PropertyTypeService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IInquiryService, InquiryService>();
builder.Services.AddScoped<IAuthService, AuthService>();

// AutoMapper
builder.Services.AddAutoMapper(cfg => { }, typeof(MappingProfile));

builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection("JwtConfig"));
builder.Services.Configure<AppUrlConfig>(builder.Configuration.GetSection("AppUrlConfig"));
builder.Services.Configure<CorsConfig>(builder.Configuration.GetSection("CorsSettings"));


// Add FluentValidation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<InquiryCreateDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<InquiryUpdateDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<LoginDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<PropertyCreateDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<PropertyFilterDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<PropertyImageCreateDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<PropertyImageUpdateDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<PropertyTypeCreateDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<PropertyTypeUpdateDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<PropertyUpdateDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<RegisterDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UserDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UserUpdateDtoValidator>();


builder.Services.AddIdentity<AppUser, AppRole>(options =>
{
    // Şifre politikalarımız
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 8;

    // Kullanıcı politikalarımız
    options.User.RequireUniqueEmail = true;
}).AddEntityFrameworkStores<RealEstateManagementDbContext>().AddDefaultTokenProviders();


var jwtConfig = builder.Configuration.GetSection("JwtConfig").Get<JwtConfig>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(opt =>
{
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = jwtConfig!.Issuer,
        ValidateAudience = true,
        ValidAudience = jwtConfig.Audience,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.Secret))
    };
});


// Rate Limiting
builder.Services.AddMemoryCache();
builder.Services.Configure<IpRateLimitOptions>(builder.Configuration.GetSection("IpRateLimitPolicies"));
builder.Services.AddInMemoryRateLimiting();
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

// CORS
var corsConfig = builder.Configuration.GetSection("CorsSettings").Get<CorsConfig>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins", policy =>
    {
        if (corsConfig!.AllowedOrigins.Length > 0)
        {
            policy
                .WithOrigins(corsConfig.AllowedOrigins)
                .WithMethods(corsConfig.AllowedMethods)
                .WithHeaders(corsConfig.AllowedHeaders);
        }
        else
        {
            policy
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        }
        if (corsConfig.AllowCredentials)
        {
            policy.AllowCredentials();
        }
    });
});


//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<RealEstateManagementDbContext>();
    var migrationLogger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
    var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();
    
    try
    {
        var connString = configuration.GetConnectionString("PostgreSqlConnection");
        var maskedConnString = connString?.Replace("Password=", "Password=*");
        migrationLogger.LogInformation("=== Migration İşlemi Başlatılıyor ===");
        migrationLogger.LogInformation("Connection String: {ConnectionString}", maskedConnString);
        migrationLogger.LogInformation("Veritabanı bağlantısı kontrol ediliyor...");
        
        var canConnect = await dbContext.Database.CanConnectAsync();
        migrationLogger.LogInformation("Veritabanına bağlanılabilir: {CanConnect}", canConnect);
        
        if (!canConnect)
        {
            migrationLogger.LogError("❌ Veritabanına bağlanılamıyor! Connection string'i kontrol edin.");
            throw new InvalidOperationException("Veritabanına bağlanılamıyor!");
        }
        
        var pendingMigrations = await dbContext.Database.GetPendingMigrationsAsync();
        migrationLogger.LogInformation("Bekleyen migration sayısı: {Count}", pendingMigrations.Count());
        
        if (pendingMigrations.Any())
        {
            migrationLogger.LogInformation("Uygulanacak migration'lar: {Migrations}", string.Join(", ", pendingMigrations));
            await dbContext.Database.MigrateAsync();
            migrationLogger.LogInformation("✅ Veri tabanı migration işlemleri başarıyla uygulandı!");
        }
        else
        {
            migrationLogger.LogInformation("✅ Tüm migration'lar zaten uygulanmış. Bekleyen migration yok.");
        }
    }
    catch (Exception ex)
    {
        migrationLogger.LogError(ex, "❌ Migration sırasında hata oluştu: {Message}", ex.Message);
        if (ex.InnerException != null)
        {
            migrationLogger.LogError("Inner Exception: {InnerException}", ex.InnerException.Message);
        }
        migrationLogger.LogError("Stack Trace: {StackTrace}", ex.StackTrace);
        throw;
    }
};

// Global Exception Handling Middleware (must be first)
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseMiddleware<ValidationExceptionMiddleware>();

// Rate Limiting
app.UseIpRateLimiting();

// CORS
app.UseCors("AllowSpecificOrigins");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();