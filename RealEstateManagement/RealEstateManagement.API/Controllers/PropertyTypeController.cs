using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstateManagement.API.Controllers;
using RealEstateManagement.Business.Abstract;
using RealEstateManagement.Business.Dto;

namespace RealEstate.API.Controllers
{
    [Route("api/propertytypes")]
    [ApiController]
    public class PropertyTypesController : CustomControllerBase
    {
        private readonly IPropertyTypeService _propertyTypeService;

        public PropertyTypesController(IPropertyTypeService propertyTypeService)
        {
            _propertyTypeService = propertyTypeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPropertyTypes()
        {
            var result = await _propertyTypeService.GetAllAsync();
            return CreateResult(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPropertyTypeById(int id)
        {
            var result = await _propertyTypeService.GetAsync(id);
            return CreateResult(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreatePropertyType([FromBody] PropertyTypeCreateDto propertyTypeCreateDto)
        {
            var result = await _propertyTypeService.CreateAsync(propertyTypeCreateDto);
            return CreateResult(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePropertyType(int id, [FromBody] PropertyTypeUpdateDto propertyTypeUpdateDto)
        {
            var result = await _propertyTypeService.UpdateAsync(id, propertyTypeUpdateDto);
            return CreateResult(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePropertyType(int id)
        {
            var result = await _propertyTypeService.DeleteAsync(id);
            return CreateResult(result);
        }
    }
}