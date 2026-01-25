using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstateManagement.API.Controllers;
using RealEstateManagement.Business.Dto;

namespace RealEstate.API.Controllers
{
    [Route("api/propertytypes")]
    [ApiController]
    public class PropertyTypesController : CustomControllerBase
    {
    
        [HttpGet]
        public async Task<IActionResult> GetAllPropertyTypes()
        {
            return base.Ok();
        }

        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPropertyTypeById(int id)
        {
            return base.Ok();
        }

        
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreatePropertyType([FromBody] PropertyTypeCreateDto propertyTypeCreateDto)
        {
            return base.Ok();
        }

        
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePropertyType(int id, [FromBody] PropertyTypeUpdateDto propertyTypeUpdateDto)
        {
            return base.Ok();
        }

        
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePropertyType(int id)
        {
            return base.Ok();
        }
    }
}