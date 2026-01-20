using Microsoft.AspNetCore.Mvc;
using RealEstateManagement.Business.Concrete;
using RealEstateManagement.Business.Dto;
using RealEstateManagement.Entity.Concrete;

namespace RealEstateManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PropertyController : ControllerBase
    {
        private readonly PropertyService _service;

        public PropertyController(PropertyService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PropertyCreateDto dto)
        {
            var property = await _service.CreatePropertyAsync(dto);
            return Ok(property); // Şu an entity dönüyor, response DTO ekleyebiliriz
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var properties = await _service.GetAllPropertiesAsync();
            return Ok(properties);
        }
    }
}
