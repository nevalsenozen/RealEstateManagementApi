# Real Estate Management API - Advanced Features Setup Guide

## Tamamlanan Özellikler (✅ Completed)

---

## 1. DATABASE SEEDING ✅

### Neler Yapıldı?
- **DbContext.cs** updated with seed data
- **OnModelCreating()** metodu eklendi
- 5 PropertyType seed data
- 4 Property seed data
- 4 PropertyImage seed data

### Seed Data İçeriği
```csharp
// PropertyTypes: Apartment, House, Villa, Commercial, Land
// Properties: 4 örnek emlak
// PropertyImages: Her emlaka resim
```

### Kullanım
Migration çalıştırıldığında otomatik olarak seed data'lar veritabanına yüklenir:
```bash
dotnet ef database update
```

---

## 2. API DOCUMENTATION ✅

### Neler Yapıldı?
- XML Summary comments PropertyImageController'a eklendi
- **[ProduceResponseType]** attributes eklendi
- Her endpoint'in açıklaması yapıldı
- HTTP status codes belirtildi

### Swagger Örnekleri
```csharp
/// <summary>
/// Gets all images for a specific property
/// </summary>
/// <param name="propertyId">The ID of the property</param>
/// <returns>A list of property images</returns>
[HttpGet("{propertyId}/images")]
[ProduceResponseType(StatusCodes.Status200OK)]
[ProduceResponseType(StatusCodes.Status404NotFound)]
```

### Enable Etme
`Swagger`UI'da otomatik olarak çalışıyor:
- http://localhost:5000/swagger/index.html

---

## 3. UNIT TESTS ✅

### Oluşturulan Dosyalar
- `RealEstateManagement.Tests` projesi
- `PropertyImageServiceTests.cs` - Service test örneği
- `PropertyImageCreateDtoValidatorTests.cs` - Validator test örneği

### NuGet Packages
- xunit v2.6.6
- Moq v4.20.70
- FluentAssertions v6.12.0
- Microsoft.EntityFrameworkCore.InMemory

### Test Çalıştırma
```bash
cd RealEstateManagement.Tests
dotnet test
```

### Test Örneği
```csharp
[Fact]
public async Task GetPropertyImagesAsync_WithValidPropertyId_ReturnsImages()
{
    // Arrange - Setup
    // Act - Execute
    // Assert - Verify
}
```

---

## 4. REDIS CACHING ✅

### NuGet Package
- StackExchange.Redis v2.7.4

### Özellikler
- **ICachingService** interface oluşturuldu
- **RedisCachingService** implementation
- Get, Set, Remove, Exists işlemleri

### Kullanım
```csharp
private readonly ICachingService _cachingService;

// Cache'e yazma
await _cachingService.SetAsync("property_1", propertyDto, TimeSpan.FromHours(1));

// Cache'ten okuma
var cached = await _cachingService.GetAsync<PropertyDto>("property_1");

// Cache'ten silme
await _cachingService.RemoveAsync("property_1");
```

### Configuration
```json
"ConnectionStrings": {
  "Redis": "localhost:6379"
}
```

### Redis Başlatma
```bash
# Windows
redis-server

# Docker
docker run -d -p 6379:6379 redis:latest
```

---

## 5. RATE LIMITING & SECURITY ✅

### Rate Limiting
- **AspNetCoreRateLimit** v4.0.1 paketi
- IP-based rate limiting
- Default: 100 requests/minute
- Auth endpoints: 5 requests/15 minutes

### CORS Ayarları
```json
"Cors": {
  "AllowedOrigins": [ 
    "http://localhost:3000", 
    "http://localhost:5173" 
  ],
  "AllowedMethods": [ "GET", "POST", "PUT", "DELETE", "OPTIONS" ],
  "AllowedHeaders": [ "*" ]
}
```

### Program.cs'de Setup
```csharp
// CORS
app.UseCors("AllowSpecificOrigins");

// Rate Limiting
app.UseIpRateLimiting();
```

### Response (Rate Limit Exceeded)
```json
{
  "statusCode": 429,
  "message": "Too many requests, please try again later"
}
```

---

## 6. ADVANCED FILTERING ✅

### Yeni DTO: AdvancedPropertyFilterDto

**Filtreleme Parametreleri:**
```csharp
public decimal? MinPrice { get; set; }
public decimal? MaxPrice { get; set; }
public decimal? MinArea { get; set; }
public decimal? MaxArea { get; set; }
public int? MinRooms { get; set; }
public int? MaxRooms { get; set; }
public int? PropertyTypeId { get; set; }
public string? City { get; set; }
public string? District { get; set; }
public int? Status { get; set; }
public int? MinYearBuilt { get; set; }
public string? SearchKeyword { get; set; }
public string? SortBy { get; set; }    // price, area, createdAt, title
public string? SortDirection { get; set; } // asc or desc
```

### API Örneği
```bash
GET /api/properties/search?minPrice=100000&maxPrice=500000&city=Istanbul&sortBy=price&sortDirection=desc&pageNumber=1&pageSize=10
```

### Validator
- **AdvancedPropertyFilterDtoValidator** oluşturuldu
- Tüm parametrelerin validation'ı yapılıyor

---

## 7. AZURE KEY VAULT ✅

### NuGet Packages
- Azure.Identity v1.10.4
- Azure.Security.KeyVault.Secrets v4.7.0

### Konfigürasyon
```json
"KeyVault": {
  "Enabled": false,
  "VaultUri": "https://your-keyvault.vault.azure.net/"
}
```

### Kullanım

**Adım 1: Key Vault'u Azure Portal'da Oluştur**
- Create Key Vault
- Secrets ekle (ConnectionString, JwtKey, vb)

**Adım 2: Program.cs'de Yapılandır**
```csharp
builder.Configuration.AddAzureKeyVault(builder.Configuration, builder.Environment.EnvironmentName);
```

**Adım 3: Secrets'dan Oku**
```csharp
var connectionString = builder.Configuration.GetSecret("DbConnectionString");
var jwtKey = builder.Configuration.GetSecret("JwtKey");
```

### Best Practice
```csharp
// Development: appsettings.json
// Production: Azure Key Vault (çok daha güvenli!)
```

---

## 📋 Program.cs Konfigürasyonu

```csharp
// 1. Serilog
builder.Host.UseSerilog();

// 2. FluentValidation
builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.Services.AddFluentValidationAutoValidation();

// 3. Redis
builder.Services.AddSingleton<IConnectionMultiplexer>(redis);
builder.Services.AddScoped<ICachingService, RedisCachingService>();

// 4. Rate Limiting
builder.Services.AddMemoryCache();
builder.Services.Configure<IpRateLimitOptions>(builder.Configuration.GetSection("IpRateLimitPolicies"));
builder.Services.AddInMemoryRateLimiting();

// 5. CORS
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

// Middleware Sırası (ÖNEMLI!)
app.UseMiddleware<ExceptionHandlingMiddleware>();      // 1. Exception Handler
app.UseMiddleware<ValidationExceptionMiddleware>();    // 2. Validation
app.UseIpRateLimiting();                               // 3. Rate Limiting
app.UseCors("AllowSpecificOrigins");                   // 4. CORS
app.UseAuthentication();                                // 5. Auth
app.UseAuthorization();                                 // 6. Authorization
```

---

## 🧪 Testing Örneği

### Unit Test Çalıştırma
```bash
# Tüm testleri çalıştır
dotnet test

# Belirli test dosyasını çalıştır
dotnet test --filter PropertyImageServiceTests

# Test coverage raporu
dotnet test /p:CollectCoverage=true
```

### Test Proje Yapısı
```
RealEstateManagement.Tests/
├── Services/
│   └── PropertyImageServiceTests.cs
├── Validators/
│   └── PropertyImageCreateDtoValidatorTests.cs
└── RealEstateManagement.Tests.csproj
```

---

## 📝 Best Practices

### 1. Database Seeding
✅ Production data için seed kullanma
✅ Test data için seed kullanma
✅ Migrations'la birlikte otomatik çalışması

### 2. API Documentation
✅ Tüm public methods'a XML comments
✅ ProduceResponseType attributes
✅ Swagger UI güncel tutma

### 3. Caching
✅ Sık erişilen verileri cache etme
✅ Cache invalidation stratejisi
✅ TTL (Time To Live) ayarları

### 4. Rate Limiting
✅ API abuse'dan koruma
✅ Different limits for different endpoints
✅ Per-user rate limiting consideration

### 5. Security
✅ CORS policy dikkatli ayarla
✅ Sensitive data'yı Key Vault'a koy
✅ HTTPS her zaman enable et

### 6. Testing
✅ Tüm business logic için test
✅ Mock external dependencies
✅ Assertion'ları açık ve readable tutma

---

## 🚀 Production Checklist

- [ ] Database migrations production'da çalıştırıldı
- [ ] Redis production'da kurulu ve çalışıyor
- [ ] Azure Key Vault secrets yüklendi
- [ ] CORS origins production domain'lerine set edildi
- [ ] Rate limiting thresholds'lar optimize edildi
- [ ] All unit tests passing
- [ ] API documentation complete
- [ ] SSL/TLS certificates enabled
- [ ] Logging configured for monitoring
- [ ] Backup strategy implemented

---

## 📚 Faydalı Komutlar

```bash
# Migration oluştur
dotnet ef migrations add MigrationName

# Migration uygula
dotnet ef database update

# Migration geri al
dotnet ef database update PreviousMigration

# Test çalıştır
dotnet test

# Build et
dotnet build

# Publish et
dotnet publish -c Release

# Redis bağlantı kontrol et
redis-cli ping
```

---

## 🔗 Sonraki Adımlar

1. **Monitoring**: Application Insights/Datadog entegrasyonu
2. **Performance**: Caching strategy refinement
3. **Security**: OAuth2/OpenID Connect integration
4. **Documentation**: API documentation complete
5. **CI/CD**: GitHub Actions/Azure DevOps pipelines

---

**Created:** January 28, 2026  
**Version:** 1.0  
**Status:** ✅ Complete
