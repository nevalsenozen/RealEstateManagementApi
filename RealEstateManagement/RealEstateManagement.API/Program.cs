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
using Serilog;
using FluentValidation;
using StackExchange.Redis;
using AspNetCoreRateLimit;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

        // Configure Serilog
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .CreateLogger();

        builder.Host.UseSerilog();

       
        builder.Services.AddControllers();

        // Add FluentValidation
        builder.Services.AddValidatorsFromAssemblyContaining<Program>();
        builder.Services.AddFluentValidationAutoValidation();
        builder.Services.AddFluentValidationClientsideAdapters();
        
        builder.Services.AddAutoMapper(typeof(MappingProfile));

        
        builder.Services.AddDbContext<RealEstateManagementDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSql")));

        // Redis Caching
        var redisConnection = builder.Configuration.GetConnectionString("Redis");
        var redis = ConnectionMultiplexer.Connect(redisConnection ?? "localhost:6379");
        builder.Services.AddSingleton<IConnectionMultiplexer>(redis);
        builder.Services.AddScoped<ICachingService, RedisCachingService>();

        // Rate Limiting
        builder.Services.AddMemoryCache();
        builder.Services.Configure<IpRateLimitOptions>(builder.Configuration.GetSection("IpRateLimitPolicies"));
        builder.Services.AddInMemoryRateLimiting();
        builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

        // CORS
        var corsConfig = builder.Configuration.GetSection("Cors");
        var allowedOrigins = corsConfig.GetSection("AllowedOrigins").Get<string[]>() ?? new[] { "http://localhost:3000" };
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowSpecificOrigins", policy =>
            {
                policy.WithOrigins(allowedOrigins)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
            });
        });

        // Repository Pattern & Unit of Work
        builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Business Services
        builder.Services.AddScoped<IPropertyImageService, PropertyImageService>();
        builder.Services.AddScoped<IPropertyService, PropertyService>();
        builder.Services.AddScoped<IPropertyTypeService, PropertyTypeService>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IInquiryService, InquiryService>();
        builder.Services.AddScoped<IAuthService, AuthService>();

       
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)
                    )
                };
            });


        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

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
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        try
        {
            app.Run();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Application terminated unexpectedly");
        }
        finally
        {
            Log.CloseAndFlush();
        }
