using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using RealEstateManagement.Business.Abstract;
using RealEstateManagement.Business.Concrete;
using RealEstateManagement.Business.Dto;
using RouteAttribute = Microsoft.AspNetCore.Components.RouteAttribute;

namespace RealEstateManagement.API.Controllers;

    [Route("api/propertyimages")]
    [ApiController]

    public class PropertyImageController
    {

        [HttpGet("{propertyId}/images")]
        public async Task<IActionResult> GetPropertyImages(int propertyId)
        {
            var response = await IPropertyService.GetPropertyImagesAsync(propertyId);
            return CreateResult(response);
        }

        
        [Authorize(Roles = "Agent,Admin")]
        [HttpPost("{propertyId}/images")]
        public async Task<IActionResult> AddPropertyImage(
            int propertyId,
            [FromBody] PropertyImageCreateDto propertyImageCreateDto)
        {
            var response = await PropertyService.AddPropertyImageAsync(propertyId, propertyImageCreateDto);
            return CreateResult(response);
        }

        
        [Authorize(Roles = "Agent,Admin")]
        [HttpPut("{propertyId}/images/{imageId}")]
        public async Task<IActionResult> UpdatePropertyImage(
            int propertyId,
            int imageId,
            [FromBody] PropertyImageUpdateDto propertyImageUpdateDto)
        {
            var response = await _propertyService.UpdatePropertyImageAsync(
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
            var response = await _propertyService.DeletePropertyImageAsync(propertyId, imageId);
            return CreateResult(response);
        }
    }
    