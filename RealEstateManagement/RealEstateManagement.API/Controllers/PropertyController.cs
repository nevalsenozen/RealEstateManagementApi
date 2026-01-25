using RealEstateManagement.Business.Abstract;
using RealEstateManagement.Business.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RealEstateManagement.API.Controllers
{
    [Route("api/properties")]
    [ApiController]
    public class PropertiesController : CustomControllerBase
    {
        private readonly IPropertyService _propertyService;

        public PropertiesController(IPropertyService propertyService)
        {
            _propertyService = propertyService;
        }

        
        [HttpGet]
       public async Task<IActionResult> GetAllPropertiesPaged([FromQuery] PropertyFilterDto filterDto)
        {
            // PropertyFilterDto içindeki sayfalama bilgilerini PaginationQueryDto'ya dönüştür
            var paginationQuery = new PaginationQueryDto
            {
                PageNumber = filterDto.PageNumber,
                PageSize = filterDto.PageSize
            };

            // Servise uygun tipte parametre gönder
            var response = await _propertyService.GetAllPagedAsync(paginationQuery);

            return CreateResult(response);
    }



        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPropertyById(int id)
        {
            var response = await _propertyService.GetAsync(id);
            return CreateResult(response);
        }



        [Authorize(Roles = "Agent,Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateProperty([FromBody] PropertyCreateDto propertyCreateDto)
        {
            var response = await _propertyService.CreateAsync(propertyCreateDto);
            return CreateResult(response);
        }

       
        [Authorize(Roles = "Agent,Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProperty(int id, [FromBody] PropertyUpdateDto propertyUpdateDto)
        {
            var response = await _propertyService.UpdateAsync(id, propertyUpdateDto);
            return CreateResult(response);
        }

        
        [Authorize(Roles = "Agent,Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> SoftDeleteProperty(int id)
        {
            var response = await _propertyService.SoftDeleteAsync(id);
            return CreateResult(response);
        }

        
        //(Agent kendi ilanları)
        [Authorize(Roles = "Agent")]
        [HttpGet("my-properties")]
        public async Task<IActionResult> GetMyProperties([FromQuery] PropertyFilterDto filterDto)
        {
            var response = await _propertyService.GetMyPropertiesAsync(filterDto);
            return CreateResult(response);
        }
    }
}
