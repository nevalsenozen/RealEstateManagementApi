# Logging, Validation ve Response Handling - Implementation Guide

## 1. SERILOG LOGGING

### Konfigürasyon
- **appsettings.json**: Production log levels
- **appsettings.Development.json**: Debug log levels
- Loglar şu yerlere yazılıyor:
  - Console (real-time görmek için)
  - File (logs/ klasöründe günlük rotasyon)

### Log Levels
- **Debug**: Detailed information
- **Information**: General information
- **Warning**: Warning messages
- **Error**: Error messages
- **Fatal**: Critical failures

### Kullanım Örneği
```csharp
private readonly ILogger<YourController> _logger;

public YourController(ILogger<YourController> logger)
{
    _logger = logger;
}

_logger.LogInformation("Operation started for user: {UserId}", userId);
_logger.LogWarning("Resource not found: {ResourceId}", resourceId);
_logger.LogError(ex, "Operation failed for user: {UserId}", userId);
```

---

## 2. FLUENTVALIDATION

### Validators Oluşturulanlar
1. **PropertyCreateDtoValidator** - Property oluştururken validation
2. **PropertyUpdateDtoValidator** - Property güncellemek için validation
3. **PropertyImageCreateDtoValidator** - Resim ekleme validation
4. **PropertyImageUpdateDtoValidator** - Resim güncelleme validation
5. **PropertyFilterDtoValidator** - Sayfalama validation
6. **UserUpdateDtoValidator** - Kullanıcı profili güncelleme
7. **InquiryUpdateDtoValidator** - İnceleme güncelleme
8. **PropertyTypeCreateDtoValidator** - Emlak türü oluşturma
9. **PropertyTypeUpdateDtoValidator** - Emlak türü güncelleme

### Auto Validation
Program.cs'de aşağıdaki kod eklenmiştir:
```csharp
builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.Services.AddFluentValidationAutoValidation();
```
Bu, tüm controller action'larında otomatik olarak validation yapılmasını sağlar.

### Validation Error Response
```json
{
  "isSucceed": false,
  "message": "Validation failed",
  "validationErrors": {
    "Email": ["Email format is invalid"],
    "Age": ["Age must be greater than 18"]
  },
  "traceId": "trace-id-here"
}
```

### Özel Validator Yazma
```csharp
public class MyDtoValidator : AbstractValidator<MyDto>
{
    public MyDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .MinimumLength(3).WithMessage("Name must be at least 3 characters");

        RuleFor(x => x.Email)
            .EmailAddress().WithMessage("Email must be valid");
    }
}
```

---

## 3. RESPONSE HANDLING

### Response DTO Yapısı
```csharp
public class ResponseDto<T>
{
    public T? Data { get; set; }
    public string? Message { get; set; }
    public string? Error { get; set; }
    public bool IsSucceed { get; set; }
    public string? TraceId { get; set; }
    public Dictionary<string, string[]>? ValidationErrors { get; set; }
    public int StatusCode { get; set; }
}
```

### Success Response
```csharp
// Veri ile
return ResponseDto<PropertyDto>.Success(
    propertyDto, 
    "Property retrieved successfully", 
    200
);

// Veri olmadan
return ResponseDto<PropertyDto>.Success(200, "Operation completed");
```

### Error Response
```csharp
// Basit error
return ResponseDto<PropertyDto>.Fail(
    "Property not found", 
    404, 
    "The requested property does not exist"
);

// Validation error
return ResponseDto<PropertyDto>.FailWithValidation(
    new Dictionary<string, string[]>
    {
        { "Title", new[] { "Title is required" } }
    }
);
```

### Response Örnekleri

**Success Response (200)**
```json
{
  "data": { "id": 1, "title": "Beautiful House" },
  "message": "Operation completed successfully",
  "isSucceed": true,
  "traceId": "0HMVBI78LC1U5:00000001",
  "validationErrors": null,
  "statusCode": 200
}
```

**Error Response (400)**
```json
{
  "data": null,
  "message": "Invalid request",
  "error": "Email format is invalid",
  "isSucceed": false,
  "traceId": "0HMVBI78LC1U5:00000002",
  "validationErrors": null,
  "statusCode": 400
}
```

**Validation Error Response (400)**
```json
{
  "data": null,
  "message": "Validation failed",
  "isSucceed": false,
  "traceId": "0HMVBI78LC1U5:00000003",
  "validationErrors": {
    "Email": ["Email is required", "Email format is invalid"],
    "Password": ["Password must be at least 8 characters"]
  },
  "statusCode": 400
}
```

---

## 4. EXCEPTION HANDLING

### Exception Types
| Exception | Status Code | Handler |
|-----------|------------|---------|
| NotFoundException | 404 | ExceptionHandlingMiddleware |
| ValidationException | 400 | ValidationExceptionMiddleware |
| ConflictException | 409 | ExceptionHandlingMiddleware |
| BusinessException | 400 | ExceptionHandlingMiddleware |
| ArgumentNullException | 400 | ExceptionHandlingMiddleware |
| UnauthorizedAccessException | 401 | ExceptionHandlingMiddleware |
| Diğer Exception | 500 | ExceptionHandlingMiddleware |

### Custom Exception Kullanımı
```csharp
public async Task<PropertyDto> GetPropertyAsync(int id)
{
    var property = await _repository.GetAsync(id);
    
    if (property == null)
        throw new NotFoundException($"Property with id {id} not found");
    
    return _mapper.Map<PropertyDto>(property);
}
```

---

## 5. LOG FILES

Loglar otomatik olarak şu yerde saklanır:
```
/logs/log-2024-01-28.txt
/logs/log-2024-01-29.txt
```

Her gün yeni bir dosya oluşturulur (Rolling File Sink).

---

## 6. BEST PRACTICES

1. **Logging**: Önemli işlemlerin başında ve sonunda log yazın
2. **Validation**: DTO'lar için her zaman validator oluşturun
3. **Exception Handling**: Custom exceptions kullanın, generic exceptions kullanmayın
4. **Response Format**: Daima ResponseDto<T> döndürün
5. **Sensitive Data**: Log'lara password, token vb. sensitive data yazmayın

---

## 7. DEBUGGING

### Logları Real-time İzlemek
```powershell
# Windows
tail -f logs/log-*.txt

# Linux/Mac
tail -f logs/log-*.txt
```

### Serilog Configuration Debugging
```csharp
// Program.cs'de şu şekilde enable edin
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .CreateLogger();
```

