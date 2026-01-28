using Xunit;
using FluentValidation;
using FluentAssertions;
using RealEstateManagement.Business.Dto;
using RealEstateManagement.Business.Validators;

namespace RealEstateManagement.Tests.Validators;

public class PropertyImageCreateDtoValidatorTests
{
    private readonly PropertyImageCreateDtoValidator _validator;

    public PropertyImageCreateDtoValidatorTests()
    {
        _validator = new PropertyImageCreateDtoValidator();
    }

    [Fact]
    public void Validate_WithValidData_ReturnsSuccess()
    {
        // Arrange
        var dto = new PropertyImageCreateDto
        {
            PropertyId = 1,
            ImageUrl = "https://example.com/image.jpg"
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Validate_WithInvalidPropertyId_ReturnsFalse()
    {
        // Arrange
        var dto = new PropertyImageCreateDto
        {
            PropertyId = 0,
            ImageUrl = "https://example.com/image.jpg"
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Validate_WithInvalidUrl_ReturnsFalse()
    {
        // Arrange
        var dto = new PropertyImageCreateDto
        {
            PropertyId = 1,
            ImageUrl = "not-a-valid-url"
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        result.IsValid.Should().BeFalse();
    }
}
