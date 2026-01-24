using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstateManagement.API.Controllers;

namespace RealEstate.API.Controllers
{
    [Route("api/propertytypes")]
    [ApiController]
    public class PropertyTypesController : CustomControllerBase
    {
        // GET /api/propertytypes
        [HttpGet]
        public async Task<IActionResult> GetAllPropertyTypes()
        {
            return base.Ok();
        }

        // GET /api/propertytypes/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPropertyTypeById(int id)
        {
            return base.Ok();
        }

        // POST /api/propertytypes  (Admin)
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreatePropertyType()
        {
            return base.Ok();
        }

        // PUT /api/propertytypes/{id}  (Admin)
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePropertyType(int id)
        {
            return base.Ok();
        }

        // DELETE /api/propertytypes/{id}  (Admin)
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePropertyType(int id)
        {
            return base.Ok();
        }
    }
}