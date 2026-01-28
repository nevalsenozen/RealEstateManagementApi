using Xunit;
using Moq;
using AutoMapper;
using FluentAssertions;
using RealEstateManagement.Business.Concrete;
using RealEstateManagement.Business.Dto;
using RealEstateManagement.Data.Abstract;
using RealEstateManagement.Entity.Concrete;
using RealEstateManagement.Entity.Enums;

namespace RealEstateManagement.Tests.Services;

public class PropertyImageServiceTests
{
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly Mock<IMapper> _mockMapper;
    private readonly PropertyImageService _service;

    public PropertyImageServiceTests()
    {
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _mockMapper = new Mock<IMapper>();
        _service = new PropertyImageService(_mockUnitOfWork.Object, _mockMapper.Object);
    }

    [Fact]
    public async Task GetPropertyImagesAsync_WithValidPropertyId_ReturnsImages()
    {
        // Arrange
        int propertyId = 1;
        var images = new List<PropertyImage>
        {
            new PropertyImage { Id = 1, PropertyId = propertyId, ImageUrl = "url1" },
            new PropertyImage { Id = 2, PropertyId = propertyId, ImageUrl = "url2" }
        };

        var imageDtos = new List<PropertyImageDto>
        {
            new PropertyImageDto { Id = 1, PropertyId = propertyId, ImageUrl = "url1" },
            new PropertyImageDto { Id = 2, PropertyId = propertyId, ImageUrl = "url2" }
        };

        var mockRepository = new Mock<IRepository<PropertyImage>>();
        mockRepository.Setup(r => r.GetAllAsync(
            It.IsAny<System.Linq.Expressions.Expression<Func<PropertyImage, bool>>>(),
            null,
            false,
            false,
            null))
            .ReturnsAsync(images);

        _mockUnitOfWork.Setup(u => u.GetRepository<PropertyImage>())
            .Returns(mockRepository.Object);

        _mockMapper.Setup(m => m.Map<List<PropertyImageDto>>(images))
            .Returns(imageDtos);

        // Act
        var result = await _service.GetPropertyImagesAsync(propertyId);

        // Assert
        result.IsSucceed.Should().BeTrue();
        result.Data.Should().HaveCount(2);
    }

    [Fact]
    public async Task AddPropertyImageAsync_WithValidData_ReturnsSuccess()
    {
        // Arrange
        var dto = new PropertyImageCreateDto { PropertyId = 1, ImageUrl = "http://example.com/image.jpg" };
        var entity = new PropertyImage { Id = 1, PropertyId = 1, ImageUrl = "http://example.com/image.jpg" };
        var resultDto = new PropertyImageDto { Id = 1, PropertyId = 1, ImageUrl = "http://example.com/image.jpg" };

        var mockRepository = new Mock<IRepository<PropertyImage>>();
        _mockUnitOfWork.Setup(u => u.GetRepository<PropertyImage>())
            .Returns(mockRepository.Object);

        _mockMapper.Setup(m => m.Map<PropertyImage>(dto))
            .Returns(entity);
        _mockMapper.Setup(m => m.Map<PropertyImageDto>(entity))
            .Returns(resultDto);

        // Act
        var result = await _service.AddPropertyImageAsync(1, dto);

        // Assert
        result.IsSucceed.Should().BeTrue();
        mockRepository.Verify(r => r.AddAsync(It.IsAny<PropertyImage>()), Times.Once);
        _mockUnitOfWork.Verify(u => u.SaveAsync(), Times.Once);
    }
}
