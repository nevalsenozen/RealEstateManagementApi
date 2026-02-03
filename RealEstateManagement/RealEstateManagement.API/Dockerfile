# 1.STAGE
# Burası "build stage". Burada dotnet 10.0 Sdk'ini ilgili adresten indir(kopyalamak için)
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

# src adında bir klasör oluştur ve oraya geç
WORKDIR /src

# Host'taki RealEstateManagement.sln dosyasını, Container'daki /src klasörüne kopyala.
COPY RealEstateManagement.sln .

# Proje dosyalarını kopyala
COPY RealEstateManagement.API/*.*proj ./RealEstateManagement.API/RealEstateManagement.API/
COPY RealEstateManagement.Business/*.*proj ./RealEstateManagement.Business/
COPY RealEstateManagement.Entity/*.*proj ./RealEstateManagement.Entity/
COPY RealEstateManagement.Data/*.*proj ./RealEstateManagement.Data/

# Copy all source code
COPY . .

# API projesine geç
WORKDIR /src/RealEstateManagement.API

# Restore packages
RUN dotnet restore

# Uygulamayı production için hazırla
RUN dotnet publish -c Release -o /app/publish --no-restore

# 2.STAGE
# Burası "runtime stage". Burada Asp.Net Core 10.0 Runtime'ı ilgili adresten indir
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime

# Build stage'deki publish edilmiş dosyaları kopyala
COPY --from=build /app/publish .

# Expose port
EXPOSE 8080

# Health check
HEALTHCHECK --interval=30s --timeout=3s --start-period=10s --retries=3 \
    CMD curl -f http://localhost/health || exit 1

# Run the application
ENTRYPOINT ["dotnet", "RealEstateManagement.API.dll"]