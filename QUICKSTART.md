# 🎯 Quick Start Guide - Real Estate Management API

## 📋 Hızlı Başlangıç (5 dakika)

### 1️⃣ Projeyi Clone et
```bash
cd c:\GitHub\RealEstateManagementApi
```

### 2️⃣ Dependencies'i İndir
```bash
dotnet restore
```

### 3️⃣ Database Migrasyonlarını Uygula
```bash
# Package Manager Console'da
Update-Database

# Veya Terminal'de
dotnet ef database update
```

### 4️⃣ Redis Başlat (Optional ama recommended)
```bash
# Docker ile
docker run -d -p 6379:6379 redis:latest

# Veya Windows'ta Redis Server'ı başlat
redis-server
```

### 5️⃣ Projeyi Çalıştır
```bash
dotnet run --project RealEstateManagement.API
```

### 6️⃣ Swagger UI'ı Aç
```
https://localhost:5001/swagger/index.html
```

---

## 🗂️ Proje Yapısı

```
RealEstateManagement/
│
├── RealEstateManagement.API/
│   ├── Controllers/          # API endpoints
│   ├── Middleware/           # Custom middleware
│   ├── Services/             # Caching service
│   ├── Configuration/        # Configuration classes
│   ├── Exceptions/           # Custom exceptions
│   ├── Program.cs            # DI setup
│   ├── appsettings.json      # Configuration
│   └── RealEstateManagement.API.csproj
│
├── RealEstateManagement.Business/
│   ├── Abstract/             # Interface definitions
│   ├── Concrete/             # Service implementations
│   ├── Dto/                  # Data Transfer Objects
│   ├── Validators/           # FluentValidation validators
│   ├── Mapping/              # AutoMapper profiles
│   └── RealEstateManagement.Business.csproj
│
├── RealEstateManagement.Data/
│   ├── Abstract/             # Data interfaces
│   ├── Concrete/             # Data implementations
│   ├── Migrations/           # EF Core migrations
│   ├── DbContext.cs          # Database context
│   └── RealEstateManagement.Data.csproj
│
├── RealEstateManagement.Entity/
│   ├── Abstract/             # Base entity classes
│   ├── Concrete/             # Entity models
│   ├── Enums/                # Enumerations
│   └── RealEstateManagement.Entity.csproj
│
├── RealEstateManagement.Tests/
│   ├── Services/             # Service tests
│   ├── Validators/           # Validator tests
│   └── RealEstateManagement.Tests.csproj
│
├── RealEstateManagement.sln
├── COMPLETION_SUMMARY.md     # Tamamlanan işler
├── LOGGING_VALIDATION_GUIDE.md
├── ADVANCED_FEATURES_GUIDE.md
└── README.md
```

---

## 🚀 Önemli Endpoints

### Properties (Emlaklar)
```bash
GET    /api/properties                          # Tüm emlakları listele
GET    /api/properties/{id}                     # Emlak detayı
GET    /api/properties/search?...               # Advanced search
POST   /api/properties                          # Yeni emlak ekle (Agent/Admin)
PUT    /api/properties/{id}                     # Emlak güncelle (Agent/Admin)
DELETE /api/properties/{id}                     # Emlak sil (Agent/Admin)
```

### Property Images (Emlak Resimleri)
```bash
GET    /api/propertyimages/{propertyId}/images
POST   /api/propertyimages/{propertyId}/images
PUT    /api/propertyimages/{propertyId}/images/{imageId}
DELETE /api/propertyimages/{propertyId}/images/{imageId}
```

### Authentication
```bash
POST   /api/auth/login
POST   /api/auth/register
```

---

## 🔑 Konfigürasyon Dosyaları

### appsettings.json
```json
{
  "ConnectionStrings": {
    "PostgreSql": "Host=localhost;Port=5432;...",
    "Redis": "localhost:6379"
  },
  "Jwt": { "Key": "...", "Issuer": "...", "Audience": "..." },
  "Cors": { "AllowedOrigins": [...] },
  "IpRateLimitPolicies": { ... },
  "KeyVault": { "Enabled": false, ... }
}
```

### appsettings.Development.json
```json
{
  "Serilog": {
    "MinimumLevel": { "Default": "Debug", ... },
    ...
  }
}
```

---

## 🧪 Testing

### Unit Tests Çalıştır
```bash
dotnet test

# Belirli test dosyasını çalıştır
dotnet test --filter PropertyImageServiceTests

# Test coverage raporu
dotnet test /p:CollectCoverage=true
```

### Test Dosyaları
- `PropertyImageServiceTests.cs` - Service test örneği
- `PropertyImageCreateDtoValidatorTests.cs` - Validator test örneği

---

## 📝 API Request Örnekleri

### 1. Properties Listesini Al (Paginated)
```bash
curl -X GET "https://localhost:5001/api/properties?pageNumber=1&pageSize=10" \
  -H "Accept: application/json"
```

**Response:**
```json
{
  "data": {
    "items": [...],
    "totalCount": 4,
    "pageNumber": 1,
    "pageSize": 10
  },
  "message": "Operation completed successfully",
  "isSucceed": true,
  "statusCode": 200
}
```

### 2. Advanced Search
```bash
curl -X GET "https://localhost:5001/api/properties/search?minPrice=100000&maxPrice=500000&city=Istanbul&sortBy=price&sortDirection=desc" \
  -H "Accept: application/json"
```

### 3. Yeni Property Ekle
```bash
curl -X POST "https://localhost:5001/api/properties" \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer YOUR_TOKEN" \
  -d '{
    "title": "Beautiful Apartment",
    "description": "Nice apartment in city center",
    "price": 250000,
    "address": "123 Main St",
    "city": "Istanbul",
    "rooms": 2,
    "bathrooms": 1,
    "area": 85,
    "floor": 5,
    "totalFloors": 10,
    "yearBuilt": 2020,
    "propertyTypeId": 1
  }'
```

---

## 🔍 Troubleshooting

### Database Connection Error
```
❌ "Cannot connect to database"
✅ PostgreSQL çalışıyor mu?
✅ Connection string doğru mu?
✅ appsettings.json'da ConnectionString var mı?
```

### Redis Connection Error
```
❌ "Cannot connect to Redis"
✅ Redis çalışıyor mu? (redis-cli ping)
✅ Port 6379 açık mı?
✅ ConnectionString doğru mu?
```

### Validation Error
```
❌ "Validation failed"
✅ Validator kurallarına uyuyor mu?
✅ Required fields ekli mi?
✅ Format (email, URL, phone) doğru mu?
```

### Rate Limiting
```
❌ "429 Too many requests"
✅ 1 dakika boyunca 100 requestten fazla yollama
✅ Auth endpoints için 15 dakika içinde 5 request limit var
```

---

## 📚 Öğrenme Kaynakları

### Dosya Rehberi
- **COMPLETION_SUMMARY.md** - Tüm yapılan işlerin özeti
- **LOGGING_VALIDATION_GUIDE.md** - Logging ve validation detayları
- **ADVANCED_FEATURES_GUIDE.md** - Advanced features rehberi

### External Resources
- [Entity Framework Core Docs](https://docs.microsoft.com/en-us/ef/core/)
- [FluentValidation Docs](https://fluentvalidation.net/)
- [Serilog Docs](https://serilog.net/)
- [StackExchange.Redis](https://stackexchange.github.io/StackExchange.Redis/)

---

## 💡 Tips & Tricks

### 1. Logging Aktiv Et
```csharp
_logger.LogInformation("Property created: {PropertyId}", property.Id);
```

### 2. Cache Kullan
```csharp
await _cachingService.SetAsync("property_1", propertyDto, TimeSpan.FromHours(1));
```

### 3. Validation
```csharp
var validator = new PropertyCreateDtoValidator();
var result = validator.Validate(dto);
```

### 4. Exception Throw Et
```csharp
throw new NotFoundException("Property not found");
```

### 5. Test Yaz
```csharp
[Fact]
public async Task TestMethod() { }
```

---

## 🎯 Development Workflow

```
1. Özellik planla
   ↓
2. DTO + Validator oluştur
   ↓
3. Service metodu yaz
   ↓
4. Controller endpoint ekle
   ↓
5. Unit test yaz
   ↓
6. API test et (Swagger/Postman)
   ↓
7. Logging ekle
   ↓
8. Commit & Push
```

---

## ✨ Frequently Used Commands

```bash
# Restore NuGet packages
dotnet restore

# Build project
dotnet build

# Run project
dotnet run

# Run tests
dotnet test

# Create migration
dotnet ef migrations add MigrationName

# Update database
dotnet ef database update

# Remove migration
dotnet ef migrations remove

# List migrations
dotnet ef migrations list

# Generate migration script
dotnet ef migrations script -o migration.sql

# Clean cache
dotnet nuget locals all --clear
```

---

## 🔐 Security Reminders

⚠️ **BEFORE PRODUCTION:**
- [ ] Change JWT secret key
- [ ] Set proper CORS origins (not *)
- [ ] Enable HTTPS
- [ ] Set up Azure Key Vault for secrets
- [ ] Configure rate limiting limits
- [ ] Enable database backups
- [ ] Set up monitoring
- [ ] Enable audit logging
- [ ] Review security policies
- [ ] Penetration testing

---

## 📞 Support & Help

Sorularınız varsa:
1. COMPLETION_SUMMARY.md okuyun
2. ADVANCED_FEATURES_GUIDE.md kontrol edin
3. Swagger UI'ı inceleyin
4. Unit tests'leri örnek olarak kullanın

---

**Happy Coding!** 🚀

**Version:** 1.0  
**Last Updated:** January 28, 2026
