using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstateManagement.Business.Abstract;
using RealEstateManagement.Business.Dto;

namespace RealEstateManagement.API.Controllers
{
    [Route("api/propertyimages")]
    [ApiController]
    public class PropertyImageController : CustomControllerBase
    {
        private readonly IPropertyImageService _propertyImageService;

        public PropertyImageController(IPropertyImageService propertyImageService)
        {
            _propertyImageService = propertyImageService;
        }

        [HttpGet("{propertyId}/images")]
        public async Task<IActionResult> GetPropertyImages(int propertyId)
        {
            var response = await _propertyImageService.GetPropertyImagesAsync(propertyId);
            return CreateResult(response);
        }

        [Authorize(Roles = "Agent,Admin")]
        [HttpPost("{propertyId}/images")]
        public async Task<IActionResult> AddPropertyImage(
            int propertyId,
            [FromBody] PropertyImageCreateDto propertyImageCreateDto)
        {
            var response = await _propertyImageService.AddPropertyImageAsync(propertyId, propertyImageCreateDto);
            return CreateResult(response);
        }

        [Authorize(Roles = "Agent,Admin")]
        [HttpPut("{propertyId}/images/{imageId}")]
        public async Task<IActionResult> UpdatePropertyImage(
            int propertyId,
            int imageId,
            [FromBody] PropertyImageUpdateDto propertyImageUpdateDto)
        {
            var response = await _propertyImageService.UpdatePropertyImageAsync(
                propertyId,
                imageId,
                propertyImageUpdateDto
            );

            return CreateResult(response);
        }

        [Authorize(Roles = "Agent,Admin")]
        [HttpDelete("{propertyId}/images/{imageId}")]
        public async Task<IActionResult> DeletePropertyImage(int propertyId, int imageId)
        {
            var response = await _propertyImageService.DeletePropertyImageAsync(propertyId, imageId);
            return CreateResult(response);
        }
    }
}